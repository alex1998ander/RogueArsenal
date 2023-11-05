using BehaviorTree;
using UnityEngine;

public class UpgradeShockwave : Upgrade
{
    public override string Name => "Shockwave";
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

            if (results[i].gameObject.GetComponent<EnemyBehaviourTree>().ThrowCharacter())
                results[i].attachedRigidbody.AddForce(playertoEnemy * 1800f);
        }
    }
}