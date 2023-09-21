using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IUpgradeablePlayer, ICharacterController
{
    private PlayerInput _playerInput;
    private Rigidbody2D _rigidbody;
    private PlayerHealth _playerHealth;

    private static float _maxHealth = 200f;
    private static float _defaultDamage = 35f;
    private static float _moveSpeed = 5f;
    [SerializeField] private float defaultFireDelay = 0.4f;
    [SerializeField] private float defaultBlockDelay = 5.0f;
    [SerializeField] private PlayerWeapon playerWeapon;

    [SerializeField] private Transform playerSpriteTransform;

    private Vector2 _mousePosition;
    private Vector2 _movementInput;
    private bool _isMouse;
    private Vector3 _aimDirection;
    private float _angle;

    private float _fireCooldownEndTimestamp;
    private float _blockCooldownEndTimestamp;

    // Upgrade: Burst
    [Header("Upgrade: Burst")] [SerializeField]
    private float burstDelayInSec = 0.1f;

    // Upgrade: Demonic Pact 
    [Header("Upgrade: Demonic Pact")] [SerializeField]
    private float demonicPactHealthLoss = 10f;

    // Upgrade: Healing Field 
    [Header("Upgrade: Healing Field")] [SerializeField]
    private GameObject healingFieldPrefab;

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
        _rigidbody.MovePosition(_rigidbody.position +
                                _movementInput * (GetPlayerMovementSpeed() * Time.fixedDeltaTime));
        UpgradeManager.PlayerUpdate(this);
    }

    public ICharacterHealth GetHealthManager()
    {
        return _playerHealth;
    }

    public static float GetMaxHealth()
    {
        return (_maxHealth + UpgradeManager.MaxHealthIncrease.Value) * UpgradeManager.GetHealthMultiplier();
    }

    public static float GetBulletDamage()
    {
        return (_defaultDamage + UpgradeManager.BulletDamageIncrease.Value) *
               UpgradeManager.GetBulletDamageMultiplier();
    }

    public static float GetPlayerMovementSpeed()
    {
        return (_moveSpeed + UpgradeManager.PlayerMovementSpeedIncrease.Value) *
               UpgradeManager.GetPlayerMovementSpeedMultiplier();
    }

    public void StunCharacter()
    {
        throw new NotImplementedException();
    }

    #region Player input

    private void OnMove(InputValue value)
    {
        if (!GameManager.GamePaused)
            _movementInput = value.Get<Vector2>();
        else
            _movementInput = Vector2.zero;
    }

    private void OnFire()
    {
        if (!GameManager.GamePaused && Time.time > _fireCooldownEndTimestamp)
        {
            // This assignment has to be done before "UpgradeManager.OnFire()" so that the variable can be overwritten by this function if necessary
            _fireCooldownEndTimestamp = Time.time + defaultFireDelay * UpgradeManager.GetFireDelayMultiplier();

            playerWeapon.Fire();
            UpgradeManager.OnFire(this);
        }
    }

    private void OnBlock()
    {
        if (!GameManager.GamePaused && Time.time > _blockCooldownEndTimestamp)
        {
            // This assignment has to be done before "UpgradeManager.OnBlock()" so that the variable can be overwritten by this function if necessary
            _blockCooldownEndTimestamp = Time.time + defaultBlockDelay * UpgradeManager.GetBlockDelayMultiplier();

            UpgradeManager.OnBlock(this);
        }
    }

    private void OnAim(InputValue value)
    {
        if (!GameManager.GamePaused)
        {
            _aimDirection = value.Get<Vector2>();
            if (Vector2.Distance(Vector2.zero, _aimDirection) > 0.5)
            {
                if (_playerInput.currentControlScheme.Equals("Keyboard&Mouse"))
                {
                    _aimDirection = (Vector2) Camera.main.ScreenToWorldPoint(_aimDirection) - _rigidbody.position;
                }

                _angle = Mathf.Atan2(_aimDirection.y, _aimDirection.x) * Mathf.Rad2Deg - 90f;
                //_rigidbody.rotation = _angle;
                playerWeapon.transform.rotation = Quaternion.Euler(0, 0, _angle);

                playerSpriteTransform.rotation = Quaternion.AngleAxis(_angle, Vector3.forward);
            }
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
        _playerHealth.InflictDamage(demonicPactHealthLoss, false);
        _fireCooldownEndTimestamp = 0f;
    }


    // Upgrade: Healing Field 
    public void ExecuteHealingField_OnBlock()
    {
        GameObject healingField = Instantiate(healingFieldPrefab, transform.position, Quaternion.identity);
        Destroy(healingField, 1.5f);
    }


    // Upgrade: Phoenix
    public void ExecutePhoenix_OnPlayerDeath()
    {
        if (!_phoenixed)
        {
            _playerHealth.ResetHealth();
            _phoenixed = true;
        }
    }

    #endregion
}