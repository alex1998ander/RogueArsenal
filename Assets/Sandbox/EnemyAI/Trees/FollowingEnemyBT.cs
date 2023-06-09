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

        Node root = new Selector(new List<Node>()
        {
            // Attack the target
            new Sequence(new List<Node>()
            {
                // Is enemy at target?
                new CheckIsAtTarget(rb),
                // if so: Either check if the player is NOT visible from its location
                // If it's not, clear the target
                new Selector(new List<Node>()
                {
                    new CheckPlayerVisible(rb, playerTransform, wallLayer),
                    // Invert because if we get here, we want the selector to return a failure state
                    // so the sequence can quit
                    new Inverter(new TaskClearTarget()),
                }),

                // Else, aim at the player and shoot
                new TaskLookAtPlayer(rb, playerTransform),
                new TaskAim(),
                new TaskAttackPlayer(weapon),
                new TaskClearTarget(),
            }),
            // Move to target
            new Sequence(new List<Node>()
            {
                // Only move the enemy if he isn't currently aiming
                new Inverter(new CheckIsAiming()),
                // Check first if target has been defined
                // If not, pick a new target around the player
                new Selector(new List<Node>()
                {
                    new CheckTargetIsDefined(),
                    new TaskPickTargetAroundPlayer(playerTransform),
                }),
                new TaskMoveToTarget(rb, seeker),
                new TaskLookAtMovementDirection(rb), // Look at movement direction
            }),
        });

        root.SetData("targetReached", false);
        root.SetData("isAiming", false);

        return root;
    }
}