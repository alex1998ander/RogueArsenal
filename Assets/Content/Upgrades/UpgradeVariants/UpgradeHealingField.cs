using UnityEngine;

public class UpgradeHealingField : Upgrade
{
    public override string Name => "Healing Field";
    public override UpgradeIdentification UpgradeIdentification => UpgradeIdentification.HealingField;
    public override UpgradeType UpgradeType => UpgradeType.Ability;
    public override string FlavorText => "Transform the battlefield into a spa-like oasis of mending with a field of rejuvenation that magically patches up your injuries.";
    public override string Description => "Patches up your injuries in this a spa-like oasis of mending.";

    public override float AbilityDelay => 0.6f;

    public override void OnAbility(PlayerController playerController, PlayerWeapon playerWeapon)
    {
        UpgradeSpawnablePrefabHolder.SpawnPrefab(UpgradeSpawnablePrefabHolder.instance.healingFieldPrefab, playerController.transform.position, Configuration.HealingField_Duration + 0.1f);
    }
}