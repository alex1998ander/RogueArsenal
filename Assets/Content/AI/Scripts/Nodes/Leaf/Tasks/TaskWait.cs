using UnityEngine;

namespace BehaviorTree
{
    public class TaskWait : Node
    {
        private float _waitTime;

        private bool _critical;

        private float _timeCounter;

        public TaskWait(float waitTime, bool critical)
        {
            _waitTime = waitTime;
            _critical = critical;
        }

        public override NodeState Evaluate()
        {
            state = _critical ? NodeState.FAILURE : NodeState.RUNNING;

            Debug.Log("Time left: " + (_waitTime - _timeCounter));
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