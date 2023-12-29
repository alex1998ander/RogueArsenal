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
        [SerializeField] private Collider2D damageCollider;
        [SerializeField] private GameObject mine;
        [SerializeField] private GameObject turret;
        [SerializeField] private GameObject clone;
        [SerializeField] private GameObject shieldGenerator;
        [SerializeField] private GameObject bullet;
        [SerializeField] private GameObject shockWave;
        [SerializeField] private GameObject ui;

        protected override Node SetupTree()
        {
            base.SetupTree();
            
            const float attackSpeed = 0.75f;
            const float abilityCooldown = 5f;

            Collider2D bossCollider2D = GetComponent<BoxCollider2D>();
            LineRenderer lineRenderer = GetComponent<LineRenderer>();
            SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            Transform transform = this.transform;
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            Transform playerTransform = GameObject.Find("Player").GetComponent<Transform>();
            EnemyWeapon weapon = GetComponentInChildren<EnemyWeapon>();
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            Animator animator = GetComponentInChildren<Animator>();

            //For the nav mesh agent
            agent.updateRotation = false;
            agent.updateUpAxis = false;

            //All abilities the boss can have
            Node[] tasksPool = new Node[]
            {
                new BossAttackDash(transform, rb, playerTransform, this.damageCollider),
                new BossAttackStomp(transform, playerTransform, spriteRenderer, damageCollider, bossCollider2D, ui),
                new BossAttackLaserFocus(lineRenderer, playerTransform, transform), new BossAttackSpawnObject(playerTransform, mine, new Vector3(0.5f, 0.5f, 0.5f)),
                new BossAttackSpawnObject(transform, turret), new BossAttackSpawnObject(transform, clone, new Vector3(1.5f, 1.5f, 1.5f)),
                new BossAttack360Shot(transform, bullet), new BossAttackShield(shieldGenerator, bossCollider2D), new BossAttackShockwave(shockWave)
            };
            
            damageCollider.enabled = false;
            shieldGenerator.SetActive(false);
            shockWave.SetActive(false);

            SharedData sharedData = new SharedData();
            sharedData.SetData(sharedData.AbilityState, AbilityState.None);
            sharedData.SetData(sharedData.RandomAbility, -1);

            //Each round the boss gets three of the abilities that can be used 
            int[] taskCounter = {-1, -1, -1};
            while ((taskCounter[0] == taskCounter[1]) || (taskCounter[1] == taskCounter[2]) || (taskCounter[0] == taskCounter[2]))
            {
                for (int i = 0; i < taskCounter.Length; i++)
                {
                    taskCounter[i] = Random.Range(0, tasksPool.Length - 1);
                }
            }
            
            Node[] tasks = {tasksPool[taskCounter[0]], tasksPool[taskCounter[1]], tasksPool[taskCounter[2]]};

            Node root = new Selector(new List<Node>
            {
                // Case: Enemy is aware of player
                new Sequence(new List<Node>
                {
                    new CheckIsAwareOfPlayer(),
                    new Selector(new List<Node>
                    {
                        // Case: Enemy can see player and attacks him
                        new Sequence(new List<Node>
                        {
                            new Inverter(new ExpectData<AbilityState>(sharedData.AbilityState, AbilityState.Ability)),
                            new TaskSetLastKnownPlayerLocation(playerTransform),
                            new TaskLookAt(playerTransform, rb, null),
                            new TaskPickTargetAroundTransforms(playerTransform, minDistanceFromPlayer,
                                maxDistanceFromPlayer),
                            new TaskMoveToTarget(rb, agent, animator, 1f),
                            //new CheckIsAtTarget(),
                            new TaskAttackPlayer(weapon, attackSpeed),
                            new ChooseRandomAttackMove(tasks.Length),
                            new TaskWait(abilityCooldown, true),
                            new SetData<AbilityState>(sharedData.AbilityState, AbilityState.Ability)
                        }),
                        new Sequence(new List<Node>
                        {
                            new ExpectData<AbilityState>(sharedData.AbilityState, AbilityState.Ability),
                            new TaskSetLastKnownPlayerLocation(playerTransform),
                            new TaskLookAt(playerTransform, rb, null),
                            new TaskSetMovementSpeed(agent, 0),
                            new RandomAttackMove(tasks),
                            //tasksPool[3], //tasksPool.Length - 2
                            new TaskSetMovementSpeed(agent, 3.5f),
                            new SetData<AbilityState>(sharedData.AbilityState, AbilityState.Cooldown)
                        }),
                        // Case: Enemy just heard the player shoot
                        new Sequence(new List<Node>
                        {
                            new Inverter(new ExpectData<AbilityState>(sharedData.AbilityState, AbilityState.Ability)),
                            new HasData<bool>(sharedData.HasHeardPlayerShot),
                            new TaskSetLastKnownPlayerLocation(playerTransform),
                            new TaskSetTargetToLastKnownPlayerLocation(),
                            new ClearData<bool>(sharedData.HasHeardPlayerShot)
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