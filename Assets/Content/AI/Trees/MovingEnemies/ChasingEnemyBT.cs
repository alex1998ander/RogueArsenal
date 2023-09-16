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
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        Transform[] spawnPointTransforms = new Transform[spawnPoints.Length];

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            spawnPointTransforms[i] = spawnPoints[i].transform;
        }

        SharedData sharedData = new SharedData();
        
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
                        new TaskPickTargetAroundTransforms(playerTransform, minDistanceFromPlayer,
                            maxDistanceFromPlayer),
                        new Selector(new List<Node>()
                        {
                            new CheckIsAtTarget(),
                            new TaskMoveToTarget(rb, agent, 1f)
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
                            new CheckHasData<Vector3>(sharedData.PlayerLocation),
                            new Selector(new List<Node>()
                            {
                                new CheckHasData<Vector3>(sharedData.Target),
                                new TaskPickTargetAroundTransforms(playerTransform, minDistanceFromPlayer,
                                    maxDistanceFromPlayer),
                            }),
                            new TaskMoveToTarget(rb, agent, 1f),
                            new TaskLookAt(rb),
                            new CheckIsAtTarget(),
                            new TaskClearData<Vector3>(sharedData.PlayerLocation)
                        }),
                        // Enemy doesn't have last known player location
                        new Sequence(new List<Node>()
                        {
                            new Selector(new List<Node>()
                            {
                                new CheckHasData<Vector3>(sharedData.Target),
                                new TaskPickTargetAroundTransforms(spawnPointTransforms, 0f, 0f)
                            }),
                            new TaskMoveToTarget(rb, agent, 1f),
                            new CheckIsAtTarget(),
                            new TaskClearData<Vector3>(sharedData.Target)
                        })
                    })
                })
            }),
            // Enemy sees Player for the first time
            new Sequence(new List<Node>()
            {
                new CheckIfPlayerIsInRange(rb, playerTransform, 1f),
                new CheckPlayerVisible(rb, playerTransform, wallLayer),
                new TaskSetData<bool>(sharedData.IsAwareOfPlayer, true)
            })
        });

        root.SetupSharedData(sharedData);

        return root;
    }
}