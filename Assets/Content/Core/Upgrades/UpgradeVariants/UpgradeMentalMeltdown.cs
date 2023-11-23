using UnityEngine;

public class UpgradeMentalMeltdown : Upgrade
{
    public override string Name => "Mental Meltdown";
    public override string Description => "Your bullets possess the power to crash your enemies' brains, leaving them searching for a Ctrl+Alt+Delete button to reboot their shattered thoughts.";
    public override string HelpfulDescription => "Bullets stun the opponent";

    public override bool OnBulletTrigger(PlayerBullet playerBullet, Collider2D other)
    {
        ICharacterController characterController = other.gameObject.GetComponent<ICharacterController>();
        if (characterController != null)
        {
            if (characterController.StunCharacter())
                UpgradeSpawnablePrefabHolder.SpawnPrefab(UpgradeSpawnablePrefabHolder.instance.mentalMeltdownPrefab, other.gameObject.transform.position, Configuration.Enemy_StunTime, other.gameObject);
        }

        return false;
    }
}