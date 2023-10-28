public class UpgradeTimefreeze : Upgrade
{
    public override string Name => "Timefreeze";
    public override string HelpfulDescription => "";

    public override void OnAbility(PlayerController playerController)
    {
        playerController.StartCoroutine(Util.OnOffCoroutine(
            () => TimeController.ChangeTimeScale(Configuration.Timefreeze_TimeScale),
            TimeController.ResetTimeScale,
            Configuration.Timefreeze_Duration / Configuration.Timefreeze_TimeScale)
        );
    }
}