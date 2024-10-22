﻿using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class TutorialTurretEnemyBehaviourTree : EnemyBehaviourTree
    {
        private float _attackSpeed = 0.15f;

        protected override Node SetupTree()
        {
            base.SetupTree();
            EnemyWeapon weapon = GetComponentInChildren<EnemyWeapon>();

            SharedData sharedData = new SharedData();

            Node root = new Sequence(new List<Node>()
            {
                new TaskAttackPlayer(weapon, _attackSpeed),
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