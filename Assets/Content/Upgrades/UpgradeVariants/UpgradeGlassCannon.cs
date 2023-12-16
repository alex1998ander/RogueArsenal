public class UpgradeGlassCannon : Upgrade
{
    public override string Name => "Glass Cannon";
    public override UpgradeIdentification UpgradeIdentification => UpgradeIdentification.GlassCannon;
    public override UpgradeType UpgradeType => UpgradeType.Weapon;
    public override string FlavorText => "Deal devastating damage to your enemies, but be warned: A mere sneeze could knock you out.";
    public override string Description => "Bullet Damage +300%\nHealth -95%";

    public override float BulletDamage => 3f;
    public override float Health => -0.95f;
}