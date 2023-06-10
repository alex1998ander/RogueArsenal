using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    [SerializeField] private float fireForce = 20f;
    [SerializeField] private float defaultDamage = 35f;

    public void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Bullet>().Init(defaultDamage, transform.parent.gameObject, false);
        bullet.GetComponent<Rigidbody2D>().velocity = firePoint.up * fireForce;
    }
}