﻿public class UpgradeBuckshot : Upgrade
{
    public override string Name => "Buckshot";
    public override UpgradeIdentification UpgradeIdentification => UpgradeIdentification.Buckshot;
    public override UpgradeType UpgradeType => UpgradeType.Weapon;
    public override string FlavorText => "Unleash a shotgun-inspired impact that scatters enemies like confetti.";
    public override string Description => "Adds a shotgun vibe to your weapon.";

    public override int BulletCount => 4;
    public override float FireCooldown => 2.0f;
    public override float ReloadTime => 0.8f;
    public override float MagazineSize => -0.6f;
    public override float WeaponSpray => 4f;
    public override float BulletRange => -0.5f;
}