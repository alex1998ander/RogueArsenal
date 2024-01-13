public class UpgradeMinigun : Upgrade
{
    public override string Name => "Minigun";
    public override UpgradeIdentification UpgradeIdentification => UpgradeIdentification.Minigun;
    public override UpgradeType UpgradeType => UpgradeType.Weapon;
    public override string FlavorText => "";
    public override string Description => "Did someone say \"More bullets\"?";

    public override float BulletDamage => -0.8f;
    public override float BulletSize => -0.4f;
    public override float FireCooldown => -0.9f;
    public override float MagazineSize => 9f;
    public override float ReloadTime => 2f;
    public override float WeaponSpray => 3f;
}