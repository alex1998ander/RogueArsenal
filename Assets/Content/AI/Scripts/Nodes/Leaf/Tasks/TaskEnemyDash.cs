using UnityEngine;

namespace BehaviorTree
{
    public class TaskEnemyDash : Node
    {
        private Transform _target;
        private Rigidbody2D _rb;
        private Transform _body;

        public TaskEnemyDash(Transform target, Rigidbody2D rb, Transform body)
        {
            this._target = target;
            this._rb = rb;
            this._body = body;
        }

        public override NodeState Evaluate()
        {
            Vector2 dashDirection = (_target.position - _body.position).normalized;
            _rb.AddForce(dashDirection * Configuration.Enemy_DashForce, ForceMode2D.Impulse);
            state = NodeState.SUCCESS;
            return state;
        }
    }
}