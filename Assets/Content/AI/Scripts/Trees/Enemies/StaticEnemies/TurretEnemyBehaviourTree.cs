using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class TurretEnemyBehaviourTree : EnemyBehaviourTree
    {
        protected override Node SetupTree()
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            Transform playerTransform = GameObject.Find("Player").GetComponent<Transform>();
            EnemyWeapon weapon = GetComponentInChildren<EnemyWeapon>();

            SharedData sharedData = new SharedData();

            Node root = new Sequence(new List<Node>()
            {
                new Inverter(new CheckHasState(sharedData.IsStunned)),
                new CheckPlayerVisible(rb, playerTransform, wallLayer),
                new TaskLookAt(rb, playerTransform),
                new TaskWait(1f, false),
                new TaskAttackPlayer(weapon, 1f),
            });

            root.SetupSharedData(sharedData);

            return root;
        }

        /// <summary>
        /// Override: Turret enemies can't be thrown
        /// </summary>
        /// <returns>Always false</returns>
        public override bool ThrowCharacter()
        {
            return false;
        }
    }
}