public class UpgradeCarefulPlanning : Upgrade
{
    public override string Name => "Careful Planning";
    public override UpgradeIdentification UpgradeIdentification => UpgradeIdentification.CarefulPlanning;
    public override UpgradeType UpgradeType => UpgradeType.Weapon;
    public override string FlavorText => "Embrace the spirit of meticulous plotting, trading rapid-fire chaos for jaw-dropping destruction.";
    public override string Description => "Trade rapid-fire chaos for jaw-dropping destruction.";

    public override float BulletDamage => 1f;
    public override float FireCooldown => 2f;
    public override float WeaponSpray => -0.5f;
}