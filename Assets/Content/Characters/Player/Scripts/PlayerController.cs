using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IUpgradeablePlayer, ICharacterController
{
    private PlayerInput _playerInput;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float defaultFireDelay = 0.4f;
    [SerializeField] private PlayerWeapon playerWeapon;

    private Rigidbody2D _rigidbody;
    private PlayerHealth _playerHealth;

    private float _nextShot = 0.0f;

    private Vector2 _mousePosition;
    private Vector2 _movementInput;
    private bool _isMouse;
    private Vector3 _aimDirection;
    private float _angle;

    private bool _phoenixed = false;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerHealth = GetComponent<PlayerHealth>();

        _playerHealth.ResetHealth();

        UpgradeManager.Init(this);
    }

    void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + _movementInput * (moveSpeed * UpgradeManager.GetMovementSpeedMultiplier() * Time.fixedDeltaTime));
        UpgradeManager.PlayerUpdate(this);
    }

    private void OnMove(InputValue value)
    {
        _movementInput = value.Get<Vector2>();
    }

    private void OnFire()
    {
        if (Time.time > _nextShot)
        {
            playerWeapon.Fire();
            UpgradeManager.OnFire(this);

            _nextShot = Time.time + defaultFireDelay * UpgradeManager.GetAttackDelayMultiplier();
        }
    }

    private void OnAim(InputValue value)
    {
        _aimDirection = value.Get<Vector2>();
        if (Vector2.Distance(Vector2.zero, _aimDirection) > 0.5)
        {
            if (_playerInput.currentControlScheme.Equals("Keyboard&Mouse"))
            {
                _aimDirection = (Vector2)Camera.main.ScreenToWorldPoint(_aimDirection) - _rigidbody.position;
            }

            _angle = Mathf.Atan2(_aimDirection.y, _aimDirection.x) * Mathf.Rad2Deg - 90;
            _rigidbody.rotation = _angle;
        }
    }

    #region Upgrade implementation

    public void ExecuteBurst_OnFire()
    {
        StartCoroutine(BurstCoroutine());
    }

    private IEnumerator BurstCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        playerWeapon.Fire();
        yield return new WaitForSeconds(0.2f);
        playerWeapon.Fire();
    }

    public void ExecuteHealingField_OnBlock()
    {
        throw new NotImplementedException();
    }

    public void ExecutePhoenix_OnPlayerDeath()
    {
        if (!_phoenixed)
        {
            Debug.Log("The player has died, but he rises from the ashes with the power of a phoenix");
            _playerHealth.ResetHealth();
            _phoenixed = true;
        }
    }

    #endregion
}