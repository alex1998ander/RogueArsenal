using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorTree
{
    public class ChargingEnemyBehaviourTree : MovingEnemyBehaviourTree
    {
        [SerializeField] private Collider2D damageZoneCollider;
        [SerializeField] private float chargingSpeed = 20f;
        [SerializeField] private float chargingAcceleration = 100f;
        [SerializeField] private float preChargeTime = 1f;
        [SerializeField] private float postChargeTime = 1f;
        [SerializeField] private float chargePastPlayerDistance = 2f;

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
            agent.acceleration = movementAcceleration;

            // Get all spawn points in level
            GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
            Transform[] spawnPointTransforms = new Transform[spawnPoints.Length];
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                spawnPointTransforms[i] = spawnPoints[i].transform;
            }

            damageZoneCollider.enabled = false;

            SharedData sharedData = new SharedData();
            sharedData.SetData(sharedData.ChargeState, ChargeState.None);

            Node root = new Selector(new List<Node>
            {
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
                            new TaskLookAt(rb, playerTransform),
                            new TaskWait(preChargeTime, true),
                            new TaskPickTargetBehindTransform(agent, playerTransform, chargePastPlayerDistance),
                            new TaskSetMovementSpeed(agent, chargingSpeed),
                            new TaskSetMovementAcceleration(agent, chargingAcceleration),
                            new SetData<ChargeState>(sharedData.ChargeState, ChargeState.MidCharge),
                        }),
                        // Case: Enemy is currently charging
                        new Sequence(new List<Node>
                        {
                            new ExpectData<ChargeState>(sharedData.ChargeState, ChargeState.MidCharge),
                            new TaskActivateDamageZone(true, damageZoneCollider),
                            new TaskLookAt(rb, agent),
                            new TaskMoveToTarget(rb, agent, 1f),
                            new SetData<ChargeState>(sharedData.ChargeState, ChargeState.PostCharge),
                        }),
                        // Case: Enemy finished charging
                        new Sequence(new List<Node>
                        {
                            new ExpectData<ChargeState>(sharedData.ChargeState, ChargeState.PostCharge),
                            new TaskSetMovementSpeed(agent, movementSpeed),
                            new TaskSetMovementAcceleration(agent, movementAcceleration),
                            new TaskWait(postChargeTime, true),
                            new TaskActivateDamageZone(false, damageZoneCollider),
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
                // Case: Enemy sees Player for the first time
                new Sequence(new List<Node>
                {
                    new CheckIfPlayerIsInRange(rb, playerTransform, viewDistance),
                    new CheckPlayerVisible(rb, playerTransform, wallLayer),
                    new SetData<bool>(sharedData.IsAwareOfPlayer, true),
                }),
                // Case: Enemy hears player
                new Sequence(new List<Node>
                {
                    new ExpectData<bool>(sharedData.HasHeardPlayerShot, true),
                    new CheckIfPlayerIsInRange(rb, playerTransform, hearDistance),
                    new SetData<bool>(sharedData.IsAwareOfPlayer, true),
                }),
                // At end, overwrite HasHeardPlayerShot so enemy doesn't "hear" the player in the future
                new SetData<bool>(sharedData.HasHeardPlayerShot, false)
            });

            root.SetupSharedData(sharedData);

            return root;
        }
    }
}