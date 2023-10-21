public class UpgradeStimpack : Upgrade
{
    public override string Name => "Stimpack";
    public override string HelpfulDescription => "";

    public override void OnAbility(IUpgradeablePlayer upgradeablePlayer)
    {
        upgradeablePlayer.ExecuteStimpack_OnAbility();
    }
}