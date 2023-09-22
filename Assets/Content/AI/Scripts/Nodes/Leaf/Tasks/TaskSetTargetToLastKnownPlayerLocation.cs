using UnityEngine;

namespace BehaviorTree
{
    public class TaskSetTargetToLastKnownPlayerLocation : Node
    {
        public override NodeState Evaluate()
        {
            state = NodeState.FAILURE;

            if (sharedData.HasData(sharedData.LastKnownPlayerLocation))
            {
                state = NodeState.SUCCESS;
                Vector3 lastKnownPlayerLocation = GetData(sharedData.LastKnownPlayerLocation);
                SetData(sharedData.Target, lastKnownPlayerLocation);
            }

            return state;
        }
    }
}