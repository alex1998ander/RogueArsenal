using BehaviorTree;

/// <summary>
/// Checks if the enemy is aware of the player
/// </summary>
public class CheckIsAwareOfPlayer : Node
{
    public override NodeState Evaluate()
    {
        if (GetData<bool>(SharedData.IsAwareOfPlayer))
            state = NodeState.SUCCESS;
        else
            state = NodeState.FAILURE;

        return state;
    }
}