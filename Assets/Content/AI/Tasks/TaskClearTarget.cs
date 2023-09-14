using BehaviorTree;

/// <summary>
/// Task for the enemy to clear its pathfinding target
/// </summary>
public class TaskClearTarget : Node
{
    public override NodeState Evaluate()
    {
        ClearData(SharedData.Target);
        SetDataInRoot(SharedData.TargetReached, false);
        state = NodeState.SUCCESS;
        return state;
    }
}