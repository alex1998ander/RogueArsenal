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
    public class BossEnemyBehaviourTree : MovingEnemyBehaviourTree
    {
        [SerializeField] private BoxCollider2D trigger;
        [SerializeField] private BoxCollider2D contactDamageZone;
        [SerializeField] private GameObject mine;
        [SerializeField] private GameObject turret;
        [SerializeField] private GameObject clone;
        [SerializeField] private GameObject shieldGenerator;
        [SerializeField] private GameObject bullet;
        [SerializeField] private GameObject shockWave;
        [SerializeField] private GameObject ui;

        [SerializeField] private ParticleSystem chargingEffect;
        [SerializeField] private ParticleSystem dashingEffect;
        [SerializeField] private LightFader chargeLightFader;
        [SerializeField] private float chargeLightMaxIntensity;

        private GameObject[] _mineSpawns;

        protected override Node SetupTree()
        {
            base.SetupTree();

            float Boss_AbilityCooldown = 3f;
            GameObject[] _mineSpawns = GameObject.FindGameObjectsWithTag("MineSpawn");
            LineRenderer lineRenderer = GetComponent<LineRenderer>();
            Transform transform = this.transform;
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            Transform playerTransform = FindObjectOfType<PlayerController>().GetComponent<Transform>();
            EnemyWeapon weapon = GetComponentInChildren<EnemyWeapon>();
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            //For the nav mesh agent
            agent.updateRotation = false;
            agent.updateUpAxis = false;

            shieldGenerator.SetActive(false);
            shockWave.SetActive(false);

            SharedData sharedData = new SharedData();
            sharedData.SetData(sharedData.AbilityState, AbilityState.None);
            sharedData.SetData(sharedData.RandomAbility, -1);
            sharedData.SetData(sharedData.AbilityPool, 0);

            Node[][] tasks =
            {
                new Node[]
                {
                    // new BossAttackLaserFocus(lineRenderer, playerTransform, transform),
                    // new BossAttackLaserFocus(lineRenderer, playerTransform, transform),
                    // new BossAttackLaserFocus(lineRenderer, playerTransform, transform),
                    new BossAttackSpawnObject(transform, turret, Vector3.one, 3),
                    new BossAttackSpawnObject(transform, clone, new Vector3(1.5f, 1.5f, 1.5f), 3),
                    new BossAttackStomp(trigger, transform, playerTransform, enemyAnimator, ui, weapon),
                },
                new Node[]
                {
                    new BossAttackDash(transform, rb, playerTransform, contactDamageZone, chargingEffect, dashingEffect, chargeLightFader, chargeLightMaxIntensity),
                    new BossAttack360Shot(transform, bullet),
                    new BossAttackSpawnObject(_mineSpawns, mine, new Vector3(0.5f, 0.5f, 0.5f))
                },
                new Node[]
                {
                    new BossAttackLaserFocus(lineRenderer, playerTransform, transform),
                    new BossAttackShield(shieldGenerator),
                    new BossAttackShockwave(shockWave)
                }
            };

            Node root = new Selector(new List<Node>
            {
                // Case: Enemy is aware of player
                new Sequence(new List<Node>
                {
                    new CheckIsAwareOfPlayer(),
                    new Selector(new List<Node>
                    {
                        new Sequence(new List<Node>()
                        {
                            new ExpectData<AbilityState>(sharedData.AbilityState, AbilityState.Cooldown),
                            new TaskWait(Configuration.Boss_AbilityCooldown, true),
                            new SetData<AbilityState>(sharedData.AbilityState, AbilityState.None)
                        }),
                        // Case: Enemy can see player and attacks him
                        new Sequence(new List<Node>
                        {
                            new ExpectData<AbilityState>(sharedData.AbilityState, AbilityState.None),
                            new TaskSetLastKnownPlayerLocation(playerTransform),
                            new TaskLookAt(playerTransform, rb, null),
                            new TaskPickTargetAroundTransforms(playerTransform, minDistanceFromPlayer,
                                maxDistanceFromPlayer),
                            new TaskMoveToTarget(rb, agent, enemyAnimator, 1f),
                            //new CheckIsAtTarget(),
                            new TaskAimAt(rb, weapon, playerTransform, enemyAnimator),
                            new TaskAttackPlayer(weapon, Configuration.Boss_AttackSpeed),
                            new TaskWait(Configuration.Boss_AbilityCooldown, true),
                            new ChooseRandomAttackMove(tasks[sharedData.GetData(sharedData.AbilityPool)].Length),
                            new BossChangeAttackDependingOnHealth(transform),
                            new SetData<AbilityState>(sharedData.AbilityState, AbilityState.Ability)
                        }),
                        new Sequence(new List<Node>
                        {
                            new ExpectData<AbilityState>(sharedData.AbilityState, AbilityState.Ability),
                            new ExpectData<int>(sharedData.AbilityPool, 0),
                            new TaskSetLastKnownPlayerLocation(playerTransform),
                            new TaskAimAt(rb, weapon, playerTransform, enemyAnimator),
                            new TaskLookAt(playerTransform, rb, null),
                            new TaskSetMovementSpeed(agent, 0),
                            new RandomAttackMove(tasks[0]),
                            //tasksPool[4], //tasksPool.Length - 2
                            new TaskSetMovementSpeed(agent, 3.5f),
                            new SetData<AbilityState>(sharedData.AbilityState, AbilityState.Cooldown)
                        }),
                        new Sequence(new List<Node>
                        {
                            new ExpectData<AbilityState>(sharedData.AbilityState, AbilityState.Ability),
                            new ExpectData<int>(sharedData.AbilityPool, 1),
                            new TaskSetLastKnownPlayerLocation(playerTransform),
                            new TaskAimAt(rb, weapon, playerTransform, enemyAnimator),
                            new TaskLookAt(playerTransform, rb, null),
                            new TaskSetMovementSpeed(agent, 0),
                            new RandomAttackMove(tasks[1]),
                            //tasksPool[4], //tasksPool.Length - 2
                            new TaskSetMovementSpeed(agent, 3.5f),
                            new SetData<AbilityState>(sharedData.AbilityState, AbilityState.Cooldown)
                        }),
                        new Sequence(new List<Node>
                        {
                            new ExpectData<AbilityState>(sharedData.AbilityState, AbilityState.Ability),
                            new ExpectData<int>(sharedData.AbilityPool, 2),
                            new TaskSetLastKnownPlayerLocation(playerTransform),
                            new TaskAimAt(rb, weapon, playerTransform, enemyAnimator),
                            new TaskLookAt(playerTransform, rb, null),
                            new TaskSetMovementSpeed(agent, 0),
                            new RandomAttackMove(tasks[2]),
                            //tasksPool[4], //tasksPool.Length - 2
                            new TaskSetMovementSpeed(agent, 3.5f),
                            new SetData<AbilityState>(sharedData.AbilityState, AbilityState.Cooldown)
                        })
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