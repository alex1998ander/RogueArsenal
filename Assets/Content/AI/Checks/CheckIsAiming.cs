using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class CheckIsAiming : Node
{
    public CheckIsAiming() : base()
    {
    }

    public override NodeState Evaluate()
    {
        if ((bool) GetData("isAiming"))
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