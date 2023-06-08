using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using BehaviorTree;

public class FollowingEnemyBT : BTree
{
    // Layer mask of the walls of the level.
    [SerializeField] private LayerMask wallLayer;

    // Walking speed.
    public const float Speed = 200f;

    // Size of the bounding box around the player where the enemy IS NOT allowed to move.
    public const float MinDistanceFromPlayer = 4f;

    // Size of the bounding box around the player where the enemy IS allowed to move.
    public const float MaxDistanceFromPlayer = 6f;

    // Distance how close the enemy needs to be to a waypoint until he moves on to the next one.
    public const float NextWaypointDistance = 1f;

    protected override Node SetupTree()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Transform playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        Weapon weapon = GetComponentInChildren<Weapon>();
        Seeker seeker = GetComponent<Seeker>();

        Node root = new Sequence(new List<Node>()
        {
            // Check first if target has been defined
            // If not, pick a new target around the player
            new Selector(new List<Node>()
            {
                new CheckTargetIsDefined(),
                new TaskPickTargetAroundPlayer(playerTransform)
            }),
            // Check if enemy is at target
            // If not, continue moving towards it
            new Selector(new List<Node>()
            {
                new CheckIsAtTarget(rb),
                new TaskMoveToTarget(rb, seeker)
            }),
            new TaskLookAtMovementDirection(rb), // Look at movement direction
            new CheckPlayerVisible(rb, playerTransform,
                wallLayer), // Check if player is visible after target was reached
            new CheckIsAtTarget(rb), // TODO: Secondary CheckIsAtTarget kinda sucks
            new TaskLookAtPlayer(rb, playerTransform), // Look towards the player
            new TaskAim(), // Aim at the player
            new TaskAttackPlayer(weapon), // Attack the player
            new TaskClearTarget() // Clear target so a new one is picked
        });
        
        root.SetData("targetReached", false);

        return root;
    }
}