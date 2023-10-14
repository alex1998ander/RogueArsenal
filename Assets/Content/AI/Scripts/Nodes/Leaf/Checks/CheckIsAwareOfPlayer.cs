namespace BehaviorTree
{
    /// <summary>
    /// Checks if the enemy is aware of the player
    /// </summary>
    public class CheckIsAwareOfPlayer : Node
    {
        public override NodeState Evaluate()
        {
            state = GetData(sharedData.IsAwareOfPlayer) ? NodeState.SUCCESS : NodeState.FAILURE;
            return state;
        }
    }
}