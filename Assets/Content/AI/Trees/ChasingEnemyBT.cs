using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using BehaviorTree;

public class ChasingEnemyBT : BTree
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
        EnemyWeapon weapon = GetComponentInChildren<EnemyWeapon>();
        Seeker seeker = GetComponent<Seeker>();

        Node root = new Sequence(new List<Node>()
        {
            // Else, aim at the player and shoot
            new TaskClearTarget(),
            new TaskPickTargetAroundPlayer(playerTransform),
            new TaskLookAtPlayer(rb, playerTransform),
            new TaskMoveToTarget(rb, seeker),
            new TaskAim(),
            new TaskAttackPlayer(weapon),
        });

        root.SetData("targetReached", false);
        root.SetData("isAiming", false);

        return root;
    }
}