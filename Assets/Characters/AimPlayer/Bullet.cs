using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IUpgradableBullet
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        UpgradeManager.OnBulletImpact(this);
        Destroy(gameObject);
    }

    private void Update() {
        UpgradeManager.BulletUpdate(this);
    }

    public void ExecuteBounce_BulletUpdate() {
        throw new NotImplementedException();
    }

    public void ExecuteTargetTracer_BulletUpdate() {
        throw new NotImplementedException();
    }

    public void ExecuteExplosiveBullet_OnBulletImpact() {
        throw new NotImplementedException();
    }
}