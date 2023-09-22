using UnityEngine;

namespace BehaviorTree
{
    public class Logger : Node
    {
        private string _logMessage;

        public Logger(string logMessage)
        {
            _logMessage = logMessage;
        }

        public override NodeState Evaluate()
        {
            Debug.Log(_logMessage);
            state = NodeState.SUCCESS;
            return state;
        }
    }
}