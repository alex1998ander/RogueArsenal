using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private GameObject playerBulletPrefab;
    [SerializeField] private Transform firePointTransform;
    [SerializeField] private SpriteRenderer muzzleFlashSprite;
    [SerializeField] private Animator muzzleFlashAnimator;
    [SerializeField] private SpriteRenderer weaponSprite;
    [SerializeField] private SpriteOrderer weaponSpriteOrderer;

    private Vector3 _defaultFirePointOffset;
    private Vector3 _defaultMuzzleFlashOffset;

    private static readonly int Shoot = Animator.StringToHash("Shoot");

    public void Init_Sandbox()
    {
        Awake();
    }

    private void Awake()
    {
        PlayerData.maxAmmo = Mathf.RoundToInt(Configuration.Weapon_MagazineSize * UpgradeManager.GetMagazineSizeMultiplier());
        PlayerData.reloadTime = Configuration.Weapon_ReloadTime * UpgradeManager.GetReloadTimeMultiplier();
        PlayerData.ammo = PlayerData.maxAmmo;

        _defaultFirePointOffset = firePointTransform.localPosition;
        _defaultMuzzleFlashOffset = muzzleFlashSprite.transform.localPosition;
    }

    private void FixedUpdate()
    {
        Vector3 newOffset = IsAimingLeft() ? new Vector3(_defaultFirePointOffset.x, -_defaultFirePointOffset.y, _defaultFirePointOffset.z) : _defaultFirePointOffset;
        firePointTransform.localPosition = newOffset;
    }

    private void Update()
    {
        bool aimingLeft = IsAimingLeft();

        // When the player is aiming left, flip weapon so it's not heads-down
        weaponSprite.flipY = aimingLeft;
        muzzleFlashSprite.flipY = aimingLeft;

        // when aiming left, flip the y offset of the muzzle flash to fit the flipped sprite
        Vector3 newOffset = aimingLeft ? new Vector3(_defaultMuzzleFlashOffset.x, -_defaultMuzzleFlashOffset.y, _defaultMuzzleFlashOffset.z) : _defaultMuzzleFlashOffset;
        muzzleFlashSprite.transform.localPosition = newOffset;

        // When the player is aiming up, adjust sorting order so weapon is behind player
        weaponSpriteOrderer.orderOffset = IsAimingUp() ? -32 : 0;
    }

    /// <summary>
    /// Tries to fire the player weapon
    /// </summary>
    /// <param name="spendAmmo">Spend ammo on shot</param>
    /// <param name="useFirePointTransform">Use the fire point transform of the weapon as fire point + direction</param>
    /// <param name="fireDirectionOverwrite">Overwritten fire direction</param>
    /// <returns>true if weapon could be fired, false if not</returns>
    public bool TryFire(bool spendAmmo, bool useFirePointTransform = true, Vector2 fireDirectionOverwrite = default)
    {
        if (spendAmmo && PlayerData.ammo <= 0)
            return false;

        Vector2 firePoint;
        Vector2 fireDirection;
        if (useFirePointTransform)
        {
            firePoint = firePointTransform.position;
            fireDirection = firePointTransform.right;
        }
        else
        {
            // If not using fire point, spawn bullets a certain distance from the player away, shoot at given direction
            firePoint = (Vector2) transform.root.position + fireDirectionOverwrite * Configuration.Weapon_BulletSpawnDistance;
            fireDirection = fireDirectionOverwrite;
        }

        Fire(firePoint, fireDirection);
        PlayMuzzleFlashEffects();

        if (spendAmmo)
        {
            PlayerData.ammo--;
            EventManager.OnPlayerAmmoUpdate.Trigger();
        }

        return true;
    }

    private void Fire(Vector2 firePoint, Vector2 fireDirection)
    {
        // Calculate the angle of rotation based on shootDirection
        float angle = Mathf.Atan2(fireDirection.y, fireDirection.x) * Mathf.Rad2Deg - 90f;

        // Calculate the random angle range for weapon spraying
        float randomAngleRange = Configuration.Weapon_SprayAngle * 0.5f * UpgradeManager.GetWeaponSprayMultiplier();

        int bulletCount = Configuration.Weapon_BulletCount + UpgradeManager.GetBulletCountAdjustment();

        for (int i = 0; i < bulletCount; i++)
        {
            GameObject bullet = Instantiate(playerBulletPrefab, firePoint, Quaternion.Euler(0, 0, angle + Random.Range(-randomAngleRange, randomAngleRange)));
            bullet.transform.localScale *= UpgradeManager.GetBulletSizeMultiplier();

            PlayerBullet playerBullet = bullet.GetComponent<PlayerBullet>();
            playerBullet.Init(PlayerController.GetBulletDamage());
            UpgradeManager.OnFire(playerBullet);
        }
    }

    public void StartReload()
    {
        EventManager.OnWeaponReloadStart.Trigger();
        StartCoroutine(_EndReload());
    }

    private IEnumerator _EndReload()
    {
        yield return new WaitForSeconds(PlayerData.reloadTime);
        PlayerData.ammo = PlayerData.maxAmmo;
        EventManager.OnWeaponReloadEnd.Trigger();
    }

    private void PlayMuzzleFlashEffects()
    {
        muzzleFlashAnimator.SetTrigger(Shoot);
    }

    private bool IsAimingLeft()
    {
        return transform.eulerAngles.z is >= 90f and <= 270f;
    }

    private bool IsAimingUp()
    {
        return transform.eulerAngles.z is >= 45f and <= 135f;
    }
}