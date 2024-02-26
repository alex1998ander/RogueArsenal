using System.Collections;
using BehaviorTree;
using UnityEngine;

namespace BehaviorTree
{
    public class BossAttackLaserFocus : Node
    {
        private readonly BoxCollider2D _contactDamageZone;
        private readonly Animator _animator;

        private float _timeCounter;

        private int _waveCounter;

        private static readonly int Aim = Animator.StringToHash("Aim");
        private static readonly int Start = Animator.StringToHash("Start");
        private static readonly int Active = Animator.StringToHash("Active");
        private static readonly int Inactive = Animator.StringToHash("Inactive");
        private static readonly int End = Animator.StringToHash("End");

        public BossAttackLaserFocus(BoxCollider2D contactDamageZone, Animator animator)
        {
            _contactDamageZone = contactDamageZone;
            _animator = animator;
        }

        public override NodeState Evaluate()
        {
            state = NodeState.FAILURE;

            _timeCounter += Time.fixedDeltaTime;
            switch (_timeCounter)
            {
                case <= Configuration.Boss_LaserAimTime:
                {
                    _animator.SetTrigger(Aim);

                    _animator.ResetTrigger(Start);
                    _animator.ResetTrigger(Active);
                    _animator.ResetTrigger(Inactive);
                    _animator.ResetTrigger(End);

                    break;
                }
                case <= Configuration.Boss_LaserAimTime
                        + Configuration.Boss_LaserPreStartTime:
                {
                    sharedData.SetData(sharedData.BossLaserFiring, true);

                    break;
                }
                case <= Configuration.Boss_LaserAimTime
                        + Configuration.Boss_LaserPreStartTime
                        + Configuration.Boss_LaserStartTime:
                {
                    _animator.SetTrigger(Start);

                    _animator.ResetTrigger(Aim);
                    _animator.ResetTrigger(Active);
                    _animator.ResetTrigger(Inactive);
                    _animator.ResetTrigger(End);

                    break;
                }
                case <= Configuration.Boss_LaserAimTime
                        + Configuration.Boss_LaserPreStartTime
                        + Configuration.Boss_LaserStartTime
                        + Configuration.Boss_LaserActiveTime:
                {
                    _contactDamageZone.enabled = true;
                    _animator.SetTrigger(Active);

                    _animator.ResetTrigger(Aim);
                    _animator.ResetTrigger(Start);
                    _animator.ResetTrigger(Inactive);
                    _animator.ResetTrigger(End);

                    break;
                }
                case <= Configuration.Boss_LaserAimTime
                        + Configuration.Boss_LaserPreStartTime
                        + Configuration.Boss_LaserStartTime
                        + Configuration.Boss_LaserActiveTime
                        + Configuration.Boss_LaserEndTime:
                {
                    _contactDamageZone.enabled = false;
                    _animator.SetTrigger(End);

                    _animator.ResetTrigger(Aim);
                    _animator.ResetTrigger(Start);
                    _animator.ResetTrigger(Active);
                    _animator.ResetTrigger(Inactive);

                    break;
                }
                case <= Configuration.Boss_LaserAimTime
                        + Configuration.Boss_LaserPreStartTime
                        + Configuration.Boss_LaserStartTime
                        + Configuration.Boss_LaserActiveTime
                        + Configuration.Boss_LaserEndTime
                        + Configuration.Boss_LaserTimeBetweenRepetitions:
                {
                    sharedData.SetData(sharedData.BossLaserFiring, false);

                    _timeCounter = 0f;

                    _waveCounter++;
                    if (_waveCounter == Configuration.Boss_LaserRepetitions)
                    {
                        _animator.SetTrigger(Inactive);

                        _animator.ResetTrigger(Aim);
                        _animator.ResetTrigger(Start);
                        _animator.ResetTrigger(Active);
                        _animator.ResetTrigger(End);

                        _waveCounter = 0;
                        state = NodeState.SUCCESS;
                    }

                    break;
                }
            }

            return state;
        }
    }
}