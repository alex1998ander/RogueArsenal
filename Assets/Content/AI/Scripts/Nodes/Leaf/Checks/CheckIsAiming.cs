namespace BehaviorTree
{
    /// <summary>
    /// Checks if the enemy is currently aiming at the player
    /// </summary>
    public class CheckIsAiming : Node
    {
        public override NodeState Evaluate()
        {
            if (GetData<bool>(sharedData.IsAiming))
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
}