using System;
using UnityEngine;

namespace BehaviorTree
{
    public class SetAnimatorParameter<T> : Node
    {
        private readonly Animator _animator;
        private readonly string _parameterName;
        private readonly T _parameterValue;

        public SetAnimatorParameter(Animator animator, string parameterName, T parameterValue)
        {
            _animator = animator;
            _parameterName = parameterName;
            _parameterValue = parameterValue;
        }

        public override NodeState Evaluate()
        {
            state = NodeState.SUCCESS;

            if (_parameterValue is float)
                _animator.SetFloat(_parameterName, (float) (object) _parameterValue);
            else if (_parameterValue is int)
                _animator.SetInteger(_parameterName, (int) (object) _parameterValue);
            else if (_parameterValue is bool)
                _animator.SetBool(_parameterName, (bool) (object) _parameterValue);
            else
                state = NodeState.FAILURE;

            return state;
        }
    }
}