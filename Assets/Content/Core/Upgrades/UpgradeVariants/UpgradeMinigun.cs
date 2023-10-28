public class UpgradeMinigun : Upgrade
{
    public override string Name => "Minigun";
    public override string Description => "";
    public override string HelpfulDescription => "";

    public override float BulletDamage => -0.8f;
    public override float FireCooldown => -0.9f;
    public override float MagazineSize => 4f;
    public override float WeaponSpray => 3f;
}