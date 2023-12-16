using UnityEngine;

namespace BehaviorTree
{
    public class TaskLookAt : Node
    {
        // Transform to look at
        private readonly Transform _lookAtTransform;

        // Rigidbody of enemy
        private readonly Rigidbody2D _rb;

        // Animator of enemy
        private readonly Animator _animator;

        private static readonly int MovementDirectionX = Animator.StringToHash("MovementDirectionX");
        private static readonly int MovementDirectionY = Animator.StringToHash("MovementDirectionY");

        public TaskLookAt(Transform lookAtTransform, Rigidbody2D rb, Animator animator)
        {
            _lookAtTransform = lookAtTransform;
            _rb = rb;
            _animator = animator;
        }

        public override NodeState Evaluate()
        {
            Vector2 lookAtDirection = ((Vector2) _lookAtTransform.position - _rb.position).normalized;

            if (_animator && lookAtDirection != Vector2.zero)
            {
                _animator.SetFloat(MovementDirectionX, lookAtDirection.x);
                _animator.SetFloat(MovementDirectionY, lookAtDirection.y);
            }

            state = NodeState.SUCCESS;
            return state;
        }
    }
}