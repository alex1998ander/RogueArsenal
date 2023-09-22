using UnityEngine;

namespace BehaviorTree
{
    public class TaskSetLastKnownPlayerLocation : Node
    {
        private Transform _playerTransform;

        public TaskSetLastKnownPlayerLocation(Transform playerTransform)
        {
            _playerTransform = playerTransform;
        }

        public override NodeState Evaluate()
        {
            SetData(sharedData.LastKnownPlayerLocation, _playerTransform.position);

            state = NodeState.SUCCESS;
            return NodeState.SUCCESS;
        }
    }
}