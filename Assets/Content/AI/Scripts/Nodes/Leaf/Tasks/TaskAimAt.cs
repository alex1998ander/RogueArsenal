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
        private Rigidbody2D _rb;

        // Weapon of enemy
        private EnemyWeapon _weapon;

        // Transform of the player
        private Transform _aimAtTransform;

        private SpriteRenderer _weaponSprite;

        public TaskAimAt(Rigidbody2D rb, EnemyWeapon weapon, Transform aimAtTransform)
        {
            _rb = rb;
            _weapon = weapon;
            _aimAtTransform = aimAtTransform;

            _weaponSprite = weapon.GetComponentInChildren<SpriteRenderer>();
        }

        public override NodeState Evaluate()
        {
            Vector2 aimDirection = ((Vector2) _aimAtTransform.position - _rb.position).normalized;

            if (aimDirection != Vector2.zero)
            {
                // Calculate angle
                float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
                _weapon.transform.rotation = Quaternion.Euler(0, 0, angle);

                // When the enemy is aiming left, flip weapon so it's not heads-down
                _weaponSprite.flipY = aimDirection.x < 0.0f;
                // When the enemy is aiming up, adjust sorting order so weapon is behind enemy
                _weaponSprite.sortingOrder = angle >= 45.0 && angle <= 135.0f ? -1 : 1;
            }

            state = NodeState.SUCCESS;
            return state;
        }
    }
}