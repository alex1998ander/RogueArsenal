using UnityEngine.AI;

namespace BehaviorTree
{
    public class TaskSetMovementSpeed : Node
    {
        private NavMeshAgent _agent;

        private float _movementSpeed;

        public TaskSetMovementSpeed(NavMeshAgent agent, float movementSpeed)
        {
            _agent = agent;
            _movementSpeed = movementSpeed;
        }

        public override NodeState Evaluate()
        {
            _agent.speed = _movementSpeed;

            state = NodeState.SUCCESS;
            return state;
        }
    }
}