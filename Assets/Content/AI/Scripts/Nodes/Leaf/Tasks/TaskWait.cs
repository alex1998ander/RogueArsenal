using UnityEngine;

namespace BehaviorTree
{
    public class TaskWait : Node
    {
        // Time to wait
        private static float _waitTime;

        // Time counter
        private static float _timeCounter;

        public TaskWait(float waitTime)
        {
            _waitTime = waitTime;
        }

        public override NodeState Evaluate()
        {
            state = NodeState.RUNNING;

            _timeCounter += Time.fixedDeltaTime;
            if (_timeCounter >= _waitTime)
            {
                _timeCounter = 0f;
                state = NodeState.SUCCESS;
            }

            return state;
        }
    }
}