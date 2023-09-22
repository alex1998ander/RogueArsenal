namespace BehaviorTree
{
    /// <summary>
    /// Task for the enemy to clear its pathfinding target
    /// </summary>
    public class TaskClearTarget : Node
    {
        public override NodeState Evaluate()
        {
            ClearData(sharedData.Target);
            SetData(sharedData.IsAtTarget, false);
            state = NodeState.SUCCESS;
            return state;
        }
    }
}