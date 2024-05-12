using UnityEngine;

namespace BehaviorTree
{
    public class TaskSetGameObjectLayer : Node
    {
        private readonly GameObject _gameObject;
        private readonly int _layer;

        public TaskSetGameObjectLayer(GameObject gameObject, int layer)
        {
            _gameObject = gameObject;
            _layer = layer;
        }

        public override NodeState Evaluate()
        {
            _gameObject.layer = _layer;

            state = NodeState.SUCCESS;
            return state;
        }
    }
}