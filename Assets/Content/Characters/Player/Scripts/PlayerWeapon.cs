using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    private const int DefaultBulletCount = 1;
    private const float DefaultBulletSpreadAngle = 2f;

    [SerializeField] private GameObject playerBulletPrefab;
    [SerializeField] private Transform firePoint;

    [SerializeField] private float defaultDamage = 35f;

    public void Fire()
    {
        int bulletCount = DefaultBulletCount + UpgradeManager.GetBulletCountAdjustment();

        for (int i = 0; i < bulletCount; i++)
        {
            GameObject bullet = Instantiate(playerBulletPrefab, firePoint.position, firePoint.rotation * Quaternion.Euler(0,  0,  (i - (bulletCount - 1) / 2.0f) * DefaultBulletSpreadAngle));
            bullet.GetComponent<PlayerBullet>().Init(defaultDamage * UpgradeManager.GetBulletDamageMultiplier(), transform.parent.gameObject);
            bullet.transform.localScale *= UpgradeManager.GetBulletSizeMultiplier();
        }
        
        EventManager.OnPlayerFire.Trigger();
    }
}