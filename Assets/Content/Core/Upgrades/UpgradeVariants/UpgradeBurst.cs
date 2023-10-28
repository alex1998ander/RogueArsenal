using System.Collections;
using UnityEngine;

public class UpgradeBurst : Upgrade
{
    public override string Name => "Burst";
    public override string Description => "Trade the single-shot snooze for a burst of pew-pew-pew and turn your enemies into a walking target.";
    public override string HelpfulDescription => "Multiple bullets are fired in a sequence\n\nBullet Damage -60%\nFire Delay +100%";

    public override float BulletDamage => -0.3f;
    public override float MagazineSize => -0.3f;
    public override float FireCooldown => 0.8f;

    public override void OnFire(PlayerController playerController, PlayerWeapon playerWeapon, Vector2 fireDirection)
    {
        playerController.StartCoroutine(BurstCoroutine(playerWeapon, fireDirection));
    }

    private IEnumerator BurstCoroutine(PlayerWeapon playerWeapon, Vector2 fireDirection)
    {
        float delayFraction = Configuration.Player_FireCoolDown * UpgradeManager.GetFireCooldownMultiplier() * Configuration.Burst_FireDelayFraction;
        for (int i = 0; i < Configuration.Burst_AdditionalBulletCount; i++)
        {
            yield return new WaitForSeconds(delayFraction);
            playerWeapon.TryFire(fireDirection, false);
        }
    }
}