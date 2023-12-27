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

    private readonly LayerMask _targetLayer = LayerMask.GetMask("Player", "Enemies");

    public override bool OnBulletTrigger(PlayerBullet playerBullet, Collider2D other)
    {
        _SpawnExplosion(playerBullet);
        return false;
    }

    public override bool OnBulletCollision(PlayerBullet playerBullet, Collision2D collision)
    {
        _SpawnExplosion(playerBullet);
        return false;
    }

    private void _SpawnExplosion(PlayerBullet playerBullet)
    {
        Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(playerBullet.transform.position, Configuration.ExplosiveBullet_Radius, _targetLayer);
        foreach (Collider2D targetCollider in rangeCheck)
        {
            targetCollider.GetComponent<ICharacterHealth>().InflictDamage(Configuration.ExplosiveBullet_Damage);
        }

        UpgradeSpawnablePrefabHolder.SpawnPrefab(UpgradeSpawnablePrefabHolder.instance.explosiveBullet, playerBullet.transform.position, 1f);
    }
}