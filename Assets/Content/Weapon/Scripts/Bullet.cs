using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IUpgradeableBullet
{
    [SerializeField] private PhysicsMaterial2D bulletBouncePhysicsMaterial;
    [SerializeField] private int maxBounces = 3;
    [SerializeField] private float defaultLifetime = 0.5f;

    private float _assignedDamage;
    private GameObject _sourceCharacter;
    private bool _playerBullet;

    private bool _currentlyColliding = false;
    private int _bouncesLeft;

    private void Awake()
    {
        _bouncesLeft = maxBounces;
        UpgradeManager.Init(this);
        Destroy(gameObject,defaultLifetime * UpgradeManager.GetBulletRangeMultiplier());
    }

    private void FixedUpdate()
    {
        UpgradeManager.BulletUpdate(this);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!_currentlyColliding && !UpgradeManager.OnBulletImpact(this, other))
        {
            Destroy(gameObject);
        }

        //TODO: tag enemy
        // Enemy hit
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyHealth>().InflictDamage(_assignedDamage);
        }
        // Player hit
        else if (other.gameObject.CompareTag("Player"))
        {
            if (_playerBullet)
            {
                // Player hits themself
                other.gameObject.GetComponent<PlayerHealth>().InflictDamage(_assignedDamage, _sourceCharacter.GetComponent<PlayerController>());
            }
            else
            {
                // Player hits enemy
                other.gameObject.GetComponent<PlayerHealth>().InflictDamage(_assignedDamage, _sourceCharacter.GetComponent<EnemyController>());
            }
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
    /// <param name="playerBullet">Indicates whether the bullet was shot from the player or from the enemy.</param>
    public void Init(float assignedDamage, GameObject sourceCharacter, bool playerBullet)
    {
        _assignedDamage = assignedDamage;
        _sourceCharacter = sourceCharacter;
        _playerBullet = playerBullet;
    }

    public void InitBounce()
    {
        GetComponent<Rigidbody2D>().sharedMaterial = bulletBouncePhysicsMaterial;
    }

    public void ExecuteTargetTracer_BulletUpdate()
    {
        throw new NotImplementedException();
    }

    public bool ExecuteBounce_OnBulletImpact(Collision2D collision)
    {
        if (_bouncesLeft > 0)
        {
            _bouncesLeft--;
            return true;
        }

        return false;
    }

    public bool ExecuteExplosiveBullet_OnBulletImpact(Collision2D collision)
    {
        throw new NotImplementedException();
    }
}