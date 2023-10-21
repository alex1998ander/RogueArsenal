public class UpgradeHealingField : Upgrade
{
    public override string Name => "Healing Field";
    public override string Description => "Transform the battlefield into a spa-like oasis of mending with a field of rejuvenation that magically patches up your injuries.";
    public override string HelpfulDescription => "Right click creates a healing field\n\nHealth +30%";

    public override float Health => 0.3f;

    public override void OnAbility(IUpgradeablePlayer upgradeablePlayer)
    {
        upgradeablePlayer.ExecuteHealingField_OnAbility();
    }
}