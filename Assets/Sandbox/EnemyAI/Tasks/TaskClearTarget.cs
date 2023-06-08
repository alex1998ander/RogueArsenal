using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskClearTarget : Node
{
    public override NodeState Evaluate()
    {
        ClearData("target");
        SetDataInRoot("targetReached", false);
        state = NodeState.SUCCESS;
        return state;
    }
}