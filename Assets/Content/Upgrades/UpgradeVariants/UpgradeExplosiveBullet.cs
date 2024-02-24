using System;
using UnityEngine;

public class UpgradeExplosiveBullet : Upgrade
{
    public override string Name => "Explosive Bullet";

    public override UpgradeIdentification UpgradeIdentification => UpgradeIdentification.ExplosiveBullet;
    public override UpgradeType UpgradeType => UpgradeType.Weapon;
    public override string FlavorText => "Arm yourself with these explosive delights, turning your bullets into cheeky troublemakers that go 'boom' upon impact.";
    public override string Description => "Arm yourself with these explosive delights.";

    public override float FireCooldown => 1f;
    public override float MagazineSize => -0.2f;

    private readonly LayerMask _targetLayer = LayerMask.GetMask("Player_Trigger", "Enemy_Trigger");

    public override void OnBulletDestroy(PlayerBullet playerBullet)
    {
        _SpawnExplosion(playerBullet);
    }

    private void _SpawnExplosion(PlayerBullet playerBullet)
    {
        float bulletDamageMultiplierBase = Mathf.InverseLerp(Configuration.ExplosiveBullet_BulletDamageBaseMin, Configuration.ExplosiveBullet_BulletDamageBaseMax, playerBullet.Damage);
        float explosionDamage = Mathf.Lerp(Configuration.ExplosiveBullet_MinDamage, Configuration.ExplosiveBullet_MaxDamage, bulletDamageMultiplierBase);
        float explosionScale = Mathf.Lerp(Configuration.ExplosiveBullet_MinSize, Configuration.ExplosiveBullet_MaxSize, bulletDamageMultiplierBase);

        Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(playerBullet.transform.position, Configuration.ExplosiveBullet_Radius * explosionScale, _targetLayer);
        foreach (Collider2D targetCollider in rangeCheck)
        {
            ICharacterHealth characterHealth = targetCollider.GetComponentInParent<ICharacterHealth>();
            if (characterHealth is PlayerHealth)
                characterHealth.InflictDamage(explosionDamage * Configuration.Player_SelfDamageMultiplier);
            else
                characterHealth.InflictDamage(explosionDamage);
        }

        GameObject explosion = UpgradeSpawnablePrefabHolder.SpawnPrefab(UpgradeSpawnablePrefabHolder.instance.explosiveBullet, playerBullet.transform.position, 1f);
        explosion.transform.localScale = new Vector3(explosionScale, explosionScale, explosionScale);
        EventManager.OnExplosiveBulletExplosion.Trigger();
    }
}