using BehaviorTree;

/// <summary>
/// Checks if the enemy is currently aiming at the player
/// </summary>
public class CheckIsAiming : Node
{
    public override NodeState Evaluate()
    {
        if (GetData<bool>(SharedData.IsAiming))
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