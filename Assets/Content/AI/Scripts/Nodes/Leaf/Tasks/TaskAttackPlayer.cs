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

        private Animator _enemyAnimator;

        private Animator _muzzleFlashAnimator;

        // Time counter
        private float _cooldownTimeCounter;

        private static readonly int AimDirectionX = Animator.StringToHash("AimDirectionX");
        private static readonly int AimDirectionY = Animator.StringToHash("AimDirectionY");
        private static readonly int Shoot = Animator.StringToHash("Shoot");

        public TaskAttackPlayer(EnemyWeapon weapon, float cooldownTime, Animator enemyAnimator, Animator muzzleFlashAnimator)
        {
            _weapon = weapon;
            _cooldownTime = cooldownTime;
            _enemyAnimator = enemyAnimator;
            _muzzleFlashAnimator = muzzleFlashAnimator;
        }

        public override NodeState Evaluate()
        {
            state = NodeState.RUNNING;

            if (_enemyAnimator)
            {
                Vector3 aimDirection = _weapon.transform.right.normalized;

                _enemyAnimator.SetFloat(AimDirectionX, aimDirection.x);
                _enemyAnimator.SetFloat(AimDirectionY, aimDirection.y);
            }

            _cooldownTimeCounter += Time.fixedDeltaTime;
            if (_cooldownTimeCounter >= _cooldownTime)
            {
                _cooldownTimeCounter = 0f;
                state = NodeState.SUCCESS;
                _weapon.Fire();
                EventManager.OnEnemyShotFired.Trigger();

                if (_muzzleFlashAnimator)
                    _muzzleFlashAnimator.SetTrigger(Shoot);
            }

            return state;
        }
    }
}