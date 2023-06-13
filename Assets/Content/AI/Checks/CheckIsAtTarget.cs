using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class CheckIsAtTarget : Node
{
    private Rigidbody2D _rb;

    public CheckIsAtTarget(Rigidbody2D rb) : base()
    {
        _rb = rb;
    }

    public override NodeState Evaluate()
    {
        if ((bool) GetData("targetReached"))
        {
            state = NodeState.SUCCESS;
        }
        else
        {
            state = NodeState.FAILURE;
        }

        return state;
    }
}