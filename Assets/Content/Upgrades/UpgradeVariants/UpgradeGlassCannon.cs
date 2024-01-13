public class UpgradeGlassCannon : Upgrade
{
    public override string Name => "Glass Cannon";
    public override UpgradeIdentification UpgradeIdentification => UpgradeIdentification.GlassCannon;
    public override UpgradeType UpgradeType => UpgradeType.Weapon;
    public override string FlavorText => "Deal devastating damage to your enemies, but be warned: A mere sneeze could knock you out.";
    public override string Description => "Massively increased damage but health is also massively decreased";

    public override float BulletDamage => 1f;
    public override float Health => -0.6f;
}