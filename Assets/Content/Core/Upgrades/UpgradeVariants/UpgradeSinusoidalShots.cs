using UnityEngine;

public class UpgradeSinusoidalShots : Upgrade
{
    public override string Name => "Sinusoidal Shots";
    public override string Description => "Straight lines are so passé! Unleash the power of trigonometry on your foes and watch them tremble in confusion.";
    public override string HelpfulDescription => "";

    public override int BulletCount => 1;
    public override float BulletDamage => -0.4f;
    public override float BulletSpeed => -0.5f;
    public override float BulletRange => 0.5f;

    private int _rotationMultiplier = 1;

    public override void OnFire(PlayerBullet playerBullet)
    {
        playerBullet.RotationMultiplier = _rotationMultiplier;
        _rotationMultiplier *= -1;
    }

    public override void BulletUpdate(PlayerBullet playerBullet)
    {
        // To be honest I don't know what I'm doing here but it works so whatever
        float rotationAngle = playerBullet.RotationMultiplier * Mathf.Cos(playerBullet.Lifetime * Configuration.SinusoidalShots_Frequency) * Configuration.SinusoidalShots_Amplitude;
        Vector3 rotatedVelocity = Quaternion.Euler(0f, 0f, rotationAngle) * playerBullet.Rigidbody.velocity;
        playerBullet.Rigidbody.velocity = rotatedVelocity;
        playerBullet.AdjustFacingMovementDirection();
    }
}