using UnityEngine;
using UnityEngine.AI;

namespace BehaviorTree
{
    public class TaskPickTargetBehindTransform : Node
    {
        private readonly NavMeshAgent _agent;
        private readonly Transform _targetTransform;
        private readonly float _distanceFromTarget;

        public TaskPickTargetBehindTransform(NavMeshAgent agent, Transform targetTransform, float distanceFromTarget)
        {
            _agent = agent;
            _targetTransform = targetTransform;
            _distanceFromTarget = distanceFromTarget;
        }

        public override NodeState Evaluate()
        {
            Vector3 direction = _agent.transform.up;
            Debug.Log("direction" + direction);
            Vector3 positionBehindTransform = _targetTransform.position + direction * _distanceFromTarget;

            SetData(sharedData.IsAtTarget, false);
            SetData(sharedData.Target, positionBehindTransform);

            state = NodeState.SUCCESS;
            return NodeState.SUCCESS;
        }
    }
}