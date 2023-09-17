using UnityEngine;

namespace BehaviorTree
{
    /// <summary>
    /// Lets the enemy wait for a given amount of time in preparation before shooting
    /// </summary>
    public class TaskAim : Node
    {
        // Time to aim
        private static float _aimTime = 0.5f;

        // Time counter
        private static float _aimCounter;

        public override NodeState Evaluate()
        {
            _aimCounter += Time.fixedDeltaTime;
            if (_aimCounter >= _aimTime)
            {
                SetData(sharedData.IsAiming, false);
                _aimCounter = 0f;
                state = NodeState.SUCCESS;
                return state;
            }

            SetData(sharedData.IsAiming, true);
            state = NodeState.FAILURE;

            return state;
        }
    }
}