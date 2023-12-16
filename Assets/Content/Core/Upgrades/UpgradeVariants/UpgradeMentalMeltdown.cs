using UnityEngine;

public class UpgradeMentalMeltdown : Upgrade
{
    public override string Name => "Mental Meltdown";
    public override UpgradeIdentification UpgradeIdentification => UpgradeIdentification.MentalMeltdown;
    public override UpgradeType UpgradeType => UpgradeType.Weapon;
    public override string FlavorText => "Your bullets possess the power to crash your enemies' brains, leaving them searching for a Ctrl+Alt+Delete button to reboot their shattered thoughts.";
    public override string Description => "Bullets stun the opponent";

    public override bool OnBulletTrigger(PlayerBullet playerBullet, Collider2D other)
    {
        other.gameObject.GetComponent<ICharacterController>()?.StunCharacter();
        return false;
    }
}