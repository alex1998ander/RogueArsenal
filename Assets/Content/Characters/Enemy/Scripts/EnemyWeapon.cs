using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] private GameObject enemyBulletPrefab;
    [SerializeField] private Transform firePoint;

    [SerializeField] private float defaultDistance = 20f;
    [SerializeField] private float defaultDamage = 35f;

    [SerializeField] private int bulletCount = 1;
    [SerializeField] private float weaponSpray = 0f;

    public void Fire()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            GameObject bullet = Instantiate(enemyBulletPrefab, firePoint.position, firePoint.rotation);
            bullet.transform.Rotate(Vector3.forward, Random.Range(-weaponSpray, weaponSpray));
            bullet.GetComponent<EnemyBullet>().Init(defaultDamage, defaultDistance, transform.parent.gameObject);
        }
    }
}