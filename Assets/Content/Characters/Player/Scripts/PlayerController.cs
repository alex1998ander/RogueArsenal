using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IUpgradeablePlayer, ICharacterController
{
    private PlayerInput _playerInput;
    private Rigidbody2D _rigidbody;
    private PlayerHealth _playerHealth;

    private static float _maxHealth = 100f;
    private static float _defaultDamage = 30f;
    private static float _moveSpeed = 5f;
    private float dashSpeed = 15f;
    private float dashTime = 0.2f;
    private float dashDelay = 0.1f;
    private float defaultFireDelay = 0.2f;
    private float defaultAbilityDelay = 5.0f;
    private float stimpackDuration = 5f;
    private float stimpackDamageMultiplier = 2f;
    private float currentStimpackDamageMultiplier = 1f;

    [SerializeField] private PlayerWeapon playerWeapon;

    [SerializeField] private Transform playerSpriteTransform;

    private Vector2 _movementInput;
    private Vector2 _dashMovementDirection;
    private Vector3 _aimDirection;
    private float _angle;
    private bool _isFiring;
    private bool _isDashing;
    private bool _canDash = true;

    private float _fireCooldownEndTimestamp;
    private float _abilityCooldownEndTimestamp;
    private float _dashEndTimestamp;
    private float _dashDelayEndTimestamp;

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
        UpgradeManager.PlayerUpdate(this);
        MovePlayer();
        FireWeapon();
    }

    private void MovePlayer()
    {
        float moveSpeed;
        Vector2 movementDirection;
        if (_isDashing)
        {
            moveSpeed = dashSpeed;
            movementDirection = _dashMovementDirection;

            if (Time.time > _dashEndTimestamp)
            {
                _isDashing = false;
                _playerHealth.SetInvulnerable(false);
                _dashDelayEndTimestamp = Time.time + dashDelay;
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
        if (!_isDashing && _isFiring && !GameManager.GamePaused && Time.time > _fireCooldownEndTimestamp)
        {
            // This assignment has to be done before "UpgradeManager.OnFire()" so that the variable can be overwritten by this function if necessary
            _fireCooldownEndTimestamp = Time.time + defaultFireDelay * UpgradeManager.GetFireDelayMultiplier();

            if (playerWeapon.TryFire(true))
            {
                UpgradeManager.OnFire(this);
                EventManager.OnPlayerShotFired.Trigger();
            }
        }
    }

    public ICharacterHealth GetHealthManager()
    {
        return _playerHealth;
    }

    public static float GetMaxHealth()
    {
        return Mathf.RoundToInt((_maxHealth + UpgradeManager.MaxHealthIncrease.Value) *
                                UpgradeManager.GetHealthMultiplier());
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
            _abilityCooldownEndTimestamp = Time.time + defaultAbilityDelay * UpgradeManager.GetAbilityDelayMultiplier();

            UpgradeManager.OnAbility(this);
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

                _angle = Mathf.Atan2(_aimDirection.y, _aimDirection.x) * Mathf.Rad2Deg - 90f;
                playerWeapon.transform.rotation = Quaternion.Euler(0, 0, _angle);

                playerSpriteTransform.rotation = Quaternion.AngleAxis(_angle, Vector3.forward);
            }
        }
    }

    private void OnReload()
    {
        playerWeapon.Reload();
    }

    private void OnDash()
    {
        if (_canDash && !_isDashing && Time.time > _dashDelayEndTimestamp)
        {
            _isDashing = true;
            _dashEndTimestamp = Time.time + dashTime;
            _playerHealth.SetInvulnerable(true);
            _dashMovementDirection = _movementInput;
        }
    }

    #endregion

    #region Upgrade implementation
    
    /// <summary>
    /// Default coroutine for a start and end action that are delayed by a certain time
    /// </summary>
    /// <param name="actionOn">start action</param>
    /// <param name="actionOff">end action</param>
    /// <param name="delayInSec">delay in seconds</param>
    /// <returns></returns>
    private IEnumerator OnOffCoroutine(Action actionOn, Action actionOff, float delayInSec)
    {
        actionOn?.Invoke();
        yield return new WaitForSeconds(delayInSec);
        actionOff?.Invoke();
    }

    // Upgrade: Tank
    public void InitTank()
    {
        _canDash = false;
    }

    // Upgrade: Burst
    public void ExecuteBurst_OnFire()
    {
        StartCoroutine(BurstCoroutine());
    }

    private IEnumerator BurstCoroutine()
    {
        float quarterDelay = defaultFireDelay * UpgradeManager.GetFireDelayMultiplier() * 0.25f;
        for (int i = 0; i < 2; i++)
        {
            yield return new WaitForSeconds(quarterDelay);
            playerWeapon.TryFire(false);
        }
    }

    // Upgrade: Demonic Pact 
    public void ExecuteDemonicPact_OnFire()
    {
        _playerHealth.InflictDamage(demonicPactHealthLoss, false);
        _fireCooldownEndTimestamp = 0f;
    }


    // Upgrade: Healing Field 
    public void ExecuteHealingField_OnAbility()
    {
        GameObject healingField = Instantiate(healingFieldPrefab, transform.position, Quaternion.identity);
        StartCoroutine(OnOffCoroutine(null, () => Destroy(healingField), 1.5f));
    }

    // Upgrade: Stimpack
    public void ExecuteStimpack_OnAbility()
    {
        StartCoroutine(OnOffCoroutine(() => {}, () => {}, stimpackDuration));
    }

    // Upgrade: Stimpack
    public void ExecuteTimefreeze_OnAbility()
    {
        StartCoroutine(OnOffCoroutine(() => {}, () => {}, stimpackDuration));
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