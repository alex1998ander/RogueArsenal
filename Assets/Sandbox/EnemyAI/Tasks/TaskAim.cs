using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskAim : Node
{
    private static float _aimTime = 0.5f;
    private static float _aimCounter = 0f;

    public override NodeState Evaluate()
    {
        _aimCounter += Time.fixedDeltaTime;
        if (_aimCounter >= _aimTime)
        {
            _aimCounter = 0f;
            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.FAILURE;
        return state;
    }
}
