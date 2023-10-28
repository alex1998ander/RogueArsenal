public class UpgradeCarefulPlanning : Upgrade
{
    public override string Name => "Careful Planning";
    public override string Description => "Embrace the spirit of meticulous plotting, trading rapid-fire chaos for jaw-dropping destruction.";
    public override string HelpfulDescription => "Bullet Damage +150%\nFire Delay +200%";

    public override float BulletDamage => 1.5f;
    public override float FireCooldown => 2f;
}