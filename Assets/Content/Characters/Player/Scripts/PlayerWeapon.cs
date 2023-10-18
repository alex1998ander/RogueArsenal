using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private GameObject playerBulletPrefab;
    [SerializeField] private Transform firePoint;

    private const int DefaultBulletCount = 1;
    private const float DefaultBulletSprayAngle = 5f;
    private const int DefaultMagazineSize = 10;
    private const float DefaultReloadTime = 1f;

    private int _currentAmmo = DefaultMagazineSize;
    private bool _reloading;
    private float _weaponReloadedTimeStamp;

    public bool TryFire(bool spendAmmo)
    {
        if (spendAmmo && _currentAmmo <= 0 || (_reloading && !CheckReloaded()))
            return false;

        float angle = DefaultBulletSprayAngle * 0.5f * UpgradeManager.GetWeaponSprayMultiplier();
        int bulletCount = DefaultBulletCount + UpgradeManager.GetBulletCountAdjustment();
        for (int i = 0; i < bulletCount; i++)
        {
            GameObject bullet = Instantiate(playerBulletPrefab, firePoint.position,
                firePoint.rotation * Quaternion.Euler(0, 0, Random.Range(-angle, angle)));
            bullet.GetComponent<PlayerBullet>().Init(PlayerController.GetBulletDamage(), transform.parent.gameObject);
            bullet.transform.localScale *= UpgradeManager.GetBulletSizeMultiplier();
        }

        if (spendAmmo)
            _currentAmmo--;

        return true;
    }

    public void Reload()
    {
        if (_reloading)
            return;

        _reloading = true;
        _weaponReloadedTimeStamp = Time.time + DefaultReloadTime * UpgradeManager.GetReloadTimeMultiplier();
        _currentAmmo = (int) (DefaultMagazineSize * UpgradeManager.GetMagazineSizeMultiplier());
    }

    private bool CheckReloaded()
    {
        if (Time.time > _weaponReloadedTimeStamp)
        {
            _reloading = false;
            return true;
        }

        return false;
    }
}