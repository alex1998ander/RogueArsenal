public class UpgradeHoming : Upgrade
{
    public override string Name => "Homing";
    public override string Description => "Give your bullets a crash course in stalking 101, turning them into slightly creepy projectiles that relentlessly pursue visible targets.";
    public override string HelpfulDescription => "Bullets home towards visible targets\n\nBullet Damage -25%\nFire Delay +50%";

    public override float BulletDamage => -0.25f;
    public override float FireDelay => 0.5f;

    public override void BulletUpdate(IUpgradeableBullet upgradeableBullet)
    {
        upgradeableBullet.ExecuteHoming_BulletUpdate();
    }
}