using UnityEngine;

public class UpgradeBigBullet : Upgrade
{
    public override string Name => "Big Bullet";
    public override UpgradeIdentification UpgradeIdentification => UpgradeIdentification.BigBullet;
    public override UpgradeType UpgradeType => UpgradeType.Weapon;
    public override string FlavorText => "Because size matters, watch as your bullets look big and intimidating. Who needs modesty when you can have an ego as big as a cannonball?";
    public override string Description => "Bullet size increased";

    public override float BulletSize => 4f;
    public override float BulletDamage => 1f;
    public override float MagazineSize => -0.6f;
    public override float ReloadTime => 1.6f;
    public override float FireCooldown => 2.5f;
    public override float BulletSpeed => -0.7f;

    public override void Init(PlayerBullet playerBullet)
    {
        playerBullet.PiercesLeft += Configuration.BigBullet_PiercesCount;
    }

    public override bool OnBulletTrigger(PlayerBullet playerBullet, Collider2D other)
    {
        if (playerBullet.PiercesLeft <= 0 || (!other.CompareTag("Enemy") && !other.CompareTag("Player")))
            return false;

        playerBullet.PiercesLeft--;
        return true;
    }
}