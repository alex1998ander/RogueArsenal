using System;
using UnityEngine;

    public class LaserBarrier: MonoBehaviour
    {
        private LineRenderer _lineRenderer;
        private bool _gotHitOnce = false;
        private RaycastHit2D[] _hits = new RaycastHit2D[1];
        private int _layerMaskToInt;
        private void Start()
        {
            _layerMaskToInt = LayerMask.GetMask("Player");
            _lineRenderer = GetComponent<LineRenderer>();  
        }

        private void Update()
        {
            _lineRenderer.enabled = true;
            _lineRenderer.startColor = Color.red;
            _lineRenderer.endColor = Color.red;
            _lineRenderer.startWidth = 0.5f;
            _lineRenderer.endWidth = 0.5f;
            _lineRenderer.SetPositions(new[] { transform.position, transform.position + new Vector3(6,0,0) });
            int gotHits = Physics2D.BoxCastNonAlloc(transform.position, new Vector2(6, 0.5f), 0, new Vector2(1,0), _hits, 20, _layerMaskToInt);
            if (gotHits == 1  && !_gotHitOnce)
            {
                _hits[0].transform.GetComponent<PlayerHealth>().InflictDamage(99, true);
                Debug.Log("Hit");
                _gotHitOnce = true;
            }
        }
    }
