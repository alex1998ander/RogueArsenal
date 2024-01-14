using UnityEngine;

public class UpgradeSmartPistol : Upgrade
{
    public override string Name => "Smart Pistol";
    public override UpgradeIdentification UpgradeIdentification => UpgradeIdentification.SmartPistol;
    public override UpgradeType UpgradeType => UpgradeType.Ability;
    public override string FlavorText => "";
    public override string Description => "A pistol that skillfully shoots down all nearby enemies.";

    public override void OnAbility(PlayerController playerController, PlayerWeapon playerWeapon)
    {
        // Get all colliders of enemies around the player
        Collider2D[] results = Physics2D.OverlapCircleAll(playerController.transform.position, Configuration.SmartPistol_Range, LayerMask.GetMask("Enemy_Trigger"));

        for (int i = 0; i < results.Length; i++)
        {
            // If a wall is between the player and the enemy, ignore it
            Vector2 playertoEnemy = results[i].transform.position - playerController.transform.position;
            if (Physics2D.Raycast(playerController.transform.position, playertoEnemy, playertoEnemy.magnitude, LayerMask.GetMask("Walls")))
            {
                break;
            }

            // Temporarily turn off sticky fingers so the player doesn't start emptying his magazine on ability use
            bool hasStickyFingers = PlayerData.stickyFingers;
            PlayerData.stickyFingers = false;

            // Check if the player can dash to reset it later (The first shot with sticky fingers will turn off dashing)
            bool canDash = PlayerData.canDash;

            // Shoot in direction of the enemy
            playertoEnemy.Normalize();
            playerWeapon.TryFire(false, false, playertoEnemy);
            UpgradeManager.OnFire(playerController, playerWeapon, playertoEnemy);
            EventManager.OnPlayerShot.Trigger();

            // Turn sticky fingers and dashing on/off again
            PlayerData.stickyFingers = hasStickyFingers;
            PlayerData.canDash = canDash;
        }
    }
}