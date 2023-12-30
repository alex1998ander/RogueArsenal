using System.Collections;
using BehaviorTree;
using UnityEngine;

namespace BehaviorTree
{
    [RequireComponent(typeof(LineRenderer))]
    
    public class BossAttackLaserFocus : Node
    {
        private Transform _focusTarget;

        private LineRenderer _lineRenderer;

        private float _waitTime = 3f;

        private float _timeCounter = 0;

        private Transform _body;

        private bool _gotHitOnce = false;
        
        Vector2 _direction = Vector2.zero;

        public BossAttackLaserFocus(LineRenderer lineRenderer, Transform focusTarget, Transform body)
        {
            this._lineRenderer = lineRenderer;
            _lineRenderer.startColor = Color.red;
            _lineRenderer.endColor = Color.red;
            _lineRenderer.enabled = false;
            this._focusTarget = focusTarget;
            this._body = body;
        }

        public override NodeState Evaluate()
        {
            state = NodeState.FAILURE;

            Vector3 laserStart = _body.position;
            Vector3 focusPos = _focusTarget.position;
            Vector3 laserEnd = focusPos + (focusPos - laserStart) * 3f;
            
            _lineRenderer.enabled = true;
            _lineRenderer.startWidth = 0.05f;
            _lineRenderer.endWidth = 0.05f;

            if (_timeCounter < ((_waitTime / 3) -  0.2))
            {
                _lineRenderer.SetPositions(new[] { laserStart, laserEnd });
            }

            _timeCounter += Time.fixedDeltaTime;
            if (_timeCounter >= ((_waitTime / 3) -  0.2) && _direction == Vector2.zero)
            {
                _direction = new Vector2(_focusTarget.position.x, _focusTarget.position.y) - new Vector2(_body.position.x, _body.position.y);
                _lineRenderer.SetPositions(new[] { laserStart, laserEnd });
            }
            
            if (_timeCounter >= (_waitTime/3))
            {
                _lineRenderer.startWidth = 1f;
                _lineRenderer.endWidth = 1f;
                int layerMaskToInt = LayerMask.GetMask("Player_Trigger");
                RaycastHit2D[] hits = new RaycastHit2D[1];
                int gotHits = Physics2D.BoxCastNonAlloc(new Vector2(_body.position.x, _body.position.y), new Vector2(2, 10), 0, _direction, hits, 20, layerMaskToInt);
                if (gotHits == 1  && !_gotHitOnce)
                {
                    hits[0].transform.GetComponent<PlayerHealth>().InflictDamage(Configuration.Boss_LaserDamage, true);
                    _gotHitOnce = true;
                }
            }
            
            if (_timeCounter >= _waitTime)
            {
                _lineRenderer.enabled = false;
                _timeCounter = 0;
                _gotHitOnce = false;
                _direction = Vector2.zero;
                state = NodeState.SUCCESS;
            }

            return state;
        }
    }
}