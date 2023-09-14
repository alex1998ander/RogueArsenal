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

        Node root = new Selector(new List<Node>()
        {
            // Enemy is stunned
            new Sequence(new List<Node>()
            {
                new CheckIsStunned(stunTime)
                // TODO: Behavior while stunned
            }),
            // Enemy is aware of player
            new Sequence(new List<Node>()
            {
                new CheckIsAwareOfPlayer(),
                new Selector(new List<Node>()
                {
                    // Enemy can see player
                    new Sequence(new List<Node>()
                    {
                        new CheckPlayerVisible(rb, playerTransform, wallLayer),
                        new TaskSavePlayerLocation(playerTransform),
                        new TaskLookAt(rb, playerTransform),
                        new Selector(new List<Node>()
                        {
                            new CheckHasData(SharedData.Target),
                            new TaskPickTargetAroundPlayer(playerTransform, minDistanceFromPlayer,
                                maxDistanceFromPlayer)
                        }),
                        new Selector(new List<Node>()
                        {
                            new CheckIsAtTarget(),
                            new TaskMoveToTarget(rb, agent)
                        }),
                        new TaskWait(1f),
                        new TaskAttackPlayer(weapon)
                    }),
                    // Enemy can't see player
                    new Selector(new List<Node>()
                    {
                        // Enemy has last known player location
                        new Sequence(new List<Node>()
                        {
                            new CheckHasData(SharedData.PlayerLocation),
                            new Selector(new List<Node>()
                            {
                                new CheckHasData(SharedData.Target),
                                new TaskPickTargetAroundPlayer(playerTransform, minDistanceFromPlayer,
                                    maxDistanceFromPlayer),
                            }),
                            new TaskMoveToTarget(rb, agent),
                            new CheckIsAtTarget(),
                            new TaskRemoveData(SharedData.PlayerLocation)
                        }),
                        // Enemy doesn't have last known player location
                        new Sequence(new List<Node>()
                        {
                            new Selector(new List<Node>()
                            {
                                new CheckHasData(SharedData.Target),
                            })
                        })
                    })
                })
            })
        });

        return root;
    }
}