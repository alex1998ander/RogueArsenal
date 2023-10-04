namespace BehaviorTree
{
    /// <summary>
    /// Checks if a pathfinding target has been defined
    /// </summary>
    public class CheckTargetIsDefined : Node
    {
        public override NodeState Evaluate()
        {
            object t = GetData(sharedData.Target);
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
}