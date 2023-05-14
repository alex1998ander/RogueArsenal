using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;

    public GameObject bulletPrefab;

    public float bulletForce = 20f;

    private InputActions _ia;

    private void Awake()
    {
        _ia = new InputActions();
        _ia.Player.Shoot.performed += ctx =>
        {
            Debug.Log("shoot");
            Shoot();
        };
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }

    private void OnEnable()
    {
        _ia.Enable();
    }

    private void OnDisable()
    {
        _ia.Disable();
    }
}