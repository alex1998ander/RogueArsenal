using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

namespace BehaviorTree
{
    public class BossAttackStomp : Node
    {
        private Transform _stompTarget;
        private Animator _bossAnimator;
        private Transform _body;
        private GameObject _ui;
        private GameObject _weapon;

        private bool _stompStarted;
        private bool _stomped;
        private float _timeCounter;

        private readonly LayerMask _targetLayer = LayerMask.GetMask("Player_Trigger");

        private static readonly int StompStart = Animator.StringToHash("StompStart");
        private static readonly int StompEnd = Animator.StringToHash("StompEnd");

        public BossAttackStomp(Transform body, Transform stompTarget, Animator bossAnimator, GameObject ui, EnemyWeapon weapon)
        {
            _body = body;
            _stompTarget = stompTarget;
            _bossAnimator = bossAnimator;
            _ui = ui;
            _weapon = weapon.gameObject;
        }

        public override NodeState Evaluate()
        {
            state = NodeState.FAILURE;

            if (_timeCounter == 0f)
            {
                _ui.SetActive(false);
                _weapon.SetActive(false);
                _bossAnimator.SetTrigger(StompStart);
            }

            _timeCounter += Time.fixedDeltaTime;
            switch (_timeCounter)
            {
                case <= Configuration.Boss_StompJumpTime:
                {
                    break;
                }
                case <= Configuration.Boss_StompJumpTime
                        + Configuration.Boss_StompTargetingTime:
                {
                    _body.position = _stompTarget.position;

                    break;
                }
                case <= Configuration.Boss_StompJumpTime
                        + Configuration.Boss_StompTargetingTime
                        + Configuration.Boss_StompFallingTime:
                {
                    if (!_stompStarted)
                    {
                        _bossAnimator.SetTrigger(StompEnd);
                        _stompStarted = true;
                    }

                    break;
                }
                case <= Configuration.Boss_StompJumpTime
                        + Configuration.Boss_StompTargetingTime
                        + Configuration.Boss_StompFallingTime
                        + Configuration.Boss_StompLandingTime:
                {
                    if (!_stomped)
                    {
                        Collider2D playerCollider = Physics2D.OverlapCircle(_body.position, Configuration.Boss_StompRadius, _targetLayer);
                        playerCollider?.GetComponentInParent<ICharacterHealth>()?.InflictDamage(Configuration.Boss_StompDamage);

                        _stomped = true;
                    }

                    break;
                }
                case <= Configuration.Boss_StompJumpTime
                        + Configuration.Boss_StompTargetingTime
                        + Configuration.Boss_StompFallingTime
                        + Configuration.Boss_StompLandingTime
                        + Configuration.Boss_StompCooldownTime:
                {
                    _ui.SetActive(true);
                    _weapon.SetActive(true);

                    _timeCounter = 0f;
                    _stompStarted = false;
                    _stomped = false;

                    state = NodeState.SUCCESS;

                    break;
                }
            }

            return state;
        }
    }
}