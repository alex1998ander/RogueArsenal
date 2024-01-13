public class UpgradeGlassCannon : Upgrade
{
    public override string Name => "Glass Cannon";
    public override UpgradeIdentification UpgradeIdentification => UpgradeIdentification.GlassCannon;
    public override UpgradeType UpgradeType => UpgradeType.Weapon;
    public override string FlavorText => "Deal devastating damage to your enemies, but be warned: A mere sneeze could knock you out.";
    public override string Description => "Deal massive damage, but be warned:\nA sneeze knocks you out.";

    public override float BulletDamage => 1f;
    public override float Health => -0.6f;
}