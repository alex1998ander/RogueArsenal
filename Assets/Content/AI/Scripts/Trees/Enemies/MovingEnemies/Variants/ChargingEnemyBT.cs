using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorTree
{
    public class ChargingEnemyBT : MovingEnemyBT
    {
        // Speed when the enemy is charging towards the player
        [SerializeField] private float chargingSpeed = 20f;

        protected override Node SetupTree()
        {
            // Get relevant components
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            Transform playerTransform = GameObject.Find("Player").GetComponent<Transform>();

            // Set up nav agent
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            agent.updateRotation = false;
            agent.updateUpAxis = false;
            agent.speed = movementSpeed;
            agent.acceleration = 30f;

            // Get all spawn points in level
            GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
            Transform[] spawnPointTransforms = new Transform[spawnPoints.Length];
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                spawnPointTransforms[i] = spawnPoints[i].transform;
            }

            SharedData sharedData = new SharedData();
            sharedData.SetData(sharedData.ChargeState, ChargeState.None);

            Node root = new Selector(new List<Node>
            {
                new Failer(new Logger("--------")),
                // Case: Enemy is stunned
                new Sequence(new List<Node>
                {
                    new CheckIsStunned(stunTime),
                    new SetData<bool>(sharedData.IsAwareOfPlayer, true),
                    // TODO: Behavior while stunned
                }),
                // Case: Enemy is aware of player
                new Sequence(new List<Node>
                {
                    new CheckIsAwareOfPlayer(),
                    new Selector(new List<Node>
                    {
                        // Case: Enemy is not charging and runs towards player
                        new Sequence(new List<Node>
                        {
                            new ExpectData<ChargeState>(sharedData.ChargeState, ChargeState.None),
                            new CheckPlayerVisible(rb, playerTransform, wallLayer),
                            new TaskSetLastKnownPlayerLocation(playerTransform),
                            new TaskLookAt(rb, playerTransform),
                            new TaskPickTargetAroundTransforms(playerTransform, minDistanceFromPlayer,
                                maxDistanceFromPlayer),
                            new TaskMoveToTarget(rb, agent, 1f),
                            new CheckIsAtTarget(),
                            new SetData<ChargeState>(sharedData.ChargeState, ChargeState.PreCharge),
                        }),
                        // Case: Enemy came close enough, now prepares charging
                        new Sequence(new List<Node>
                        {
                            new ExpectData<ChargeState>(sharedData.ChargeState, ChargeState.PreCharge),
                            new Logger("A"),
                            new TaskLookAt(rb, playerTransform),
                            new TaskWait(5f, true),
                            new TaskPickTargetAroundTransforms(playerTransform, 0f, 0f),
                            new TaskSetMovementSpeed(agent, chargingSpeed),
                            new TaskSetMovementAcceleration(agent, 1000f),
                            new SetData<ChargeState>(sharedData.ChargeState, ChargeState.MidCharge),
                        }),
                        // Case: Enemy is currently charging
                        new Sequence(new List<Node>
                        {
                            new ExpectData<ChargeState>(sharedData.ChargeState, ChargeState.MidCharge),
                            new Logger("B"),
                            new TaskLookAt(rb, agent),
                            new TaskMoveToTarget(rb, agent, 1f),
                            new Logger("B2"),
                            new SetData<ChargeState>(sharedData.ChargeState, ChargeState.PostCharge),
                        }),
                        // Case: Enemy finished charging
                        new Sequence(new List<Node>
                        {
                            new ExpectData<ChargeState>(sharedData.ChargeState, ChargeState.PostCharge),
                            new Logger("C"),
                            new TaskSetMovementSpeed(agent, movementSpeed),
                            new TaskSetMovementAcceleration(agent, 5f),
                            new TaskWait(1f, true),
                            new SetData<ChargeState>(sharedData.ChargeState, ChargeState.None)
                        }),
                        // Case: Enemy just heard the player shoot
                        new Sequence(new List<Node>
                        {
                            new ExpectData<ChargeState>(sharedData.ChargeState, ChargeState.None),
                            new HasData<bool>(sharedData.HasHeardPlayerShot),
                            new TaskSetLastKnownPlayerLocation(playerTransform),
                            new TaskSetTargetToLastKnownPlayerLocation(),
                            new ClearData<bool>(sharedData.HasHeardPlayerShot)
                        }),
                        // Case: Enemy moves towards last known player location
                        new Sequence(new List<Node>
                        {
                            new ExpectData<ChargeState>(sharedData.ChargeState, ChargeState.None),
                            new HasData<Vector3>(sharedData.LastKnownPlayerLocation),
                            new TaskSetTargetToLastKnownPlayerLocation(),
                            new TaskMoveToTarget(rb, agent, 1f),
                            new TaskLookAt(rb, agent),
                            new CheckIsAtTarget(),
                            new ClearData<Vector3>(sharedData.LastKnownPlayerLocation),
                        }),
                        // Case: Enemy reached last known player location, now moves to a random spawn location
                        new Sequence(new List<Node>
                        {
                            new ExpectData<ChargeState>(sharedData.ChargeState, ChargeState.None),
                            new Inverter(new HasData<Vector3>(sharedData.LastKnownPlayerLocation)),
                            new Selector(new List<Node>
                            {
                                new HasData<Vector3>(sharedData.Target),
                                new TaskPickTargetAroundTransforms(spawnPointTransforms, minDistanceFromPlayer,
                                    maxDistanceFromPlayer),
                            }),
                            new TaskMoveToTarget(rb, agent, 1f),
                            new TaskLookAt(rb, agent),
                            new CheckIsAtTarget(),
                            new ClearData<Vector3>(sharedData.Target),
                        }),
                    })
                }),
                // Enemy sees Player for the first time
                new Sequence(new List<Node>
                {
                    new CheckIfPlayerIsInRange(rb, playerTransform, viewDistance),
                    new CheckPlayerVisible(rb, playerTransform, wallLayer),
                    new SetData<bool>(sharedData.IsAwareOfPlayer, true),
                })
            });

            root.SetupSharedData(sharedData);

            return root;
        }
    }
}