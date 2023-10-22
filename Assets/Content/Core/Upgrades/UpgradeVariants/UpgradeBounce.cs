using UnityEngine;

public class UpgradeBounce : Upgrade
{
    public override string Name => "Bounce";
    public override string Description => "Inject your bullets with enthusiasm, turning your attacks into a lively pinball game.";
    public override string HelpfulDescription => "Bullets bounce off of walls\n\nBullet Damage +25%";

    public override float BulletDamage => 0.25f;

    public override void Init(PlayerBullet playerBullet)
    {
        playerBullet.SetBouncyPhysicsMaterial();
    }

    public override bool OnBulletImpact(PlayerBullet playerBullet, Collider2D other)
    {
        if (playerBullet.BouncesLeft <= 0 || other.CompareTag("Enemy") || other.CompareTag("Player"))
        {
            return false;
        }

        playerBullet.BouncesLeft--;
        playerBullet.AdjustFacingMovementDirection();

        return true;
    }
}