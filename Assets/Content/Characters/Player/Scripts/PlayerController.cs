using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IUpgradeablePlayer {
    private PlayerInput _playerInput;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Weapon weapon;

    private Rigidbody2D _rigidbody;
    private PlayerHealth _playerHealth;

    private Vector2 _mousePosition;
    private Vector2 _movementInput;
    private bool _isMouse;
    private Vector3 _aimDirection;
    private float _angle;

    private void Awake() {
        _playerInput = GetComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerHealth = GetComponent<PlayerHealth>();

        _playerHealth.ResetHealth();

        UpgradeManager.Init(this);
    }

    void FixedUpdate() {
        _rigidbody.MovePosition(_rigidbody.position + _movementInput * (moveSpeed * UpgradeManager.GetMovementSpeedMultiplier() * Time.fixedDeltaTime));
        UpgradeManager.PlayerUpdate(this);
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
                _aimDirection = (Vector2)Camera.main.ScreenToWorldPoint(_aimDirection) - _rigidbody.position;
            }

            _angle = Mathf.Atan2(_aimDirection.y, _aimDirection.x) * Mathf.Rad2Deg - 90;
            _rigidbody.rotation = _angle;
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
        _playerHealth.ResetHealth();
    }

    #endregion
}