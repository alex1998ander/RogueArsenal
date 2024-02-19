using UnityEngine;

public class UpgradeMentalMeltdown : Upgrade
{
    public override string Name => "Mental Meltdown";
    public override UpgradeIdentification UpgradeIdentification => UpgradeIdentification.MentalMeltdown;
    public override UpgradeType UpgradeType => UpgradeType.Weapon;
    public override string FlavorText => "Your bullets possess the power to crash your enemies' brains, leaving them searching for a Ctrl+Alt+Delete button to reboot their shattered thoughts.";
    public override string Description => "Stuns enemies for a short period of time";

    public override bool OnBulletTrigger(PlayerBullet playerBullet, Collider2D other)
    {
        ICharacterController characterController = other.gameObject.GetComponentInParent<ICharacterController>();
        if (characterController != null)
        {
            if (characterController.StunCharacter())
                UpgradeSpawnablePrefabHolder.SpawnPrefab(UpgradeSpawnablePrefabHolder.instance.mentalMeltdownPrefab, other.gameObject.transform.position, Configuration.Enemy_StunTime, other.gameObject);
        }

        return false;
    }
}