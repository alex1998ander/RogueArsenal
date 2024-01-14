using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private GameObject playerBulletPrefab;
    [SerializeField] private Transform firePointTransform;

    public void Init_Sandbox()
    {
        Awake();
    }

    private void Awake()
    {
        PlayerData.maxAmmo = Mathf.RoundToInt(Configuration.Weapon_MagazineSize * UpgradeManager.GetMagazineSizeMultiplier());
        PlayerData.reloadTime = Configuration.Weapon_ReloadTime * UpgradeManager.GetReloadTimeMultiplier();
        PlayerData.ammo = PlayerData.maxAmmo;
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
}