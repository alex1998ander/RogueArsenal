using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    private const int DefaultBulletCount = 1;
    
    [SerializeField] private Transform bulletPrefab;
    [SerializeField] private Transform firePoint;

    [SerializeField] private float fireForce = 20f;

    public void Fire() {
        
        int bulletCount = DefaultBulletCount + UpgradeManager.GetBulletCountAdjustment();
        
        for (int i = 0; i < bulletCount; i++) {
            Transform bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.localRotation *= Quaternion.Euler(i * 10, i * 10, i * 10);
            bullet.GetComponent<Rigidbody2D>().velocity = (Vector2) firePoint.up * fireForce;
        }
        
    }
}