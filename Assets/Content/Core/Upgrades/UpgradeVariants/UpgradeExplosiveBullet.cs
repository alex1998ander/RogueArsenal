using System;
using UnityEngine;

public class UpgradeExplosiveBullet : Upgrade
{
    public override string Name => "Explosive Bullet";

    public override UpgradeIdentification UpgradeIdentification => UpgradeIdentification.ExplosiveBullet;
    public override UpgradeType UpgradeType => UpgradeType.Weapon;
    public override string FlavorText => "Arm yourself with these explosive delights, turning your bullets into cheeky troublemakers that go 'boom' upon impact.";
    public override string Description => "Bullet explodes on impact\n\nFire Delay +100%";

    public override float FireCooldown => 1f;


    private LayerMask targetLayer = LayerMask.GetMask("Player", "Enemies");

    public override bool OnBulletTrigger(PlayerBullet playerBullet, Collider2D other)
    {
        Collider2D[] rangeCheck = Array.Empty<Collider2D>();
        Physics2D.OverlapCircleNonAlloc(playerBullet.transform.position, Configuration.ExplosiveBullet_Radius, rangeCheck, targetLayer);

        foreach (Collider2D targetCollider in rangeCheck)
        {
            targetCollider.GetComponent<ICharacterHealth>().InflictDamage(0);
        }

        return false;
    }
}