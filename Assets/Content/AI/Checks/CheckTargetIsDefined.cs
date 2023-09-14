using BehaviorTree;
using UnityEngine;

/// <summary>
/// Checks if a pathfinding target has been defined
/// </summary>
public class CheckTargetIsDefined : Node
{
    public override NodeState Evaluate()
    {
        object t = GetData<object>(SharedData.Target);
        if (t != null)
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