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

    /// <summary>
    /// Tries to fire the player weapon
    /// </summary>
    /// <param name="spendAmmo">Spend ammo on shot</param>
    /// <param name="useTransformUp">Use the up direction of the weapon as fire direction</param>
    /// <param name="fireDirectionOverwrite">Overwritten fire direction</param>
    /// <returns>true if weapon could be fired, false if not</returns>
    public bool TryFire(bool spendAmmo, bool useTransformUp = true, Vector2 fireDirectionOverwrite = default)
    {
        if (spendAmmo && _currentAmmo <= 0)
            return false;

        Vector2 fireDirection = useTransformUp ? transform.up : fireDirectionOverwrite;
        Fire(fireDirection);

        if (spendAmmo)
            _currentAmmo--;

        Debug.Log("<color=yellow> Ammo: " + _currentAmmo + "</color>");

        return true;
    }

    private void Fire(Vector2 fireDirection)
    {
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
    }

    public void Reload()
    {
        _currentAmmo = Mathf.RoundToInt(Configuration.Weapon_MagazineSize * UpgradeManager.GetMagazineSizeMultiplier());
    }
}