public class UpgradeCarefulPlanning : Upgrade
{
    public override string Name => "Careful Planning";
    public override UpgradeIdentification UpgradeIdentification => UpgradeIdentification.CarefulPlanning;
    public override UpgradeType UpgradeType => UpgradeType.Weapon;
    public override string FlavorText => "Embrace the spirit of meticulous plotting, trading rapid-fire chaos for jaw-dropping destruction.";
    public override string Description => "More bullet damage but also longer reload";

    public override float BulletDamage => 1.5f;
    public override float FireCooldown => 2f;
}