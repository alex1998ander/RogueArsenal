using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IUpgradeablePlayer, ICharacterController
{
    private PlayerInput _playerInput;
    private Rigidbody2D _rigidbody;
    private PlayerHealth _playerHealth;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float defaultFireDelay = 0.4f;
    [SerializeField] private PlayerWeapon playerWeapon;

    private Vector2 _mousePosition;
    private Vector2 _movementInput;
    private bool _isMouse;
    private Vector3 _aimDirection;
    private float _angle;

    private float _nextShot;

    // Upgrade: Burst
    [Header("Upgrade: Burst")] [SerializeField] private float burstDelayInSec = 0.2f;

    // Upgrade: Demonic Pact 
    [Header("Upgrade: Demonic Pact")] [SerializeField] private float demonicPactHealthLoss = 10f;

    // Upgrade: Phoenix
    private bool _phoenixed;

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

    #region Player input

    private void OnMove(InputValue value)
    {
        _movementInput = value.Get<Vector2>();
    }

    private void OnFire()
    {
        if (Time.time > _nextShot)
        {
            // This assignment has to be done before "UpgradeManager.OnFire()" so that the variable can be overwritten by this function if necessary
            _nextShot = Time.time + defaultFireDelay * UpgradeManager.GetAttackDelayMultiplier();

            playerWeapon.Fire();
            UpgradeManager.OnFire(this);
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

    #endregion

    #region Upgrade implementation

    // Upgrade: Burst
    public void ExecuteBurst_OnFire()
    {
        StartCoroutine(BurstCoroutine());
    }

    private IEnumerator BurstCoroutine()
    {
        yield return new WaitForSeconds(burstDelayInSec);
        playerWeapon.Fire();
        yield return new WaitForSeconds(burstDelayInSec);
        playerWeapon.Fire();
    }


    // Upgrade: Demonic Pact 

    public void ExecuteDemonicPact_OnFire()
    {
        _playerHealth.InflictDamage(demonicPactHealthLoss, false, this);
        _nextShot = 0f;
    }


    // Upgrade: Healing Field 
    public void ExecuteHealingField_OnBlock()
    {
        throw new NotImplementedException();
    }


    // Upgrade: Phoenix
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