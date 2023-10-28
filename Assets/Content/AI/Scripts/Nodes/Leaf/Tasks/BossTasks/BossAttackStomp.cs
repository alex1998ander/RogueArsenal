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
        
        private float _waitTime = 3f;
        private float _timeCounter;

        public BossAttackStomp(Transform body, Transform stompTarget, SpriteRenderer bossVisual)
        {
            this._body = body;
            this._stompTarget = stompTarget;
            this._bossVisual = bossVisual;
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
            }
            if (_timeCounter >= _waitTime)
            {
                _timeCounter = 0f;
                _bossVisual.enabled = true;
                _body.position = landPos;
                state = NodeState.SUCCESS;
            }

            return state;
        }
    }
}
