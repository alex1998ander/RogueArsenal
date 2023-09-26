using UnityEngine.AI;

namespace BehaviorTree
{
    public class TaskSetMovementAcceleration : Node
    {
        private NavMeshAgent _agent;

        private float _movementAcceleration;

        public TaskSetMovementAcceleration(NavMeshAgent agent, float movementAcceleration)
        {
            _agent = agent;
            _movementAcceleration = movementAcceleration;
        }

        public override NodeState Evaluate()
        {
            _agent.acceleration = _movementAcceleration;

            state = NodeState.SUCCESS;
            return state;
        }
    }
}