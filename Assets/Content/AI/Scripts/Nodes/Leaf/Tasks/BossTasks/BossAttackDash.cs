using System.Collections;
using UnityEngine;

namespace BehaviorTree
{
    public class BossAttackDash : Node
    {
        private float _waitTime = 3f;

        private const float DashForce = 1200f;

        private float _timeCounter;
        
        private Transform _dashTarget;

        private Transform _body;
        
        private Rigidbody2D _rigidbody2D;

        private bool hasDashed = false;
        
        Vector2 _dashDir;

        public BossAttackDash(Transform body, Rigidbody2D rigidbody2D, Transform dashTarget)
        {
            this._body = body;
            this._rigidbody2D = rigidbody2D;
            this._dashTarget = dashTarget;
            _dashDir = Vector2.zero;
        }
        
        public override NodeState Evaluate()
        {
            state = NodeState.FAILURE;

            _timeCounter += Time.fixedDeltaTime;
            if (_timeCounter >= _waitTime/3 && _dashDir == Vector2.zero)
            {
                _dashDir = (_dashTarget.position - _body.position);
                Debug.DrawLine(_body.position, _dashTarget.position, Color.red , 2);
            }
            if (_timeCounter >= _waitTime/2 && !hasDashed)
            {
                _rigidbody2D.AddForce(_dashDir * DashForce);
                hasDashed = true;
            }
            if (_timeCounter >= _waitTime)
            {
                hasDashed = false;
                _dashDir = Vector2.zero;
                _timeCounter = 0f;
                state = NodeState.SUCCESS;
            }

            return state;
        }
    }
}