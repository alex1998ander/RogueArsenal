namespace BehaviorTree
{
    /// <summary>
    /// Checks if specific data has been defined and contains expected data inside the behavior tree.
    /// </summary>
    public class ExpectData<T> : Node
    {
        private SharedDataType<T> _type;

        private T _expectedValue;

        public ExpectData(SharedDataType<T> type, T expectedValue)
        {
            _type = type;
            _expectedValue = expectedValue;
        }

        public override NodeState Evaluate()
        {
            state = NodeState.FAILURE;

            if (!HasData(_type))
                return state;

            T actualValue = GetData(_type);
            if (actualValue.Equals(_expectedValue))
                state = NodeState.SUCCESS;

            return state;
        }
    }
}