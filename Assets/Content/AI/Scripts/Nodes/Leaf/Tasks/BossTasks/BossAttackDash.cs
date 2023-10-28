using System.Collections;
using UnityEngine;

namespace BehaviorTree
{
    public class BossAttackDash : Node
    {
        private float _waitTime = 1f;

        private float _timeCounter;
        
        private Transform _dashTarget;

        private Transform _body;
        
        private Rigidbody2D _rigidbody2D;

        public BossAttackDash(Transform body, Rigidbody2D rigidbody2D, Transform dashTarget)
        {
            this._body = body;
            this._rigidbody2D = rigidbody2D;
            this._dashTarget = dashTarget;
        }
        
        public override NodeState Evaluate()
        {
            state = NodeState.FAILURE;
            
            Vector2 dashDir = (_dashTarget.position - _body.position);

            _timeCounter += Time.fixedDeltaTime;
            if (_timeCounter >= _waitTime)
            {
                _timeCounter = 0f;
                _rigidbody2D.AddForce(dashDir * 1500f);
                state = NodeState.SUCCESS;
            }

            return state;
        }
    }
}