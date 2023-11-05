using UnityEngine;

public class UpgradeSmartPistol : Upgrade
{
    public override string Name => "Smart Pistol";
    public override string Description => "";
    public override string HelpfulDescription => "";

    public override void OnAbility(PlayerController playerController, PlayerWeapon playerWeapon)
    {
        // Get all colliders of enemies around the player
        Collider2D[] results = Physics2D.OverlapCircleAll(playerController.transform.position, Configuration.SmartPistol_Range, LayerMask.GetMask("Enemies"));

        for (int i = 0; i < results.Length; i++)
        {
            // If a wall is between the player and the enemy, ignore it
            Vector2 playertoEnemy = results[i].transform.position - playerController.transform.position;
            if (Physics2D.Raycast(playerController.transform.position, playertoEnemy, playertoEnemy.magnitude, LayerMask.GetMask("Walls")))
            {
                break;
            }

            // Temporarily turn off sticky fingers so the player doesn't start emptying his magazine on ability use
            bool hasStickyFingers = playerController.StickyFingers;
            playerController.StickyFingers = false;

            // Check if the player can dash to reset it later (The first shot with sticky fingers will turn off dashing)
            bool canDash = playerController.CanDash;

            // Shoot in direction of the enemy
            playertoEnemy.Normalize();
            playerWeapon.TryFire(false, false, playertoEnemy);
            UpgradeManager.OnFire(playerController, playerWeapon, playertoEnemy);
            EventManager.OnPlayerShotFired.Trigger();

            // Turn sticky fingers and dashing on/off again
            playerController.StickyFingers = hasStickyFingers;
            playerController.CanDash = canDash;
        }
    }
}