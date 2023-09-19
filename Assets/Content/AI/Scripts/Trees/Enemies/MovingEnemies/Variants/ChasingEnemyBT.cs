using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorTree
{
    /// <summary>
    /// Behavior tree for a standard chasing enemy.
    /// Starts not aware of the player and stays still.
    /// When he sees/hears the enemy, becomes aware of the player.
    /// Follows and attacks the player when he sees them.
    /// When the player is not visible, goes to the last known player position.
    /// Otherwise patrols the area.
    /// </summary>
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

            Node root = new Selector(new List<Node>
            {
                // Case: Enemy is stunned
                new Sequence(new List<Node>
                {
                    new CheckIsStunned(stunTime),
                    new TaskSetData<bool>(sharedData.IsAwareOfPlayer, true),
                    // TODO: Behavior while stunned
                }),
                // Case: Enemy is aware of player
                new Sequence(new List<Node>
                {
                    new CheckIsAwareOfPlayer(),
                    new Selector(new List<Node>
                    {
                        // Case: Enemy can see player
                        new Sequence(new List<Node>
                        {
                            new CheckPlayerVisible(rb, playerTransform, wallLayer),
                            new TaskSetLastKnownPlayerLocation(playerTransform),
                            new TaskLookAt(rb, playerTransform),
                            new TaskPickTargetAroundTransforms(playerTransform, minDistanceFromPlayer,
                                maxDistanceFromPlayer),
                            new TaskMoveToTarget(rb, agent, 1f),
                            new TaskWait(1f),
                            new TaskAttackPlayer(weapon, 1f),
                        }),
                        // Case: Enemy moves towards last known player location
                        new Sequence(new List<Node>
                        {
                            new CheckHasData<Vector3>(sharedData.LastKnownPlayerLocation),
                            new TaskSetTargetToLastKnownPlayerLocation(),
                            new TaskMoveToTarget(rb, agent, 1f),
                            new CheckIsAtTarget(),
                            new TaskClearData<Vector3>(sharedData.LastKnownPlayerLocation),
                        }),
                        // Case: Enemy reached last known player location, now moves to a random spawn location
                        new Sequence(new List<Node>
                        {
                            new Inverter(new CheckHasData<Vector3>(sharedData.LastKnownPlayerLocation)),
                            new Selector(new List<Node>
                            {
                                new CheckHasData<Vector3>(sharedData.Target),
                                new TaskPickTargetAroundTransforms(spawnPointTransforms, minDistanceFromPlayer,
                                    maxDistanceFromPlayer),
                            }),
                            new TaskMoveToTarget(rb, agent, 1f),
                            new CheckIsAtTarget(),
                            new TaskClearData<Vector3>(sharedData.Target),
                        }),
                    })
                }),
                // Enemy sees Player for the first time
                new Sequence(new List<Node>
                {
                    new CheckIfPlayerIsInRange(rb, playerTransform, viewDistance),
                    new CheckPlayerVisible(rb, playerTransform, wallLayer),
                    new TaskSetData<bool>(sharedData.IsAwareOfPlayer, true),
                })
            });

            root.SetupSharedData(sharedData);

            return root;
        }
    }
}