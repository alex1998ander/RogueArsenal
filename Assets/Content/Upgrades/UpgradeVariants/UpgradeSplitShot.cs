using System.Collections;
using UnityEngine;

public class UpgradeSplitShot : Upgrade
{
    public override string Name => "Split Shot";
    public override UpgradeIdentification UpgradeIdentification => UpgradeIdentification.SplitShot;
    public override UpgradeType UpgradeType => UpgradeType.Weapon;
    public override string Description => "Turn every pull of the trigger into a triple-play spectacle.";

    public override float BulletDamage => -0.2f;
    public override float FireCooldown => 0.3f;


    public override void OnFire(PlayerBullet playerBullet)
    {
        playerBullet.StartCoroutine(DelayedSplit(playerBullet));
    }

    private IEnumerator DelayedSplit(PlayerBullet playerBullet)
    {
        yield return new WaitForSeconds(Configuration.SplitShot_Delay);

        // Prevent triggering 'OnDestroy' upgrades on this bullet (e.g. when has 'ExplosiveBullets', don't explode on splitting)
        // Do this AFTER the delay so the original bullet triggers 'OnDestroy' upgrades if it gets destroyed beforehand
        playerBullet.TriggerUpgradesOnDestroy = false;

        PlayerBullet leftBullet = CopyBullet(playerBullet, Configuration.SplitShot_HalfAngle);
        PlayerBullet rightBullet = CopyBullet(playerBullet, -Configuration.SplitShot_HalfAngle);
        PlayerBullet middleBullet = CopyBullet(playerBullet, 0);

        // For Sinusoidal Shots: rotate the split bullets coming from the left bullet to the right and vice versa
        if (playerBullet.RotationMultiplier != 0)
        {
            RotateBulletVelocity(leftBullet, playerBullet.RotationMultiplier * Configuration.SinusoidalShots_SplitShotHalfAngleAdjustment);
            RotateBulletVelocity(rightBullet, playerBullet.RotationMultiplier * Configuration.SinusoidalShots_SplitShotHalfAngleAdjustment);
        }

        // For Sinusoidal Shots: Split bullets need opposing rotation multipliers
        leftBullet.RotationMultiplier = 1;
        rightBullet.RotationMultiplier = -1;
        middleBullet.RotationMultiplier = 1;

        Object.Destroy(playerBullet.gameObject);
    }

    private PlayerBullet CopyBullet(PlayerBullet originalBullet, float velocityRotationAngle)
    {
        // Instantiate a new bullet at the location of the original bullet
        Transform originalBulletTransform = originalBullet.transform;
        GameObject copiedBulletObject = Object.Instantiate(originalBullet.gameObject, originalBulletTransform.position, originalBulletTransform.rotation);

        // Set damage multiplier
        PlayerBullet copiedBullet = copiedBulletObject.GetComponent<PlayerBullet>();
        copiedBullet.Init(PlayerController.GetBulletDamage() * Configuration.SplitShot_DamageMultiplierAfterwards);

        // In testing, origin and copied bullets don't start at the same position unless you manually equalize their rigidbody positions
        copiedBullet.Rigidbody.position = originalBullet.Rigidbody.position;

        // Rotate the velocity of the copied bullet
        copiedBullet.Rigidbody.velocity = originalBullet.Rigidbody.velocity;
        RotateBulletVelocity(copiedBullet, velocityRotationAngle);

        // Destroy copied bullet with total lifetime adjustment
        Object.Destroy(copiedBulletObject, originalBullet.TotalLifetime - Configuration.SplitShot_Delay);

        return copiedBullet;
    }

    private void RotateBulletVelocity(PlayerBullet bullet, float velocityRotationAngle)
    {
        bullet.Rigidbody.velocity = Quaternion.Euler(0f, 0f, velocityRotationAngle) * bullet.Rigidbody.velocity;
        bullet.AdjustFacingMovementDirection();
    }
}