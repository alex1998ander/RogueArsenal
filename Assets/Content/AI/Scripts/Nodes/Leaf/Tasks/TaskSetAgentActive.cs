using UnityEngine.AI;

namespace BehaviorTree
{
    public class TaskSetAgentActive : Node
    {
        private readonly NavMeshAgent _agent;
        private readonly bool _active;

        public TaskSetAgentActive(NavMeshAgent agent, bool active)
        {
            _agent = agent;
            _active = active;
        }

        public override NodeState Evaluate()
        {
            _agent.enabled = _active;

            state = NodeState.SUCCESS;
            return state;
        }
    }
}