public class UpgradeShield : Upgrade
{
    public override string Name => "Shield";
    public override UpgradeIdentification UpgradeIdentification => UpgradeIdentification.Shield;
    public override UpgradeType UpgradeType => UpgradeType.Ability;
    
    public override string FlavorText => "Rise from the ashes with the power of a phoenix and turn your defeat into a glorious opportunity that ignite your comeback.";
    public override string Description => "Shields you of damage";

    public static bool IsShieldActive { get; private set; } = false;

    public override void OnAbility(PlayerController playerController, PlayerWeapon playerWeapon)
    {
        playerController.StartCoroutine(Util.OnOffCoroutine(
            ActivateShield,
            DeactivateShield,
            Configuration.Shield_Duration)
        );
    }

    public void ActivateShield()
    {
        PlayerData.canDash = false;
        PlayerData.invulnerable = true;
        IsShieldActive = true;
    }

    public void DeactivateShield()
    {
        PlayerData.invulnerable = false;
        IsShieldActive = false;
        PlayerData.canDash = true;
    }
}