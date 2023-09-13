using BehaviorTree;
using UnityEngine;

public class CheckHeardShots : Node
{
    public override NodeState Evaluate()
    {
        if ((bool) GetData("stunned"))
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
