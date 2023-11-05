public class UpgradePhoenix : Upgrade
{
    public override string Name => "Phoenix";
    public override string Description => "Rise from the ashes with the power of a phoenix and turn your defeat into a glorious opportunity that ignite your comeback.";
    public override string HelpfulDescription => "Respawn once on death\n\nHealth -35%";

    public override float Health => -0.35f;

    public override void OnPlayerDeath(PlayerController playerController)
    {
        if (!PlayerData.phoenixed)
        {
            PlayerData.health = PlayerData.maxHealth;
            EventManager.OnPlayerHealthUpdate.Trigger();
            
            PlayerData.phoenixed = true;
            EventManager.OnPlayerPhoenixed.Trigger();
        }
    }
}