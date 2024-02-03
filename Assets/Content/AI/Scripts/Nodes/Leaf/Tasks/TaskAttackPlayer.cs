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
        private float _cooldownTime;

        private Animator _animator;

        // Time counter
        private float _cooldownTimeCounter;

        private static readonly int AimDirectionX = Animator.StringToHash("AimDirectionX");
        private static readonly int AimDirectionY = Animator.StringToHash("AimDirectionY");

        public TaskAttackPlayer(EnemyWeapon weapon, float cooldownTime, Animator animator)
        {
            _weapon = weapon;
            _cooldownTime = cooldownTime;
            _animator = animator;
        }

        public override NodeState Evaluate()
        {
            state = NodeState.RUNNING;

            if (_animator)
            {
                Vector3 aimDirection = _weapon.transform.right.normalized;

                _animator.SetFloat(AimDirectionX, aimDirection.x);
                _animator.SetFloat(AimDirectionY, aimDirection.y);
            }

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