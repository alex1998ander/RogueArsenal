using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUpgradeable {
    
    public void ExecuteBurst_OnFire();
    public void ExecuteHealingField_OnBlock();
    
    public void ExecuteBounce_BulletUpdate();
    public void ExecuteTargetTracer_BulletUpdate();
    
    public void ExecuteExplosiveBullet_OnBulletImpact();
    
    public void ExecutePhoenix_OnPlayerDeath();
}
