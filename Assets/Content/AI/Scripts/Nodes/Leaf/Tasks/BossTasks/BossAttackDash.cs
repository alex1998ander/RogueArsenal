using System.Collections;
using UnityEngine;

namespace BehaviorTree
{
    public class BossAttackDash : Node
    {
        private Transform _body;
        private Rigidbody2D _rigidbody2D;
        private Transform _dashTarget;
        private ParticleSystem _chargingEffect;
        private ParticleSystem _dashingEffect;
        private LightFader _chargeLightFader;
        private float _chargeLightMaxIntensity;

        private float _timeCounter;
        private bool hasDashed;
        Vector2 _dashDir;

        public BossAttackDash(Transform body, Rigidbody2D rigidbody2D, Transform dashTarget, ParticleSystem chargingEffect, ParticleSystem dashingEffect, LightFader chargeLightFader, float chargeLightMaxIntensity)
        {
            _body = body;
            _rigidbody2D = rigidbody2D;
            _dashTarget = dashTarget;
            _chargingEffect = chargingEffect;
            _dashingEffect = dashingEffect;
            _chargeLightFader = chargeLightFader;
            _chargeLightMaxIntensity = chargeLightMaxIntensity;

            _chargeLightFader.MinLightIntensity = 0f;
            _chargeLightFader.MaxLightIntensity = _chargeLightMaxIntensity;

            ParticleSystem.MainModule charging = _chargingEffect.main;
            charging.duration = Configuration.Boss_DashPrepareTime;

            ParticleSystem.MainModule dashing = _dashingEffect.main;
            dashing.duration = Configuration.Boss_DashMidChargeTime;

            _dashDir = Vector2.zero;
        }

        public override NodeState Evaluate()
        {
            state = NodeState.FAILURE;

            _timeCounter += Time.fixedDeltaTime;

            switch (_timeCounter)
            {
                case <= Configuration.Boss_DashPrepareTime:
                {
                    _dashDir = _dashTarget.position - _body.position;
                    if (!_chargingEffect.isPlaying)
                    {
                        _chargingEffect.Play();
                        _chargeLightFader.IntensityChange = _chargeLightMaxIntensity / Configuration.Boss_DashPrepareTime;
                    }

                    break;
                }
                case <= Configuration.Boss_DashPrepareTime + Configuration.Boss_DashMidChargeTime:
                {
                    if (!hasDashed)
                    {
                        _rigidbody2D.AddForce(_dashDir * Configuration.Boss_DashForce);
                        _dashingEffect.Play();
                        _chargeLightFader.IntensityChange = -_chargeLightMaxIntensity / (Configuration.Boss_DashMidChargeTime * 2);
                        hasDashed = true;
                    }

                    break;
                }
                case <= Configuration.Boss_DashPrepareTime + Configuration.Boss_DashMidChargeTime + Configuration.Boss_DashPostChargeTime:
                {
                    hasDashed = false;
                    _timeCounter = 0f;
                    state = NodeState.SUCCESS;
                    break;
                }
            }

            return state;
        }
    }
}