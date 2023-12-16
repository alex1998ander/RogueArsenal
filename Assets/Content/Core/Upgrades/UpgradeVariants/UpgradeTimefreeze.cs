public class UpgradeTimefreeze : Upgrade
{
    public override string Name => "Timefreeze";
    public override UpgradeIdentification UpgradeIdentification => UpgradeIdentification.Timefreeze;
    public override UpgradeType UpgradeType => UpgradeType.Ability;
    public override string Description => "";

    public override void OnAbility(PlayerController playerController, PlayerWeapon playerWeapon)
    {
        playerController.StartCoroutine(Util.OnOffCoroutine(
            () => TimeController.ChangeTimeScale(Configuration.Timefreeze_TimeScale),
            TimeController.ResetTimeScale,
            Configuration.Timefreeze_Duration / Configuration.Timefreeze_TimeScale)
        );
    }
}