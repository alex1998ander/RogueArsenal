using System.Collections;
using UnityEngine;

public class UpgradeSplitShot : Upgrade
{
    public override string Name => "Split Shot";
    public override string HelpfulDescription => "";

    public override void OnFire(PlayerBullet playerBullet)
    {
        playerBullet.StartCoroutine(DelayedSplit(playerBullet));
    }

    private IEnumerator DelayedSplit(PlayerBullet playerBullet)
    {
        yield return new WaitForSeconds(Configuration.SplitShot_Delay);

        Transform bulletTransform = playerBullet.transform;
        GameObject splitBullet = Object.Instantiate(playerBullet.gameObject, bulletTransform.position, Quaternion.Euler(0f, 0f, Configuration.SplitShot_HalfAngle) * bulletTransform.rotation);

        splitBullet.GetComponent<PlayerBullet>().Init(PlayerController.GetBulletDamage() * Configuration.SplitShot_DamageMultiplierAfterwards);
        playerBullet.Damage *= Configuration.SplitShot_DamageMultiplierAfterwards;

        playerBullet.Rigidbody.velocity = Quaternion.Euler(0f, 0f, -Configuration.SplitShot_HalfAngle) * playerBullet.Rigidbody.velocity;
        playerBullet.AdjustFacingMovementDirection();

        Object.Destroy(splitBullet, playerBullet.Lifetime - Configuration.SplitShot_Delay);
    }
}