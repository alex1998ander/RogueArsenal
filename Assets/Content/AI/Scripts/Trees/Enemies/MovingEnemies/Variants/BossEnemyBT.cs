using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorTree
{
    /// <summary>
    /// Behavior tree for a standard boss enemy.
    /// Starts not aware of the player and stays still.
    /// When he sees/hears the enemy, becomes aware of the player.
    /// Follows and attacks the player when he sees them.
    /// When the player is not visible, goes to the last known player position.
    /// Otherwise patrols the area.
    /// </summary>
    [RequireComponent(typeof(LineRenderer))]
    public class BossEnemyBT : MovingEnemyBT
    {
        [SerializeField] private GameObject mine; 
        [SerializeField] private GameObject turret;
        [SerializeField] private GameObject clone;
        [SerializeField] private GameObject shieldGenerator;
        [SerializeField] private GameObject shield;
        protected override Node SetupTree()
        {
            LineRenderer lineRenderer = GetComponent<LineRenderer>();
            SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            Transform transform = this.transform;
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            Transform playerTransform = GameObject.Find("Player").GetComponent<Transform>();
            EnemyWeapon weapon = GetComponentInChildren<EnemyWeapon>();
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            agent.updateRotation = false;
            agent.updateUpAxis = false;

            Node[] tasks = new Node[] {new BossAttackDash(transform, rb, playerTransform),new BossAttackLaserFocus(lineRenderer,playerTransform,transform), new BossAttackStomp(transform,playerTransform,spriteRenderer), new BossAttackSpawnObject(transform, mine), new BossAttackSpawnObject(transform, turret), new BossAttackSpawnObject(transform, clone, new Vector3(3,3,3))};

            const float attackSpeed = 15f;

            SharedData sharedData = new SharedData();
            sharedData.SetData(sharedData.AbilityState, AbilityState.None);

            Node root = new Selector(new List<Node>
            {
                // Case: Enemy is stunned
                // new Sequence(new List<Node>
                // {
                //     new CheckIsStunned(stunTime),
                //     new SetData<bool>(sharedData.IsAwareOfPlayer, true),
                //     // TODO: Behavior while stunned
                // }),
                // Case: Enemy is aware of player
                new Sequence(new List<Node>
                {
                    new CheckIsAwareOfPlayer(),
                    new Selector(new List<Node>
                    {
                        // Case: Enemy can see player and attacks him
                        new Sequence(new List<Node>
                        {
                            new Inverter (new ExpectData<AbilityState>(sharedData.AbilityState, AbilityState.Ability)),
                            new TaskSetLastKnownPlayerLocation(playerTransform),
                            new TaskLookAt(rb, playerTransform),
                            new TaskPickTargetAroundTransforms(playerTransform, minDistanceFromPlayer,
                                maxDistanceFromPlayer),
                            new TaskMoveToTarget(rb, agent, 1f),
                            new TaskAttackPlayer(weapon, attackSpeed),
                            new TaskWait(2f, true),
                            new SetData<AbilityState>(sharedData.AbilityState, AbilityState.Ability)
                        }),
                        new Sequence(new List<Node>
                        {
                            new ExpectData<AbilityState>(sharedData.AbilityState, AbilityState.Ability),
                            new TaskSetLastKnownPlayerLocation(playerTransform),
                            new TaskLookAt(rb, playerTransform),
                            new TaskSetMovementSpeed(agent,  0),
                            tasks[1],
                            new TaskSetMovementSpeed(agent, 3.5f),
                            new SetData<AbilityState>(sharedData.AbilityState, AbilityState.Cooldown)
                            
                        }),
                        // Case: Enemy just heard the player shoot
                        new Sequence(new List<Node>
                        {
                            new Inverter (new ExpectData<AbilityState>(sharedData.AbilityState, AbilityState.Ability)),
                            new HasData<bool>(sharedData.HasHeardPlayerShot),
                            new TaskSetLastKnownPlayerLocation(playerTransform),
                            new TaskSetTargetToLastKnownPlayerLocation(),
                            new ClearData<bool>(sharedData.HasHeardPlayerShot)
                        }),
                        // Case: Enemy moves towards last known player location
                        new Sequence(new List<Node>
                        {
                            new Inverter (new ExpectData<AbilityState>(sharedData.AbilityState, AbilityState.Ability)),
                            new HasData<Vector3>(sharedData.LastKnownPlayerLocation),
                            new TaskSetTargetToLastKnownPlayerLocation(),
                            new TaskMoveToTarget(rb, agent, 1f),
                            new TaskLookAt(rb, agent),
                            new CheckIsAtTarget(),
                            new ClearData<Vector3>(sharedData.LastKnownPlayerLocation),
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