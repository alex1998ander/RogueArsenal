namespace BehaviorTree
{
    /// <summary>
    /// Removes a data field in the shared context.
    /// </summary>
    public class ClearData<T> : Node
    {
        private SharedDataType<T> _type;

        public ClearData(SharedDataType<T> type)
        {
            _type = type;
        }

        public override NodeState Evaluate()
        {
            state = ClearData(_type) ? NodeState.SUCCESS : NodeState.FAILURE;
            return state;
        }
    }
}