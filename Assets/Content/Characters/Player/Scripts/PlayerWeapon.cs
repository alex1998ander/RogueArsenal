using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private GameObject playerBulletPrefab;
    [SerializeField] private Transform firePoint;

    private void Awake()
    {
        PlayerData.maxAmmo = Mathf.RoundToInt(Configuration.Weapon_MagazineSize * UpgradeManager.GetMagazineSizeMultiplier());
        PlayerData.reloadTime = Configuration.Weapon_ReloadTime * UpgradeManager.GetReloadTimeMultiplier();
    }

    private void Start()
    {
        Reload();
    }

    public bool TryFire(bool spendAmmo)
    {
        if (spendAmmo && PlayerData.ammo <= 0)
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
            PlayerData.ammo--;

        return true;
    }

    public void Reload()
    {
        PlayerData.ammo = PlayerData.maxAmmo;
    }
}