using UnityEngine;

public class UpgradeHealingField : Upgrade
{
    public override string Name => "Healing Field";
    public override UpgradeIdentification UpgradeIdentification => UpgradeIdentification.HealingField;
    public override UpgradeType UpgradeType => UpgradeType.Ability;
    public override string FlavorText => "Transform the battlefield into a spa-like oasis of mending with a field of rejuvenation that magically patches up your injuries.";
    public override string Description => "Right click creates a healing field";

    public override float Health => 0.3f;

    public override void OnAbility(PlayerController playerController, PlayerWeapon playerWeapon)
    {
        UpgradeSpawnablePrefabHolder.SpawnPrefab(UpgradeSpawnablePrefabHolder.instance.healingFieldPrefab, playerController.transform.position, Configuration.HealingField_Duration);
    }
}