public class UpgradeTank : Upgrade
{
    public override string Name => "Tank";
    public override UpgradeIdentification UpgradeIdentification => UpgradeIdentification.Tank;
    public override UpgradeType UpgradeType => UpgradeType.Weapon;
    public override string FlavorText => "Roar into battle as the ferocious Tankasaurus, impervious to damage and ready to stomp through enemy lines.";
    public override string Description => "Roar into battle as the ferocious Tankasaurus.";

    public override float Health => 0.6f;
    public override float PlayerMovementSpeed => -0.08f;
    public override float FireCooldown => 0.4f;
}