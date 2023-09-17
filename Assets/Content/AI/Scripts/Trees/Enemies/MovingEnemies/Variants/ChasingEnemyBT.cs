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
                            new Logger("1"),
                            new CheckPlayerVisible(rb, playerTransform, wallLayer),
                            new Logger("2"),
                            new TaskSetData<Vector3>(sharedData.LastKnownPlayerLocation, playerTransform.position),
                            new Logger("3"),
                            new TaskLookAt(rb, playerTransform),
                            new Logger("4"),
                            new TaskPickTargetAroundTransforms(playerTransform, minDistanceFromPlayer,
                                maxDistanceFromPlayer),
                            new Logger("5"),
                            // new Selector(new List<Node>
                            // {
                            // new CheckIsAtTarget(),
                            new TaskMoveToTarget(rb, agent, 1f),
                            // }),
                            new Logger("6"),
                            new TaskWait(1f),
                            new Logger("7"),
                            new TaskAttackPlayer(weapon, 1f),
                            new Logger("8"),
                        }),
                        // Case: Enemy moves towards last known player location
                        new Sequence(new List<Node>
                        {
                            new Logger("A"),
                            new CheckHasData<Vector3>(sharedData.LastKnownPlayerLocation),
                            new Logger("B"),
                            new TaskSetData<Vector3>(sharedData.Target,
                                sharedData.GetData(sharedData.LastKnownPlayerLocation)),
                            new Logger("C"),
                            new TaskMoveToTarget(rb, agent, 1f),
                            new Logger("D"),
                            new CheckIsAtTarget(),
                            new Logger("E"),
                            new TaskClearData<Vector3>(sharedData.LastKnownPlayerLocation),
                            new Logger("F"),
                        }),
                        // Case: Enemy reached last known player location, now moves to a random spawn location
                        new Sequence(new List<Node>
                        {
                            new Logger("1.1"),
                            new Inverter(new CheckHasData<Vector3>(sharedData.LastKnownPlayerLocation)),
                            new Logger("1.2"),
                            new Selector(new List<Node>
                            {
                                new Logger("1.3"),
                                new CheckHasData<Vector3>(sharedData.Target),
                                new Logger("1.4"),
                                new TaskPickTargetAroundTransforms(spawnPointTransforms, minDistanceFromPlayer,
                                    maxDistanceFromPlayer),
                                new Logger("1.5"),
                            }),
                            new Logger("1.6"),
                            new TaskMoveToTarget(rb, agent, 1f),
                            new Logger("1.7"),
                            new CheckIsAtTarget(),
                            new Logger("1.8"),
                            new TaskClearData<Vector3>(sharedData.Target),
                            new Logger("1.9"),
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