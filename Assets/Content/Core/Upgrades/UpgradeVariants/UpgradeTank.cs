public class UpgradeTank : Upgrade
{
    public override string Name => "Tank";
    public override UpgradeIdentification UpgradeIdentification => UpgradeIdentification.Tank;
    public override UpgradeType UpgradeType => UpgradeType.Weapon;
    public override string FlavorText => "Roar into battle as the ferocious Tankasaurus, impervious to damage and ready to stomp through enemy lines.";
    public override string Description => "Health +100%\nFire Delay +100%";

    public override float Health => 1f;
    public override float FireCooldown => 1f;

    public override void Init(PlayerController playerController)
    {
        PlayerData.canDash = false;
    }
}