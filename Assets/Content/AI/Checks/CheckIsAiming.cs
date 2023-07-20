using BehaviorTree;

/// <summary>
/// Checks if the enemy is currently aiming at the player
/// </summary>
public class CheckIsAiming : Node
{
    public override NodeState Evaluate()
    {
        if ((bool) GetData("isAiming"))
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