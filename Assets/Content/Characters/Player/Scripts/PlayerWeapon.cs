using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private GameObject playerBulletPrefab;
    [SerializeField] private Transform firePoint;

    private int _currentAmmo;

    private void Start()
    {
        Reload();
    }

    public bool TryFire(bool spendAmmo)
    {
        if (spendAmmo && _currentAmmo <= 0)
            return false;

        float angle = Configuration.Weapon_SprayAngle * 0.5f * UpgradeManager.GetWeaponSprayMultiplier();
        int bulletCount = Configuration.Weapon_BulletCount + UpgradeManager.GetBulletCountAdjustment();
        for (int i = 0; i < bulletCount; i++)
        {
            GameObject bullet = Instantiate(playerBulletPrefab, firePoint.position,
                firePoint.rotation * Quaternion.Euler(0, 0, Random.Range(-angle, angle)));
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