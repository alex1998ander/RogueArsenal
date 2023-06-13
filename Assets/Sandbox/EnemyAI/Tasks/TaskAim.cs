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
            SetDataInRoot("isAiming", false);
            _aimCounter = 0f;
            state = NodeState.SUCCESS;
            Debug.Log("state: " + state);
            return state;
        }

        SetDataInRoot("isAiming", true);
        state = NodeState.FAILURE;
        
        return state;
    }
}