using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, ICharacterController
{
    [SerializeField] private PlayerWeapon playerWeapon;
    [SerializeField] private Transform playerSpriteTransform;

    public PlayerHealth PlayerHealth { get; private set; }


    // General Properties
    public bool CanDash { get; set; } = true;
    public bool CanReload { get; set; } = true;

    // Upgrade: Phoenix
    public bool Phoenixed { get; set; }

    // Upgrade: Sticky Fingers
    public bool StickyFingers { get; set; }

    private Rigidbody2D _rigidbody;

    private Vector2 _movementInput;
    private Vector2 _dashMovementDirection;
    private Vector2 _aimDirection;
    private float _angle;
    private bool _isFiring;
    private bool _isDashing;

    private float _fireCooldownEndTimestamp;
    private float _abilityCooldownEndTimestamp;
    private float _dashEndTimestamp;
    private float _dashDelayEndTimestamp;
    private float _weaponReloadedTimeStamp;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        PlayerHealth = GetComponent<PlayerHealth>();

        PlayerHealth.ResetHealth();

        UpgradeManager.Init(this);
    }

    void FixedUpdate()
    {
        UpgradeManager.PlayerUpdate(this);
        MovePlayer();
        FireWeapon();
    }

    #region PlayerActions

    private void MovePlayer()
    {
        float moveSpeed;
        Vector2 movementDirection;
        if (_isDashing)
        {
            moveSpeed = Configuration.Player_DashSpeed;
            movementDirection = _dashMovementDirection;

            if (Time.time > _dashEndTimestamp)
            {
                _isDashing = false;
                PlayerHealth.SetInvulnerable(false);
                _dashDelayEndTimestamp = Time.time + Configuration.Player_DashCoolDown;
            }
        }
        else
        {
            moveSpeed = GetPlayerMovementSpeed();
            movementDirection = _movementInput;
        }

        _rigidbody.MovePosition(_rigidbody.position + movementDirection * (moveSpeed * Time.fixedDeltaTime));
    }

    private void FireWeapon()
    {
        if (Time.time <= _fireCooldownEndTimestamp || _isDashing || GameManager.GamePaused)
            return;

        if (!CheckWeaponReloaded())
            return;

        if (_isFiring || StickyFingers)
        {
            // This assignment has to be done before "UpgradeManager.OnFire()" so that the variable can be overwritten by this function if necessary
            _fireCooldownEndTimestamp = Time.time + Configuration.Player_FireCoolDown * UpgradeManager.GetFireCooldownMultiplier();

            if (playerWeapon.TryFire(_aimDirection, true))
            {
                UpgradeManager.OnFire(this, playerWeapon, _aimDirection);
                EventManager.OnPlayerShotFired.Trigger();
            }
            else
            {
                UpgradeManager.OnMagazineEmptied(this, playerWeapon);
            }
        }
    }

    private void ReloadWeapon()
    {
        if (!CanReload)
            return;

        _weaponReloadedTimeStamp = Time.time + Configuration.Weapon_ReloadTime * UpgradeManager.GetReloadTimeMultiplier();
        playerWeapon.Reload();
        UpgradeManager.OnReload(this, playerWeapon);
    }

    private bool CheckWeaponReloaded()
    {
        if (Time.time > _weaponReloadedTimeStamp)
        {
            return true;
        }

        return false;
    }

    #endregion

    #region Getters

    public static float GetMaxHealth()
    {
        return Mathf.RoundToInt((Configuration.Player_MaxHealth + UpgradeManager.MaxHealthIncrease.Value) *
                                UpgradeManager.GetHealthMultiplier());
    }

    public static float GetBulletDamage()
    {
        return (Configuration.Weapon_Damage + UpgradeManager.BulletDamageIncrease.Value) *
               UpgradeManager.GetBulletDamageMultiplier();
    }

    public static float GetPlayerMovementSpeed()
    {
        return (Configuration.Player_MovementSpeed + UpgradeManager.PlayerMovementSpeedIncrease.Value) *
               UpgradeManager.GetPlayerMovementSpeedMultiplier();
    }

    #endregion

    #region UpgradeImplementation

    public void StunCharacter()
    {
        throw new NotImplementedException();
    }

    #endregion

    #region PlayerInput

    private void OnMove(InputValue value)
    {
        if (!GameManager.GamePaused)
            _movementInput = value.Get<Vector2>();
        else
            _movementInput = Vector2.zero;
    }

    private void OnFire(InputValue value)
    {
        _isFiring = value.isPressed;
    }

    private void OnAbility()
    {
        if (!GameManager.GamePaused && Time.time > _abilityCooldownEndTimestamp)
        {
            // This assignment has to be done before "UpgradeManager.OnAbility()" so that the variable can be overwritten by this function if necessary
            _abilityCooldownEndTimestamp = Time.time + Configuration.Player_AbilityCoolDown * UpgradeManager.GetAbilityDelayMultiplier();

            UpgradeManager.OnAbility(this, playerWeapon);
        }
    }

    private void OnAim(InputValue value)
    {
        if (!GameManager.GamePaused)
        {
            _aimDirection = value.Get<Vector2>();
            if (Vector2.Distance(Vector2.zero, _aimDirection) > 0.5)
            {
                _aimDirection = (Vector2) Camera.main.ScreenToWorldPoint(_aimDirection) - _rigidbody.position;
                _aimDirection.Normalize();

                _angle = Mathf.Atan2(_aimDirection.y, _aimDirection.x) * Mathf.Rad2Deg - 90f;
                playerWeapon.transform.rotation = Quaternion.Euler(0, 0, _angle);

                playerSpriteTransform.rotation = Quaternion.AngleAxis(_angle, Vector3.forward);
            }
        }
    }

    private void OnReload()
    {
        ReloadWeapon();
    }

    private void OnDash()
    {
        if (CanDash && !_isDashing && Time.time > _dashDelayEndTimestamp)
        {
            _isDashing = true;
            _dashEndTimestamp = Time.time + Configuration.Player_DashTime;
            PlayerHealth.SetInvulnerable(true);
            _dashMovementDirection = _movementInput;
        }
    }

    #endregion

    #region Sandbox

    public void InitUpgrades()
    {
        UpgradeManager.Init(this);
    }

    #endregion
}