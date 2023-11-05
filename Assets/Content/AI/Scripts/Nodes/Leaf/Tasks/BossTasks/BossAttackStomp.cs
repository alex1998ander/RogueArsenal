using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

namespace BehaviorTree
{
    public class BossAttackStomp : Node
    {
        private Transform _stompTarget;
        private SpriteRenderer _bossVisual;
        private Transform _body;
        private SpriteRenderer _shadow;
        private Collider2D _damageCollider;
        
        private float _waitTime = 3f;
        private float _timeCounter;

        public BossAttackStomp(Transform body, Transform stompTarget, SpriteRenderer bossVisual, SpriteRenderer shadow, Collider2D damageCollider)
        {
            this._body = body;
            this._stompTarget = stompTarget;
            this._bossVisual = bossVisual;
            this._shadow = shadow;
            this._damageCollider = damageCollider;
        }
        
        public override NodeState Evaluate()
        {
            Vector3 landPos = _body.transform.position;
            state = NodeState.FAILURE;
            
            _bossVisual.enabled = false;
            
            _timeCounter += Time.fixedDeltaTime;
            if (_timeCounter >= _waitTime/2)
            {
                landPos = _stompTarget.position;
                _shadow.enabled = true;
            }
            if (_timeCounter >= _waitTime)
            {
                _damageCollider.enabled = true;
                _timeCounter = 0f;
                _bossVisual.enabled = true;
                _shadow.enabled = false;
                _body.position = landPos;
                state = NodeState.SUCCESS;
                _damageCollider.enabled = false;
            }

            return state;
        }
    }
}
