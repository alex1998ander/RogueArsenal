using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using BehaviorTree;
using UnityEngine.Serialization;

public class FollowingEnemyBT : BTree
{
    // Layer mask of the walls of the level.
    [SerializeField] private LayerMask wallLayer;

    // Walking speed.
    [SerializeField] private float walkingSpeed = 200f;

    // Size of the bounding box around the player where the enemy IS NOT allowed to move.
    [SerializeField] private float minDistanceFromPlayer = 4f;

    // Size of the bounding box around the player where the enemy IS allowed to move.
    [SerializeField] private float maxDistanceFromPlayer = 6f;

    protected override Node SetupTree()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Transform playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        EnemyWeapon weapon = GetComponentInChildren<EnemyWeapon>();
        Seeker seeker = GetComponent<Seeker>();

        Node root = new Selector(new List<Node>()
        {
            // Attack the target
            new Sequence(new List<Node>()
            {
                // Is enemy at target?
                new CheckIsAtTarget(),
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
                    new TaskPickTargetAroundPlayer(playerTransform, minDistanceFromPlayer, maxDistanceFromPlayer),
                }),
                new TaskMoveToTarget(rb, walkingSpeed, seeker),
                new TaskLookAtMovementDirection(rb), // Look at movement direction
            }),
        });

        root.SetData("targetReached", false);
        root.SetData("isAiming", false);

        return root;
    }
}