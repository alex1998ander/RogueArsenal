using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IUpgradeablePlayer {
    private PlayerInput _playerInput;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private Weapon weapon;

    private Rigidbody2D _rb;

    private Vector2 _mousePosition;
    private Vector2 _movementInput;
    private bool _isMouse;
    private Vector3 _aimDirection;
    private float _angle;

    private float _currentHealth;

    private void Awake() {
        _playerInput = GetComponent<PlayerInput>();
        _rb = GetComponent<Rigidbody2D>();

        ResetHealth();
    }

    void FixedUpdate() {
        _rb.MovePosition(_rb.position + _movementInput * (moveSpeed * UpgradeManager.GetMovementSpeedMultiplier() * Time.fixedDeltaTime));
        UpgradeManager.MovementUpdate(this);
    }
    
    private void OnMove(InputValue value) {
        _movementInput = value.Get<Vector2>();
    }

    private void OnFire() {
        weapon.Fire();
        UpgradeManager.OnFire(this);
    }

    private void OnAim(InputValue value) {
        _aimDirection = value.Get<Vector2>();
        if (Vector2.Distance(Vector2.zero, _aimDirection) > 0.5) {
            if (_playerInput.currentControlScheme.Equals("Keyboard&Mouse")) {
                _aimDirection = (Vector2)Camera.main.ScreenToWorldPoint(_aimDirection) - _rb.position;
            }

            _angle = Mathf.Atan2(_aimDirection.y, _aimDirection.x) * Mathf.Rad2Deg - 90;
            _rb.rotation = _angle;
            Debug.Log(_angle);
        }
    }

    /// <summary>
    /// Resets the player's health. The currently active upgrades are taken into account.
    /// </summary>
    public void ResetHealth() {
        _currentHealth = maxHealth * UpgradeManager.GetHealthMultiplier();
    }

    /// <summary>
    /// Decreases the player's health by the specified value and checks if the player dies. If so, affecting upgrades are performed and further actions are initiated.
    /// </summary>
    /// <param name="damageAmount">Amount of damage the player should take</param>
    public void TakeDamage(float damageAmount) {
        _currentHealth -= damageAmount;

        if (_currentHealth <= 0) {
            UpgradeManager.OnPlayerDeath(this);
            if (_currentHealth <= 0) {
                // further actions
            }
        }
    }

    #region Upgrade implementation

    public void ExecuteBurst_OnFire() {
        StartCoroutine(BurstCoroutine());
    }

    private IEnumerator BurstCoroutine() {
        yield return new WaitForSeconds(0.2f);
        weapon.Fire();
        yield return new WaitForSeconds(0.2f);
        weapon.Fire();
    }

    public void ExecuteHealingField_OnBlock() {
        throw new NotImplementedException();
    }

    public void ExecutePhoenix_OnPlayerDeath() {
        ResetHealth();
    }

    #endregion
}