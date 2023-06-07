using UnityEngine;

public interface IUpgradeableBullet {
    public void InitBounce();
    
    public void ExecuteTargetTracer_BulletUpdate();

    public bool ExecuteBounce_OnBulletImpact(Collision2D collision);
    public bool ExecuteExplosiveBullet_OnBulletImpact(Collision2D collision);
}