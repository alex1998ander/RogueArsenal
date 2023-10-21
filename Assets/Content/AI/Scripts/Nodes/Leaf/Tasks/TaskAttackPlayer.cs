using UnityEngine;

namespace BehaviorTree
{
    /// <summary>
    /// Task to attack the player with the enemy's weapon
    /// </summary>
    public class TaskAttackPlayer : Node
    {
        // The weapon of the enemy
        private EnemyWeapon _weapon;

        // Time to wait
        private static float _cooldownTime;

        // Time counter
        private static float _cooldownTimeCounter;

        public TaskAttackPlayer(EnemyWeapon weapon, float cooldownTime)
        {
            _weapon = weapon;
            _cooldownTime = cooldownTime;
        }

        public override NodeState Evaluate()
        {
            state = NodeState.RUNNING;

            _cooldownTimeCounter += Time.fixedDeltaTime;
            if (_cooldownTimeCounter >= _cooldownTime)
            {
                _cooldownTimeCounter = 0f;
                state = NodeState.SUCCESS;
                _weapon.Fire();
                EventManager.OnEnemyShotFired.Trigger();
            }

            return state;
        }
    }
}