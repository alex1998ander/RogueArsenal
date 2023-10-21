using UnityEngine;

public class UpgradeExplosiveBullet : Upgrade
{
    public override string Name => "Explosive Bullet";
    public override string Description => "Arm yourself with these explosive delights, turning your bullets into cheeky troublemakers that go 'boom' upon impact.";
    public override string HelpfulDescription => "Bullet explodes on impact\n\nFire Delay +100%";

    public override float FireDelay => 1f;

    public override bool OnBulletImpact(IUpgradeableBullet upgradeableBullet, Collision2D collision)
    {
        return upgradeableBullet.ExecuteExplosiveBullet_OnBulletImpact(collision);
    }
}