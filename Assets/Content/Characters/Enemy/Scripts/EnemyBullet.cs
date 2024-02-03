using System;
using System.Collections;
using System.Collections.Generic;
using Content.Characters.Enemy.Scripts;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private GameObject bounceBullet;

    private Rigidbody2D _rb;

    private float _assignedDamage;
    private float _assignedDistance;
    private float _assignedSpeed;

    private int _bouncesLeft;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Player hit
        if (other.CompareTag("Player"))
        {
            if (PlayerData.ShieldActive)
            {
                Quaternion invertedRotation = transform.rotation * Quaternion.AngleAxis(180, Vector3.forward);
                GameObject bullet = GameObject.Instantiate(bounceBullet, transform.position, invertedRotation); //Quaternion.Inverse(transform.rotation)  alternative abbounce Richtung
                bullet.GetComponent<EnemyBounceBullet>().Init(_assignedDamage, _assignedDistance, transform.transform.gameObject);
                Destroy(gameObject);
            }
            else if (!PlayerData.IsDashing)
            {
                other.GetComponentInParent<PlayerHealth>()?.InflictDamage(_assignedDamage, true);
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
    public void Init(float assignedDamage, float assignedDistance, float assignedSpeed, GameObject sourceCharacter)
    {
        _assignedDamage = assignedDamage;
        _assignedDistance = assignedDistance;
        _assignedSpeed = assignedSpeed;
        _rb.velocity = transform.up * assignedSpeed;
        Destroy(gameObject, _assignedDistance / assignedSpeed);
    }
}