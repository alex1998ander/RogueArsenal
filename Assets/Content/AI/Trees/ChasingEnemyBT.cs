using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using BehaviorTree;
using UnityEngine.Serialization;

public class ChasingEnemyBT : BTree
{
    // Layer mask of the walls of the level.
    [SerializeField] private LayerMask wallLayer;

    // Walking speed.
    [SerializeField] private float walkingSpeed = 200f;

    // Size of the bounding box around the player where the enemy IS NOT allowed to move.
    [SerializeField] private float minDistanceFromPlayer = 1f;

    // Size of the bounding box around the player where the enemy IS allowed to move.
    [SerializeField] private float maxDistanceFromPlayer = 2f;

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
            new TaskPickTargetAroundPlayer(playerTransform, minDistanceFromPlayer, maxDistanceFromPlayer),
            new TaskLookAtPlayer(rb, playerTransform),
            new TaskMoveToTarget(rb, walkingSpeed, seeker),
            new TaskAim(),
            new TaskAttackPlayer(weapon),
        });

        root.SetData("targetReached", false);
        root.SetData("isAiming", false);

        return root;
    }
}