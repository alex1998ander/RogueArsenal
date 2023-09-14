using BehaviorTree;
using UnityEngine;

public class CheckHeardShots : Node
{
    public override NodeState Evaluate()
    {
        if (GetData<bool>(SharedData.IsStunned))
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