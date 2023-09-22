using UnityEngine;
using UnityEngine.AI;

namespace BehaviorTree
{
    /// <summary>
    /// Task for the enemy to look at a specific location.
    /// </summary>
    public class TaskLookAt : Node
    {
        private NavMeshAgent _agent;

        // Rigidbody of the enemy
        private Rigidbody2D _rb;

        // Transform of the player
        private Transform _lookAtTransform;

        public TaskLookAt(Rigidbody2D rb, NavMeshAgent agent)
        {
            _rb = rb;
            _agent = agent;
        }

        public TaskLookAt(Rigidbody2D rb, Transform lookAtTransform)
        {
            _rb = rb;
            _lookAtTransform = lookAtTransform;
        }

        public override NodeState Evaluate()
        {
            Vector2 lookAtDirection = Vector2.zero;
            if (_lookAtTransform)
            {
                lookAtDirection = ((Vector2) _lookAtTransform.position - _rb.position).normalized;
            }
            else if (_agent)
            {
                lookAtDirection = _agent.velocity;
            }

            if (lookAtDirection != Vector2.zero)
            {
                // Calculate angle
                float angle = Mathf.Atan2(lookAtDirection.y, lookAtDirection.x) * Mathf.Rad2Deg;
                // -90f to account for "forwards" of the enemy being the up vector and not the right vector
                angle -= 90f;
                _rb.rotation = angle;
            }

            state = NodeState.SUCCESS;
            return state;
        }
    }
}