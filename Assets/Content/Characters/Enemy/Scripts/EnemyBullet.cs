using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float defaultBulletSpeed = 10f;
    
    private Rigidbody2D _rb;

    private float _assignedDamage;
    private float _assignedDistance;
    private GameObject _sourceCharacter;

    private bool _currentlyColliding = false;
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!_currentlyColliding)
        {
            Destroy(gameObject);
        }

        // Player hit
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealth>().InflictDamage(_assignedDamage, true);
        }

        _currentlyColliding = true;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        _currentlyColliding = false;
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
        _sourceCharacter = sourceCharacter;
    }
}