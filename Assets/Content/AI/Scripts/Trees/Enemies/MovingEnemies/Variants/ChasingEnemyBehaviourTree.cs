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
    public class ChasingEnemyBehaviourTree : MovingEnemyBehaviourTree
    {
        protected override Node SetupTree()
        {
            base.SetupTree();

            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            Transform playerTransform = GameObject.Find("Player").GetComponent<Transform>();
            EnemyWeapon weapon = GetComponentInChildren<EnemyWeapon>();
            Animator animator = GetComponentInChildren<Animator>();
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
                    new CheckHasState(sharedData.IsStunned),
                    new SetData<bool>(sharedData.IsAwareOfPlayer, true),
                    new SetAnimatorParameter<bool>(animator, "Walking", false),
                    // TODO: Behavior while stunned
                }),
                // Case: Enemy is thrown
                new Sequence(new List<Node>
                {
                    new CheckHasState(sharedData.IsThrown),
                    new SetData<bool>(sharedData.IsAwareOfPlayer, true),
                    new SetAnimatorParameter<bool>(animator, "Walking", false),
                    new TaskClearPath(agent),
                }),
                // Case: Enemy is aware of player
                new Sequence(new List<Node>
                {
                    new CheckIsAwareOfPlayer(),
                    new Selector(new List<Node>
                    {
                        new Failer(new SetAnimatorParameter<bool>(animator, "Walking", true)),
                        // Case: Enemy can see player and attacks him
                        new Sequence(new List<Node>
                        {
                            new CheckPlayerVisible(rb, playerTransform, wallLayer),
                            new TaskSetLastKnownPlayerLocation(playerTransform),
                            new TaskAimAt(rb, weapon, playerTransform),
                            new TaskPickTargetAroundTransforms(playerTransform, minDistanceFromPlayer,
                                maxDistanceFromPlayer),
                            new TaskMoveToTarget(rb, agent, animator, 1f),
                            new TaskWait(1f, false),
                            new TaskAttackPlayer(weapon, 1f),
                        }),
                        // Case: Enemy just heard the player shoot
                        new Sequence(new List<Node>
                        {
                            new HasData<bool>(sharedData.HasHeardPlayerShot),
                            new TaskSetLastKnownPlayerLocation(playerTransform),
                            new TaskSetTargetToLastKnownPlayerLocation(),
                            new ClearData<bool>(sharedData.HasHeardPlayerShot)
                        }),
                        // Case: Enemy moves towards last known player location
                        new Sequence(new List<Node>
                        {
                            new HasData<Vector3>(sharedData.LastKnownPlayerLocation),
                            new TaskSetTargetToLastKnownPlayerLocation(),
                            new TaskMoveToTarget(rb, agent, animator, 1f),
                            new CheckIsAtTarget(),
                            new ClearData<Vector3>(sharedData.LastKnownPlayerLocation),
                        }),
                        // Case: Enemy reached last known player location, now moves to a random spawn location
                        new Sequence(new List<Node>
                        {
                            new Inverter(new HasData<Vector3>(sharedData.LastKnownPlayerLocation)),
                            new Selector(new List<Node>
                            {
                                new HasData<Vector3>(sharedData.Target),
                                new TaskPickTargetAroundTransforms(spawnPointTransforms, minDistanceFromPlayer,
                                    maxDistanceFromPlayer),
                            }),
                            new TaskMoveToTarget(rb, agent, animator, 1f),
                            new CheckIsAtTarget(),
                            new ClearData<Vector3>(sharedData.Target),
                        }),
                    })
                }),
                // Case: Enemy sees Player for the first time
                new Sequence(new List<Node>
                {
                    new CheckIfPlayerIsInRange(rb, playerTransform, Configuration.Enemy_ViewDistance),
                    new CheckPlayerVisible(rb, playerTransform, wallLayer),
                    new SetData<bool>(sharedData.IsAwareOfPlayer, true),
                }),
                // Case: Enemy hears player
                new Sequence(new List<Node>
                {
                    new ExpectData<bool>(sharedData.HasHeardPlayerShot, true),
                    new CheckIfPlayerIsInRange(rb, playerTransform, Configuration.Enemy_HearDistance),
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