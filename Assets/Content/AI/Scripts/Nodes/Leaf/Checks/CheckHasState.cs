namespace BehaviorTree
{
    public class CheckHasState : Node
    {
        // State to be checked
        private readonly SharedDataType<bool> _state;

        public CheckHasState(SharedDataType<bool> state)
        {
            _state = state;
        }

        public override NodeState Evaluate()
        {
            state = NodeState.FAILURE;
            if (GetData(_state))
                state = NodeState.SUCCESS;

            return state;
        }
    }
}