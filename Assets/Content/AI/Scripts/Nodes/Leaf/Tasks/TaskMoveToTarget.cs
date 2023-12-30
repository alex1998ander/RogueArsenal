using UnityEngine;
using UnityEngine.AI;

namespace BehaviorTree
{
    /// <summary>
    /// Task to move at the pathfinding target
    /// </summary>
    public class TaskMoveToTarget : Node
    {
        // Rigidbody of enemy
        private Rigidbody2D _rb;

        // Position of pathfinding target
        private Vector3 _target;

        // Old position of pathfinding target
        private Vector3 _oldTarget;

        // Nav Mesh Agent
        private NavMeshAgent _agent;

        // Animator of enemy sprite
        private readonly Animator _animator;

        // Distance to the pathfinding target to count as having reached it
        private float _targetReachedDistance;

        private static readonly int Running = Animator.StringToHash("Running");
        private static readonly int MovementDirectionX = Animator.StringToHash("MovementDirectionX");
        private static readonly int MovementDirectionY = Animator.StringToHash("MovementDirectionY");

        public TaskMoveToTarget(Rigidbody2D rb, NavMeshAgent agent, Animator animator, float targetReachedDistance)
        {
            _rb = rb;
            _agent = agent;
            _animator = animator;
            _targetReachedDistance = targetReachedDistance;
        }

        public override NodeState Evaluate()
        {
            Vector3 newTarget = GetData(sharedData.Target);
            _agent.SetDestination(newTarget);
            SetData(sharedData.IsAtTarget, false);

            if (_animator)
            {
                _animator.SetBool(Running, _agent.velocity.magnitude > 0.1f);

                if (_agent.velocity.magnitude > 0.1f)
                {
                    _animator.SetFloat(MovementDirectionX, _agent.velocity.x);
                    _animator.SetFloat(MovementDirectionY, _agent.velocity.y);
                }
            }

            state = NodeState.RUNNING;
            if ((_rb.position - (Vector2) newTarget).magnitude < _targetReachedDistance)
            {
                state = NodeState.SUCCESS;
                SetData(sharedData.IsAtTarget, true);
                ClearData(sharedData.Target);
            }

            return state;
        }
    }
}