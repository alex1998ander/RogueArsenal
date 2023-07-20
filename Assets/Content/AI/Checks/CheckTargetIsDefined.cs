using BehaviorTree;

/// <summary>
/// Checks if a pathfinding target has been defined
/// </summary>
public class CheckTargetIsDefined : Node
{
    public override NodeState Evaluate()
    {
        object t = GetData("target");
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