﻿public class UpgradeBigBullet : Upgrade
{
    public override string Name => "Big Bullet";
    public override UpgradeIdentification UpgradeIdentification => UpgradeIdentification.BigBullet;
    public override UpgradeType UpgradeType => UpgradeType.Weapon;
    public override string FlavorText => "Because size matters, watch as your bullets look big and intimidating. Who needs modesty when you can have an ego as big as a cannonball?";
    public override string Description => "Let your bullet look intimidating.";

    public override float BulletSize => 4f;
    public override float BulletDamage => 1f;
    public override float MagazineSize => -0.5f;
    public override float ReloadTime => 0.5f;
    public override float FireCooldown => 1.5f;
    public override float BulletSpeed => -0.5f;
}