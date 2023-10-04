using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] private GameObject enemyBulletPrefab;
    [SerializeField] private Transform firePoint;

    [SerializeField] private float defaultDistance = 20f;
    [SerializeField] private float defaultDamage = 35f;

    public void Fire()
    {
        GameObject bullet = Instantiate(enemyBulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<EnemyBullet>().Init(defaultDamage, defaultDistance, transform.parent.gameObject);
    }
}