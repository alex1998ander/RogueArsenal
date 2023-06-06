using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    private const int DefaultBulletCount = 1;
    private const float DefaultBulletSpreadAngle = 2f;

    [SerializeField] private Transform bulletPrefab;
    [SerializeField] private Transform firePoint;

    [SerializeField] private float fireForce = 20f;

    public void Fire() {
        int bulletCount = DefaultBulletCount + UpgradeManager.GetBulletCountAdjustment();

        for (int i = 0; i < bulletCount; i++) {
            Transform bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            //TODO: Correct parameters 
            bullet.GetComponent<Bullet>().Init(0, transform.parent.gameObject);
            bullet.GetComponent<Rigidbody2D>().velocity = (Vector2)(Quaternion.Euler(0, 0, (i - (bulletCount - 1) / 2.0f) * DefaultBulletSpreadAngle) * firePoint.up) * fireForce;
        }
    }
}