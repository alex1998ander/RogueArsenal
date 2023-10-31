﻿using UnityEngine;

public class UpgradeBigBullet : Upgrade
{
    public override string Name => "Big Bullet";
    public override string Description => "Because size matters, watch as your bullets look big and intimidating. Who needs modesty when you can have an ego as big as a cannonball?";
    public override string HelpfulDescription => "Bigger bullets\n\nBullet Size +100%";

    public override float BulletSize => 4f;
    public override float BulletDamage => 1f;
    public override float MagazineSize => -0.7f;
    public override float ReloadTime => 2f;
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