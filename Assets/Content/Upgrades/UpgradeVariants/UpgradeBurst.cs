using System.Collections;
using UnityEngine;

public class UpgradeBurst : Upgrade
{
    public override string Name => "Burst";
    public override UpgradeIdentification UpgradeIdentification => UpgradeIdentification.Burst;
    public override UpgradeType UpgradeType => UpgradeType.Weapon;
    public override string FlavorText => "Trade the single-shot snooze for a burst of pew-pew-pew and turn your enemies into a walking target.";
    public override string Description => "Multiple bullets are fired in a sequence\n\nBullet Damage -60%\nFire Delay +100%";

    public override float BulletDamage => -0.3f;
    public override float MagazineSize => -0.3f;
    public override float FireCooldown => 0.8f;

    public override void OnFire(PlayerController playerController, PlayerWeapon playerWeapon, Vector2 fireDirectionOverwrite = default)
    {
        playerController.StartCoroutine(BurstCoroutine(playerWeapon, fireDirectionOverwrite));
    }

    private IEnumerator BurstCoroutine(PlayerWeapon playerWeapon, Vector2 fireDirectionOverwrite)
    {
        float delayFraction = Configuration.Player_FireCoolDown * UpgradeManager.GetFireCooldownMultiplier() * Configuration.Burst_FireDelayFraction;
        for (int i = 0; i < Configuration.Burst_AdditionalBulletCount; i++)
        {
            yield return new WaitForSeconds(delayFraction);
            if (fireDirectionOverwrite != Vector2.zero)
                playerWeapon.TryFire(false, false, fireDirectionOverwrite);
            else
                playerWeapon.TryFire(false);

            EventManager.OnPlayerShot.Trigger();
        }
    }
}