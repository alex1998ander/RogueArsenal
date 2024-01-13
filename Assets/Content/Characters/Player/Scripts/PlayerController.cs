using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, ICharacterController
{
    [SerializeField] private Animator playerVisualsAnimator;
    [SerializeField] private PlayerWeapon playerWeapon;
    [SerializeField] private SpriteRenderer playerWeaponSprite;

    public PlayerHealth playerHealth;

    private Rigidbody2D _rigidbody;
    private Collider2D _collider;
    private Vector2 _movementInput;
    private Vector2 _dashMovementDirection;
    private Vector2 _aimDirection;
    private float _angle;
    private bool _isFiring;

    private float _fireCooldownEndTimestamp;
    private float _abilityCooldownEndTimestamp;
    private float _dashEndTimestamp;
    private float _dashDelayEndTimestamp;
    private float _weaponReloadedTimeStamp;

    private static readonly int MovementDirectionX = Animator.StringToHash("MovementDirectionX");
    private static readonly int MovementDirectionY = Animator.StringToHash("MovementDirectionY");
    private static readonly int AimDirectionX = Animator.StringToHash("AimDirectionX");
    private static readonly int AimDirectionY = Animator.StringToHash("AimDirectionY");
    private static readonly int RunningBackwards = Animator.StringToHash("RunningBackwards");

    public void Init_Sandbox()
    {
        Awake();
        playerWeapon.Init_Sandbox();
    }

    private void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();

        PlayerData.maxHealth = Mathf.RoundToInt(Configuration.Player_MaxHealth * UpgradeManager.GetHealthMultiplier());
        PlayerData.health = PlayerData.maxHealth;

        PlayerData.fireCooldown = Configuration.Player_FireCoolDown * UpgradeManager.GetFireCooldownMultiplier();
        PlayerData.abilityCooldown = Configuration.Player_AbilityCoolDown * UpgradeManager.GetAbilityDelayMultiplier();

        UpgradeManager.Init(this);

        EventManager.OnPhoenixRevive.Subscribe(OnPhoenixed);
    }

    void FixedUpdate()
    {
        UpgradeManager.PlayerUpdate(this);
        MovePlayer();
        FireWeapon();
    }

    private void OnDestroy()
    {
        EventManager.OnPhoenixRevive.Unsubscribe(OnPhoenixed);
    }

    #region PlayerActions

    private void MovePlayer()
    {
        float moveSpeed;
        Vector2 movementDirection;
        if (PlayerData.IsDashing)
        {
            moveSpeed = Configuration.Player_DashSpeed;
            movementDirection = _dashMovementDirection;

            if (Time.time > _dashEndTimestamp)
            {
                PlayerData.IsDashing = false;
                PlayerData.invulnerable = false;
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
        if (Time.time <= _fireCooldownEndTimestamp || PlayerData.IsDashing || GameManager.GamePlayFrozen)
            return;

        if (Time.time <= _weaponReloadedTimeStamp)
            return;

        if (_isFiring || PlayerData.stickyFingers)
        {
            // This assignment has to be done before "UpgradeManager.OnFire()" so that the variable can be overwritten by this function if necessary
            _fireCooldownEndTimestamp = Time.time + PlayerData.fireCooldown;

            if (playerWeapon.TryFire(true))
            {
                UpgradeManager.OnFire(this, playerWeapon);
                EventManager.OnPlayerShot.Trigger();

                // FIXME: This is a stupid place to do auto reloading. Too bad!
                if (PlayerData.ammo == 0)
                {
                    StartReloadWeapon();
                }
            }
            else
            {
                UpgradeManager.OnMagazineEmptied(this, playerWeapon);
                EventManager.OnPlayerShotEmpty.Trigger();
            }
        }
    }

    private void StartReloadWeapon()
    {
        if (!PlayerData.canReload || Time.time <= _weaponReloadedTimeStamp)
            return;

        _weaponReloadedTimeStamp = Time.time + PlayerData.reloadTime;
        playerWeapon.StartReload();
        UpgradeManager.OnReload(this, playerWeapon);
    }

    #endregion

    #region Getters

    [Obsolete]
    public static float GetMaxHealth()
    {
        return PlayerData.maxHealth;
    }

    public static float GetBulletDamage()
    {
        return Configuration.Weapon_Damage * UpgradeManager.GetBulletDamageMultiplier();
    }

    public static float GetPlayerMovementSpeed()
    {
        return Configuration.Player_MovementSpeed * UpgradeManager.GetPlayerMovementSpeedMultiplier();
    }

    #endregion

    #region UpgradeImplementation

    public bool StunCharacter()
    {
        throw new NotImplementedException();
    }

    public bool ThrowCharacter()
    {
        throw new NotImplementedException();
    }

    private void OnPhoenixed()
    {
        PlayerData.canMove = false;
        PlayerData.canDash = false;
        PlayerData.canFire = false;
        PlayerData.canReload = false;
        PlayerData.canUseAbility = false;

        PlayerData.invulnerable = true;

        StartCoroutine(AfterPhoenixed());
    }

    private IEnumerator AfterPhoenixed()
    {
        yield return new WaitForSeconds(Configuration.Phoenix_WarmUpTime);

        PlayerData.canMove = true;
        PlayerData.canDash = true;
        PlayerData.canFire = true;
        PlayerData.canReload = true;
        PlayerData.canUseAbility = true;

        yield return new WaitForSeconds(Configuration.Phoenix_InvincibilityTime);
        PlayerData.invulnerable = false;
    }

    #endregion

    #region PlayerInput

    private void OnMove(InputValue value)
    {
        if (PlayerData.canMove && !GameManager.GamePlayFrozen)
        {
            _movementInput = value.Get<Vector2>();
            playerVisualsAnimator.SetFloat(MovementDirectionX, _movementInput.x);
            playerVisualsAnimator.SetFloat(MovementDirectionY, _movementInput.y);

            // when movement and aim direction are at a 90 degree angle or greater, count as walking backwards
            playerVisualsAnimator.SetBool(RunningBackwards, Vector2.Angle(_movementInput, _aimDirection) > 95.0f);
        }
        else
        {
            _movementInput = Vector2.zero;
        }
    }

    private void OnFire(InputValue value)
    {
        _isFiring = PlayerData.canFire && value.isPressed;
    }

    private void OnAbility()
    {
        if (PlayerData.canUseAbility && !GameManager.GamePlayFrozen && Time.time > _abilityCooldownEndTimestamp)
        {
            // This assignment has to be done before "UpgradeManager.OnAbility()" so that the variable can be overwritten by this function if necessary
            _abilityCooldownEndTimestamp = Time.time + PlayerData.abilityCooldown;

            UpgradeManager.OnAbility(this, playerWeapon);
            EventManager.OnPlayerAbilityUsed.Trigger();
        }
    }

    private void OnAim(InputValue value)
    {
        if (PlayerData.canMove && !GameManager.GamePlayFrozen)
        {
            _aimDirection = value.Get<Vector2>();
            if (Vector2.Distance(Vector2.zero, _aimDirection) > 0.5)
            {
                _aimDirection = (Vector2) Camera.main.ScreenToWorldPoint(_aimDirection) - _rigidbody.position;
                _aimDirection.Normalize();

                _angle = Mathf.Atan2(_aimDirection.y, _aimDirection.x) * Mathf.Rad2Deg;
                playerWeapon.transform.rotation = Quaternion.Euler(0, 0, _angle);

                // When the player is aiming left, flip weapon so it's not heads-down
                playerWeaponSprite.flipY = _aimDirection.x < 0.0f;
                // When the player is aiming up, adjust sorting order so weapon is behind player
                playerWeaponSprite.sortingOrder = _angle >= 45.0 && _angle <= 135.0f ? -1 : 1;

                playerVisualsAnimator.SetFloat(AimDirectionX, _aimDirection.x);
                playerVisualsAnimator.SetFloat(AimDirectionY, _aimDirection.y);

                // when movement and aim direction are at a 90 degree angle or greater, count as walking backwards
                playerVisualsAnimator.SetBool(RunningBackwards, Vector2.Angle(_movementInput, _aimDirection) > 95.0f);
            }
        }
    }

    private void OnReload()
    {
        if (PlayerData.canReload)
            StartReloadWeapon();
    }

    private void OnDash()
    {
        if (PlayerData.canDash && !PlayerData.IsDashing && Time.time > _dashDelayEndTimestamp)
        {
            PlayerData.IsDashing = true;
            _dashEndTimestamp = Time.time + Configuration.Player_DashTime;
            PlayerData.invulnerable = true;
            _dashMovementDirection = _movementInput;
            EventManager.OnPlayerDash.Trigger();
        }
    }

    private void OnPause()
    {
        GameManager.TogglePause();
    }

    #endregion

    #region Sandbox

    public void InitUpgrades()
    {
        UpgradeManager.Init(this);

        PlayerData.maxHealth = Mathf.RoundToInt(Configuration.Player_MaxHealth * UpgradeManager.GetHealthMultiplier());
        PlayerData.health = PlayerData.maxHealth;

        PlayerData.fireCooldown = Configuration.Player_FireCoolDown * UpgradeManager.GetFireCooldownMultiplier();
        PlayerData.abilityCooldown = Configuration.Player_AbilityCoolDown * UpgradeManager.GetAbilityDelayMultiplier();

        PlayerData.maxAmmo = Mathf.RoundToInt(Configuration.Weapon_MagazineSize * UpgradeManager.GetMagazineSizeMultiplier());
        PlayerData.reloadTime = Configuration.Weapon_ReloadTime * UpgradeManager.GetReloadTimeMultiplier();
    }

    #endregion
}