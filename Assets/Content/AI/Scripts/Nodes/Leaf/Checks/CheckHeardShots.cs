namespace BehaviorTree
{
    public class CheckHeardShots : Node
    {
        public override NodeState Evaluate()
        {
            if (GetData(sharedData.IsStunned))
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