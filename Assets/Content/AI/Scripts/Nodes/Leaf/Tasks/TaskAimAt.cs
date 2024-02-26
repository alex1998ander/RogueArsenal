using UnityEngine;
using UnityEngine.AI;

namespace BehaviorTree
{
    /// <summary>
    /// Task for the enemy to aim at a specific location.
    /// </summary>
    public class TaskAimAt : Node
    {
        // Rigidbody of the enemy
        private readonly Rigidbody2D _rb;

        // Weapon of enemy
        private readonly EnemyWeapon _weapon;

        // Transform of the player
        private readonly Transform _aimAtTransform;

        private readonly Animator _enemyAnimator;

        private readonly SpriteRenderer _weaponSprite;

        private readonly bool _adjustSprite;

        private static readonly int AimDirectionX = Animator.StringToHash("AimDirectionX");
        private static readonly int AimDirectionY = Animator.StringToHash("AimDirectionY");

        public TaskAimAt(Rigidbody2D rb, EnemyWeapon weapon, Transform aimAtTransform, Animator enemyAnimator, bool adjustSprite = true)
        {
            _rb = rb;
            _weapon = weapon;
            _aimAtTransform = aimAtTransform;
            _enemyAnimator = enemyAnimator;
            _adjustSprite = adjustSprite;

            _weaponSprite = weapon.GetComponentInChildren<SpriteRenderer>();
        }

        public override NodeState Evaluate()
        {
            Vector2 aimDirection = ((Vector2) _aimAtTransform.position - _rb.position).normalized;

            if (aimDirection != Vector2.zero)
            {
                if (_enemyAnimator)
                {
                    _enemyAnimator.SetFloat(AimDirectionX, aimDirection.x);
                    _enemyAnimator.SetFloat(AimDirectionY, aimDirection.y);
                }

                // Calculate angle
                float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
                _weapon.transform.rotation = Quaternion.Euler(0, 0, angle);

                if (_adjustSprite)
                {
                    // When the enemy is aiming left, flip weapon so it's not heads-down
                    _weaponSprite.flipY = aimDirection.x < 0.0f;
                    // When the enemy is aiming up, adjust sorting order so weapon is behind enemy
                    _weaponSprite.sortingOrder = angle >= 45.0 && angle <= 135.0f ? -1 : 1;
                }
            }

            state = NodeState.SUCCESS;
            return state;
        }
    }
}