using UnityEngine;

public class UpgradeBounce : Upgrade    
{
    public override string Name => "Bounce";
    public override string Description => "Inject your bullets with enthusiasm, turning your attacks into a lively pinball game.";
    public override string HelpfulDescription => "Bullets bounce off of walls\n\nBullet Damage +25%";

    public override float BulletDamage => 0.25f;

    public override void Init(IUpgradeableBullet upgradeableBullet)
    {
        upgradeableBullet.InitBounce();
    }

    public override bool OnBulletImpact(IUpgradeableBullet upgradeableBullet, Collision2D collision)
    {
        return upgradeableBullet.ExecuteBounce_OnBulletImpact(collision);
    }
}