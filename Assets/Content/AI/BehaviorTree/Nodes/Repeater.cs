using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class Repeater : Node
{
    private float _repeatTime = 0.5f;
    private float _repeatCounter = 0f;

    public Repeater() : base()
    {
    }

    public override NodeState Evaluate()
    {
        _repeatCounter += Time.fixedDeltaTime;
        if (_repeatCounter >= _repeatTime)
        {
            _repeatCounter = 0f;
            state = NodeState.SUCCESS;
        }
        else
        {
            state = NodeState.FAILURE;
        }

        return state;
    }
}