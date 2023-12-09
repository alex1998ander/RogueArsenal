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

        public TaskAimAt(Rigidbody2D rb, EnemyWeapon weapon, Transform aimAtTransform)
        {
            _rb = rb;
            _weapon = weapon;
            _aimAtTransform = aimAtTransform;
        }

        public override NodeState Evaluate()
        {
            Vector2 aimDirection = ((Vector2) _aimAtTransform.position - _rb.position).normalized;

            if (aimDirection != Vector2.zero)
            {
                // Calculate angle
                float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
                // -90f to account for "forwards" of the enemy being the up vector and not the right vector
                angle -= 90f;
                _weapon.transform.rotation = Quaternion.Euler(0, 0, angle);
            }

            state = NodeState.SUCCESS;
            return state;
        }
    }
}