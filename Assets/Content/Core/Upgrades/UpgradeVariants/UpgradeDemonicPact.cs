public class UpgradeDemonicPact : Upgrade
{
    public override string Name => "Demonic Pact";
    public override string Description => "Embrace the dark arts of bullet wizardry and trade a bit of your life essence for instant trigger happiness.";
    public override string HelpfulDescription => "Shooting costs 10HP\nRemoves shooting cooldown";

    public override void OnFire(PlayerController playerController, PlayerWeapon playerWeapon)
    {
        // playerController.playerHealth.InflictDamage(demonicPactHealthLoss, false);
        // _fireCooldownEndTimestamp = 0f;
    }
}