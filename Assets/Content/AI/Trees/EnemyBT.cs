using UnityEngine;
using BehaviorTree;

public class EnemyBT : BTree
{
    // Layer mask of the walls of the level.
    [SerializeField] protected LayerMask wallLayer;

    // Amount of time the enemy is stunned
    [SerializeField] protected float stunTime;

    protected override Node SetupTree()
    {
        Node root = new Node();
        return root;
    }
}