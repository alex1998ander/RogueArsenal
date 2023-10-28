public class UpgradeTank : Upgrade
{
    public override string Name => "Tank";
    public override string Description => "Roar into battle as the ferocious Tankasaurus, impervious to damage and ready to stomp through enemy lines.";
    public override string HelpfulDescription => "Health +100%\nFire Delay +100%";

    public override float Health => 1f;
    public override float FireCooldown => 1f;

    public override void Init(PlayerController playerController)
    {
        playerController.CanDash = false;
    }
}