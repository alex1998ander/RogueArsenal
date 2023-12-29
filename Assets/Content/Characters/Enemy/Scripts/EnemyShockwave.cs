using System;
using UnityEngine;

namespace Content.Characters.Enemy.Scripts
{
    public class EnemyShockwave : MonoBehaviour
    {
        private const float Radius = 3;
        private const int PlayerForce = 15000;
        private const int PlayerDamage = 5;

        float _radiusFactor = 0;
        private Transform _body;
        private bool _gotHitOnce = false;
        private ParticleSystem _shockwave;
        private CircleCollider2D _shockwaveCollider;

        void OnEnable()
        {
            _shockwave.Play();
            _radiusFactor = 0;
            _gotHitOnce = false;
            _shockwaveCollider.radius = 0.0001f;
        }

        public void Awake()
        {
            _shockwave = GetComponent<ParticleSystem>();
            _shockwaveCollider = GetComponent<CircleCollider2D>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") && !_gotHitOnce)
            {
                Vector2 forceDirection = (other.transform.position - transform.position);
                other.transform.gameObject.GetComponent<Rigidbody2D>().AddForce(forceDirection.normalized * PlayerForce);
                other.transform.GetComponent<PlayerHealth>().InflictDamage(PlayerDamage, true);
                _gotHitOnce = true;
            }
        }

        private void Update()
        {
            if (!this.enabled) return;

            _radiusFactor += Time.deltaTime;
            if (_radiusFactor < 1)
            {
                _shockwaveCollider.radius = Radius * _radiusFactor;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}