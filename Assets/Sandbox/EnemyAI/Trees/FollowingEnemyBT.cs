using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class FollowingEnemyBT : BTree
{
    
    
    // Layer mask of the walls of the level.
    [SerializeField] private LayerMask wallLayer;
    
    // Size of the bounding box around the player where the enemy IS NOT allowed to move.
    public static float MinDistanceFromPlayer = 4f;

    // Size of the bounding box around the player where the enemy IS allowed to move.
    public static float MaxDistanceFromPlayer = 6f;

    protected override Node SetupTree()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Transform playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        Weapon weapon = GetComponentInChildren<Weapon>();

        Node root = new Sequence(new List<Node>()
        {
            new CheckPlayerVisible(rb, playerTransform, wallLayer),
            new TaskAimAtPlayer(rb, playerTransform),
            new TaskAttackPlayer(weapon)
        });

        return root;
    }
}