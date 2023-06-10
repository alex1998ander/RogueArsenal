using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float defaultLifetime = 0.5f;

    private float _assignedDamage;
    private GameObject _sourceCharacter;

    private bool _currentlyColliding = false;
    private int _bouncesLeft;

    private void Awake()
    {
        Destroy(gameObject, defaultLifetime);
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
            other.gameObject.GetComponent<PlayerHealth>().InflictDamage(_assignedDamage, _sourceCharacter.GetComponent<PlayerController>());
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
    public void Init(float assignedDamage, GameObject sourceCharacter)
    {
        _assignedDamage = assignedDamage;
        _sourceCharacter = sourceCharacter;
    }
}