using UnityEngine;

public class UpgradeBounce : Upgrade
{
    public override string Name => "Bounce";
    public override UpgradeIdentification UpgradeIdentification => UpgradeIdentification.Bounce;
    public override UpgradeType UpgradeType => UpgradeType.Weapon;
    public override string FlavorText => "Inject your bullets with enthusiasm, turning your attacks into a lively pinball game.";
    public override string Description => "Turn your attacks into a pinball game.";

    public override float BulletDamage => 0.25f;

    public override float BulletSpeed => -0.5f;
    public override float BulletRange => 1f;

    public override void Init(PlayerBullet playerBullet)
    {
        playerBullet.Rigidbody.sharedMaterial = UpgradeSpawnablePrefabHolder.instance.bulletBouncePhysicsMaterial;
        playerBullet.BouncesLeft = Configuration.Bounce_BounceCount;
    }

    public override bool OnBulletCollision(PlayerBullet playerBullet, Collision2D collision)
    {
        if (playerBullet.BouncesLeft <= 0)
        {
            return false;
        }

        playerBullet.AdjustFacingMovementDirection();
        playerBullet.BouncesLeft--;

        EventManager.OnBulletBounce.Trigger();

        return true;
    }
}