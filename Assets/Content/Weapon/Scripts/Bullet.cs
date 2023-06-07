using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IUpgradeableBullet {
    [SerializeField] private PhysicsMaterial2D bulletBouncePhysicsMaterial;
    [SerializeField] private int maxBounces = 3;

    private float _assignedDamage;
    private GameObject _sourceCharacter;

    private bool _currentlyColliding = false;
    private int _bouncesLeft;

    private void Awake() {
        _bouncesLeft = maxBounces;
        UpgradeManager.Init(this);
    }

    private void FixedUpdate() {
        UpgradeManager.BulletUpdate(this);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (!_currentlyColliding && UpgradeManager.OnBulletImpact(this, other)) {
            Destroy(gameObject);
        }

        //TODO: tag enemy
        // Enemy hit
        if (other.gameObject.CompareTag("Enemy")) {
            Damage damage = new(
                _assignedDamage,
                _sourceCharacter.GetComponent<PlayerController>(),
                other.gameObject.GetComponent<EnemyController>(),
                true
            );
            other.gameObject.GetComponent<EnemyHealth>().InflictDamage(damage);
        }
        // Player hit
        else if (other.gameObject.CompareTag("Player")) {
            Damage damage = new(
                _assignedDamage,
                other.gameObject.GetComponent<PlayerController>(),
                _sourceCharacter.GetComponent<EnemyController>(),
                false
            );
            other.gameObject.GetComponent<PlayerHealth>().InflictDamage(damage);
        }

        _currentlyColliding = true;
    }

    private void OnCollisionExit2D(Collision2D other) {
        _currentlyColliding = false;
    }

    /// <summary>
    /// Initializes this bullet.
    /// </summary>
    /// <param name="assignedDamage">Amount of damage caused by this bullet.</param>
    /// <param name="sourceCharacter">Reference of the character who shot this bullet.</param>
    public void Init(float assignedDamage, GameObject sourceCharacter) {
        _assignedDamage = assignedDamage;
        _sourceCharacter = sourceCharacter;
    }

    public void InitBounce() {
        GetComponent<Rigidbody2D>().sharedMaterial = bulletBouncePhysicsMaterial;
    }

    public void ExecuteTargetTracer_BulletUpdate() {
        throw new NotImplementedException();
    }

    public bool ExecuteBounce_OnBulletImpact(Collision2D collision) {
        if (_bouncesLeft > 0) {
            _bouncesLeft--;
            return false;
        }

        return true;
    }

    public bool ExecuteExplosiveBullet_OnBulletImpact(Collision2D collision) {
        throw new NotImplementedException();
    }
}