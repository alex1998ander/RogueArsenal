using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using UnityEngine.AI;

public class ChasingEnemyBT : MovingEnemyBT
{
    protected override Node SetupTree()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Transform playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        EnemyWeapon weapon = GetComponentInChildren<EnemyWeapon>();
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        Node root = new Sequence(new List<Node>()
        {
            new Inverter(new CheckIsStunned(stunTime)),
            new TaskClearTarget(),
            new TaskPickTargetAroundPlayer(playerTransform, minDistanceFromPlayer, maxDistanceFromPlayer),
            new TaskLookAtPlayer(rb, playerTransform),
            new TaskMoveToTarget(rb, agent),
            new TaskAim(),
            new TaskAttackPlayer(weapon),
        });

        return root;
    }
}