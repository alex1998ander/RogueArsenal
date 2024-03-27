using System.Collections;
using UnityEngine;

namespace BehaviorTree
{
    public class BossAttackDash : Node
    {
        private Transform _body;
        private Rigidbody2D _rigidbody2D;
        private Transform _dashTarget;
        private BoxCollider2D _contactDamageZone;
        private ParticleSystem _chargingEffect;
        private ParticleSystem _dashingEffect;
        private LightFader _chargeLightFader;
        private float _chargeLightMaxIntensity;

        private float _timeCounter;
        private bool hasDashed;
        Vector2 _dashDir;

        public BossAttackDash(Transform body, Rigidbody2D rigidbody2D, Transform dashTarget, BoxCollider2D contactDamageZone, ParticleSystem chargingEffect, ParticleSystem dashingEffect, LightFader chargeLightFader, float chargeLightMaxIntensity)
        {
            _body = body;
            _rigidbody2D = rigidbody2D;
            _dashTarget = dashTarget;
            _contactDamageZone = contactDamageZone;
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
                    _dashDir = (_dashTarget.position - _body.position).normalized;
                    if (!_chargingEffect.isPlaying)
                    {
                        _chargingEffect.Play();
                        _chargeLightFader.IntensityChange = _chargeLightMaxIntensity / Configuration.Boss_DashPrepareTime;
                        sharedData.SetData(sharedData.ChargeState, ChargeState.PreCharge);
                    }

                    break;
                }
                case <= Configuration.Boss_DashPrepareTime + Configuration.Boss_DashMidChargeTime:
                {
                    if (!hasDashed)
                    {
                        _contactDamageZone.enabled = true;
                        _rigidbody2D.AddForce(_dashDir * Configuration.Boss_DashForce, ForceMode2D.Impulse);
                        _dashingEffect.Play();
                        _chargeLightFader.IntensityChange = -_chargeLightMaxIntensity / (Configuration.Boss_DashMidChargeTime * 2);
                        hasDashed = true;
                        sharedData.SetData(sharedData.ChargeState, ChargeState.MidCharge);
                    }

                    break;
                }
                case <= Configuration.Boss_DashPrepareTime + Configuration.Boss_DashMidChargeTime + Configuration.Boss_DashPostChargeTime:
                {
                    _contactDamageZone.enabled = false;
                    hasDashed = false;
                    _timeCounter = 0f;
                    state = NodeState.SUCCESS;
                    sharedData.SetData(sharedData.ChargeState, ChargeState.None);
                    break;
                }
            }

            return state;
        }
    }
}