using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using BehaviorTree;

public class StaticEnemyBT : BTree
{
    // Layer mask of the walls of the level.
    [SerializeField] private LayerMask wallLayer;

    protected override Node SetupTree()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Transform playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        EnemyWeapon weapon = GetComponentInChildren<EnemyWeapon>();

        Node root = new Sequence(new List<Node>()
        {
            new CheckPlayerVisible(rb, playerTransform, wallLayer),
            new TaskLookAtPlayer(rb, playerTransform),
            new TaskAim(),
            new TaskAttackPlayer(weapon),
        });

        root.SetData("targetReached", false);
        root.SetData("isAiming", false);

        return root;
    }
}