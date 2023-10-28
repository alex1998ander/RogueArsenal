using UnityEngine;

public class UpgradeStickyFingers : Upgrade
{
    public override string Name => "Sticky Fingers";
    public override string Description => "";
    public override string HelpfulDescription => "";

    public override float FireCooldown => -0.5f;
    public override float WeaponSpray => 2f;

    public override void OnFire(PlayerController playerController, PlayerWeapon playerWeapon, Vector2 fireDirection)
    {
        playerController.StickyFingers = true;
        playerController.CanDash = false;
        playerController.CanReload = false;
    }

    public override void OnMagazineEmptied(PlayerController playerController, PlayerWeapon playerWeapon)
    {
        playerController.StickyFingers = false;
        playerController.CanDash = true;
        playerController.CanReload = true;
    }
}