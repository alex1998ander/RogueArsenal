using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSinusoidalShots : Upgrade
{
    public override string Name => "Sinusoidal Shots";
    public override string Description => "Straight lines are so passé! Unleash the power of trigonometry on your foes and watch them tremble in confusion.";
    public override string HelpfulDescription => "";

    public override float BulletSpeed => -0.3f;
    public override float BulletRange => 1f;

    public override void OnFire(PlayerBullet playerBullet)
    {
        playerBullet.RotationMultiplier = 1;

        Transform bulletTransform = playerBullet.transform;
        GameObject splitBulletGameObject = Object.Instantiate(playerBullet.gameObject, bulletTransform.position, bulletTransform.rotation);
        PlayerBullet splitBullet = splitBulletGameObject.GetComponent<PlayerBullet>();
        splitBullet.GetComponent<PlayerBullet>().Init(PlayerController.GetBulletDamage() * 1f); //TODO? sinus shot damage multiplier
        splitBullet.GetComponent<PlayerBullet>().RotationMultiplier = -1;
    }

    public override void BulletUpdate(PlayerBullet playerBullet)
    {
        // To be honest I don't know what I'm doing here but it works so whatever
        float rotationAngle = playerBullet.RotationMultiplier * Mathf.Sin(playerBullet.Lifetime * Configuration.SinusoidalShots_Frequency + Configuration.SinusoidalShots_Frequency / 4f) * Configuration.SinusoidalShots_Amplitude;
        Vector3 rotatedVelocity = Quaternion.Euler(0f, 0f, rotationAngle) * playerBullet.Rigidbody.velocity;
        playerBullet.Rigidbody.velocity = rotatedVelocity;
    }
}