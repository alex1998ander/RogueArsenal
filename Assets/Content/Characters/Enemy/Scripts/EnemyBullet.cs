using System;
using System.Collections;
using System.Collections.Generic;
using Content.Characters.Enemy.Scripts;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float defaultBulletSpeed = 10f;
    [SerializeField] private GameObject bounceBullet;

    private Rigidbody2D _rb;

    private float _assignedDamage;
    private float _assignedDistance;

    private int _bouncesLeft;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _rb.velocity = transform.up * defaultBulletSpeed;
        Destroy(gameObject, _assignedDistance / defaultBulletSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Player hit
        if (other.CompareTag("Player"))
        {
            if (UpgradeShield.IsShieldActive)
            {
                GameObject bullet = GameObject.Instantiate(bounceBullet, transform.position + new Vector3(2, 0, 0), transform.rotation);
                float angle = Mathf.Atan2(_rb.velocity.y, _rb.velocity.x) * Mathf.Rad2Deg;
                bullet.transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
                bullet.GetComponent<EnemyBounceBullet>().Init(_assignedDamage, Configuration.Boss_360ShotBulletDistance, transform.transform.gameObject);
            }
            else
            {
                other.GetComponentInParent<PlayerHealth>()?.InflictDamage(_assignedDamage, true);
                EventManager.OnPlayerHit.Trigger();
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
        EventManager.OnEnemyBulletDestroyed.Trigger();
    }

    /// <summary>
    /// Initializes this bullet.
    /// </summary>
    /// <param name="assignedDamage">Amount of damage caused by this bullet.</param>
    /// <param name="sourceCharacter">Reference of the character who shot this bullet.</param>
    public void Init(float assignedDamage, float assignedDistance, GameObject sourceCharacter)
    {
        _assignedDamage = assignedDamage;
        _assignedDistance = assignedDistance;
    }
}