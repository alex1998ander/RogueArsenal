using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private GameObject playerBulletPrefab;

    private int _currentAmmo;

    private void Start()
    {
        Reload();
    }

    public bool TryFire(Vector2 fireDirection, bool spendAmmo)
    {
        if (spendAmmo && _currentAmmo <= 0)
            return false;

        // Calculate the angle of rotation based on shootDirection
        float angle = Mathf.Atan2(fireDirection.y, fireDirection.x) * Mathf.Rad2Deg - 90f;

        // Calculate the random angle range for weapon spraying
        float randomAngleRange = Configuration.Weapon_SprayAngle * 0.5f * UpgradeManager.GetWeaponSprayMultiplier();

        int bulletCount = Configuration.Weapon_BulletCount + UpgradeManager.GetBulletCountAdjustment();

        Vector3 firePoint = transform.position + (Vector3) fireDirection * Configuration.Weapon_BulletSpawnDistance;
        for (int i = 0; i < bulletCount; i++)
        {
            GameObject bullet = Instantiate(playerBulletPrefab, firePoint, Quaternion.Euler(0, 0, angle + Random.Range(-randomAngleRange, randomAngleRange)));
            bullet.transform.localScale *= UpgradeManager.GetBulletSizeMultiplier();

            PlayerBullet playerBullet = bullet.GetComponent<PlayerBullet>();
            playerBullet.Init(PlayerController.GetBulletDamage());
            UpgradeManager.OnFire(playerBullet);
        }

        if (spendAmmo)
            _currentAmmo--;

        return true;
    }

    public void Reload()
    {
        _currentAmmo = Mathf.RoundToInt(Configuration.Weapon_MagazineSize * UpgradeManager.GetMagazineSizeMultiplier());
    }
}