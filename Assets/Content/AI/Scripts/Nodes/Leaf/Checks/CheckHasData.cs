namespace BehaviorTree
{
    /// <summary>
    /// Checks if specific data has been defined inside the behavior tree.
    /// </summary>
    public class CheckHasData<T> : Node
    {
        private SharedDataType<T> _type;

        public CheckHasData(SharedDataType<T> type)
        {
            _type = type;
        }

        public override NodeState Evaluate()
        {
            if (HasData(_type))
                state = NodeState.SUCCESS;
            else
                state = NodeState.FAILURE;

            return state;
        }
    }
}