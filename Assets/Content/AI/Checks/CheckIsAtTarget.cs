using BehaviorTree;

/// <summary>
/// Check if the enemy has reached its pathfinding-target
/// </summary>
public class CheckIsAtTarget : Node
{
    public override NodeState Evaluate()
    {
        if (GetData<bool>(SharedData.TargetReached))
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