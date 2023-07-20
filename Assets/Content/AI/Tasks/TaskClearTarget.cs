using BehaviorTree;

/// <summary>
/// Task for the enemy to clear its pathfinding target
/// </summary>
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