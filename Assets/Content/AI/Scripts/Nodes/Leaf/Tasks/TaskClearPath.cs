using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorTree
{
    public class TaskClearPath : Node
    {
        private NavMeshAgent _agent;

        public TaskClearPath(NavMeshAgent agent)
        {
            _agent = agent;
        }

        public override NodeState Evaluate()
        {
            _agent.ResetPath();
            _agent.velocity = Vector3.zero;
            state = NodeState.SUCCESS;
            return state;
        }
    }
}