using System.Collections;
using UnityEngine;

public class UpgradePhoenix : Upgrade
{
    public override string Name => "Phoenix";
    public override UpgradeIdentification UpgradeIdentification => UpgradeIdentification.Phoenix;
    public override UpgradeType UpgradeType => UpgradeType.Passive;
    public override string FlavorText => "Rise from the ashes with the power of a phoenix and turn your defeat into a glorious opportunity that ignite your comeback.";
    public override string Description => "Respawn once on death\n\nHealth -35%";

    public override float Health => -0.35f;


    public override void OnPlayerDeath(PlayerController playerController)
    {
        if (!PlayerData.phoenixed)
        {
            UpgradeSpawnablePrefabHolder.SpawnPrefab(UpgradeSpawnablePrefabHolder.instance.phoenixPrefab,
                playerController.transform.position,
                Configuration.Phoenix_WarmUpTime + Configuration.Phoenix_InvincibilityTime);

            PlayerData.health = PlayerData.maxHealth;
            EventManager.OnPlayerHealthUpdate.Trigger(PlayerData.health);

            PlayerData.phoenixed = true;
            EventManager.OnPlayerPhoenixed.Trigger();
        }
    }
}