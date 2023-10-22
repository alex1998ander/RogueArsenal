public class UpgradeStickyFingers : Upgrade
{
    public override string Name => "Sticky Fingers";
    public override string Description => "";
    public override string HelpfulDescription => "";

    public override float FireDelay => -0.5f;
    public override float WeaponSpray => 2f;

    public override void OnFire(PlayerController playerController, PlayerWeapon playerWeapon)
    {
        playerController.StickyFingers = true;
    }

    public override void OnReload(PlayerController playerController, PlayerWeapon playerWeapon)
    {
        playerController.StickyFingers = false;
    }
}