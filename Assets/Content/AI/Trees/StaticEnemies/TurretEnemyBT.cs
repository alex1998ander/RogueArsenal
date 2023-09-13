using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TurretEnemyBT : EnemyBT
{
    protected override Node SetupTree()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Transform playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        EnemyWeapon weapon = GetComponentInChildren<EnemyWeapon>();

        Node root = new Sequence(new List<Node>()
        {
            new Inverter(new CheckIsStunned(stunTime)),
            new CheckPlayerVisible(rb, playerTransform, wallLayer),
            new TaskLookAtPlayer(rb, playerTransform),
            new TaskAim(),
            new TaskAttackPlayer(weapon),
        });

        root.SetData(SharedData.TargetReached, false);
        root.SetData(SharedData.IsAiming, false);
        root.SetData(SharedData.IsStunned, false);

        return root;
    }
}