using BehaviorTree;
using UnityEngine;

public class UpgradeShockwave : Upgrade
{
    public override string Name => "Shockwave";
    public override UpgradeIdentification UpgradeIdentification => UpgradeIdentification.Shockwave;
    public override UpgradeType UpgradeType => UpgradeType.Weapon;
    public override string FlavorText => "";
    public override string Description => "Damage enemies in radius around you";

    public override void OnAbility(PlayerController playerController, PlayerWeapon playerWeapon)
    {
        UpgradeSpawnablePrefabHolder.SpawnPrefab(UpgradeSpawnablePrefabHolder.instance.shockwavePrefab, playerController.transform.position, Configuration.Shockwave_Duration);

        // Get all colliders of enemies around the player
        Collider2D[] results = Physics2D.OverlapCircleAll(playerController.transform.position, Configuration.Shockwave_Range, LayerMask.GetMask("Enemies"));

        for (int i = 0; i < results.Length; i++)
        {
            // If a wall is between the player and the enemy, ignore it
            Vector2 playertoEnemy = results[i].transform.position - playerController.transform.position;
            if (Physics2D.Raycast(playerController.transform.position, playertoEnemy, playertoEnemy.magnitude, LayerMask.GetMask("Walls")))
            {
                break;
            }

            // Inverse of playerToEnemy.magnitude to lessen shockwave strength for far away enemies
            float throwStrength = Mathf.Clamp((1f / playertoEnemy.magnitude) * Configuration.Shockwave_MaxStrength, Configuration.Shockwave_MinStrength, Configuration.Shockwave_MaxStrength);
            if (results[i].gameObject.GetComponent<ICharacterController>().ThrowCharacter())
                results[i].attachedRigidbody.AddForce(playertoEnemy.normalized * throwStrength);
        }

        EventManager.OnShockwave.Trigger();
    }
}