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
        private Collider2D _damageCollider;
        private Collider2D _bossCollider;

        private float _waitTime = 3f;
        private float _timeCounter;

        private bool _landPosSet = false;
        //Vector3 _landPos = Vector3.zero;

        public BossAttackStomp(Transform body, Transform stompTarget, SpriteRenderer bossVisual, Collider2D damageCollider, Collider2D bossCollider)
        {
            this._body = body;
            this._stompTarget = stompTarget;
            this._bossVisual = bossVisual;
            this._damageCollider = damageCollider;
            this._bossCollider = bossCollider;
        }

        public override NodeState Evaluate()
        {
            state = NodeState.FAILURE;

            _bossVisual.enabled = false;
            _bossCollider.enabled = false;

            _timeCounter += Time.fixedDeltaTime;
            if (_timeCounter >= _waitTime / 2 && !_landPosSet)
            {
                //_landPos = _stompTarget.position;
                _body.position = _stompTarget.position;
                _landPosSet = true;
            }

            if (_timeCounter >= _waitTime - 0.1)
            {
                _bossVisual.enabled = true;
                _damageCollider.enabled = true;
                _bossCollider.enabled = true;
            }

            if (_timeCounter >= _waitTime)
            {
                _timeCounter = 0f;
                _damageCollider.enabled = true;
                _bossCollider.enabled = true;
                _landPosSet = false;
                
                
                _damageCollider.enabled = false;
                //_body.position = _landPos;
                state = NodeState.SUCCESS;
            }

            return state;
        }
    }
}