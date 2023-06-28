using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using BehaviorTree;

public class ChasingEnemyBT : MovingEnemyBT
{
    protected override Node SetupTree()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Transform playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        EnemyWeapon weapon = GetComponentInChildren<EnemyWeapon>();
        Seeker seeker = GetComponent<Seeker>();

        Node root = new Sequence(new List<Node>()
        {
            new Inverter(new CheckIsStunned(stunTime)),
            new TaskClearTarget(),
            new TaskPickTargetAroundPlayer(playerTransform, minDistanceFromPlayer, maxDistanceFromPlayer),
            new TaskLookAtPlayer(rb, playerTransform),
            new TaskMoveToTarget(rb, walkingSpeed, seeker),
            new TaskAim(),
            new TaskAttackPlayer(weapon),
        });

        root.SetData("targetReached", false);
        root.SetData("isAiming", false);
        root.SetData("stunned", false);

        return root;
    }
}