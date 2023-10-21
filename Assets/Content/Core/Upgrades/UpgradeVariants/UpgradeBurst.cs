using System.Collections;
using UnityEngine;

public class UpgradeBurst : Upgrade
{
    public override string Name => "Burst";
    public override string Description => "Trade the single-shot snooze for a burst of pew-pew-pew and turn your enemies into a walking target.";
    public override string HelpfulDescription => "Multiple bullets are fired in a sequence\n\nBullet Damage -60%\nFire Delay +100%";

    public override float BulletDamage => -0.3f;
    public override float MagazineSize => -0.3f;
    public override float FireDelay => 0.8f;

    public override void OnFire(PlayerController playerController, PlayerWeapon playerWeapon)
    {
        playerController.StartCoroutine(BurstCoroutine(playerWeapon));
    }

    private IEnumerator BurstCoroutine(PlayerWeapon playerWeapon)
    {
        float quarterDelay = Configuration.Player_FireCoolDown * UpgradeManager.GetFireDelayMultiplier() * 0.25f;
        for (int i = 0; i < 2; i++)
        {
            yield return new WaitForSeconds(quarterDelay);
            playerWeapon.TryFire(false);
        }
    }
}