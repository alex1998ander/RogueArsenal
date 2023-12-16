using Cinemachine;
using UnityEngine;

namespace BehaviorTree
{
    public class TaskDeleteGameObject: Node
    {
        private Transform _transform;
        public TaskDeleteGameObject(Transform transform)
        {
            this._transform = transform;
        }
        
        public override NodeState Evaluate()
        {
            MonoBehaviour.Destroy(_transform.parent);
            state = NodeState.SUCCESS;
            return state;
        }
    }
}