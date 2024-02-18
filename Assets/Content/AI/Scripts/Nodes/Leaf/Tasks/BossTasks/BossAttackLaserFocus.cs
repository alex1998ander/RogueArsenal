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

        private const float WaitTime = 3f;

        private float _timeDelayBeforeFinallySettingPosition = 0.3f;

        private float _timeCounterLaser = 0;

        private Transform _body;

        private bool _gotHitOnce = false;
        
        Vector2 _direction = Vector2.zero;

        private float _timeCounter;

        private int _waveCounter;

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

            LaserFocus();

            if (_waveCounter == Configuration.Boss_LaserRepetitions)
            {
                _waveCounter = 0;
                state = NodeState.SUCCESS;
            }

            return state;
        }

        void LaserFocus()
        {
            Vector3 laserStart = _body.position;
            Vector3 focusPos = _focusTarget.position;
            Vector3 laserEnd = focusPos + (focusPos - laserStart) * 3f;
            
            _lineRenderer.enabled = true;
            _lineRenderer.startWidth = 0.05f;
            _lineRenderer.endWidth = 0.05f;
            _timeCounterLaser += Time.fixedDeltaTime;
            
            if (_timeCounterLaser < ((WaitTime / 3) -  _timeDelayBeforeFinallySettingPosition))
            {
                _lineRenderer.SetPositions(new[] { laserStart, laserEnd });
            }
            
            if (_timeCounterLaser >= ((WaitTime / 3) -  _timeDelayBeforeFinallySettingPosition) && _direction == Vector2.zero)
            {
                _direction = new Vector2(_focusTarget.position.x, _focusTarget.position.y) - new Vector2(_body.position.x, _body.position.y);
                _lineRenderer.SetPositions(new[] { laserStart, laserEnd });
            }
            
            if (_timeCounterLaser >= (WaitTime/3))
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
            
            if (_timeCounterLaser >= WaitTime)
            {
                _lineRenderer.enabled = false;
                _timeCounterLaser = 0;
                _gotHitOnce = false;
                _direction = Vector2.zero;
                _waveCounter++;
            }
        }
    }
}