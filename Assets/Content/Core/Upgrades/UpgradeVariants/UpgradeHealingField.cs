using UnityEngine;

public class UpgradeHealingField : Upgrade
{
    public override string Name => "Healing Field";
    public override string Description => "Transform the battlefield into a spa-like oasis of mending with a field of rejuvenation that magically patches up your injuries.";
    public override string HelpfulDescription => "Right click creates a healing field\n\nHealth +30%";

    public override float Health => 0.3f;

    
    private readonly GameObject healingFieldPrefab = UpgradeSpawnablePrefabHolder.instance.healingFieldPrefab;

    public override void OnAbility(PlayerController playerController)
    {
        GameObject healingField = Object.Instantiate(healingFieldPrefab, playerController.transform.position, Quaternion.identity);
        Object.Destroy(healingField, Configuration.HealingField_Duration);
    }
}