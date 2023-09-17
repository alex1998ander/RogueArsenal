namespace BehaviorTree
{
    public class TaskSetData<T> : Node
    {
        private SharedDataType<T> _type;
        private object _value;

        public TaskSetData(SharedDataType<T> type, object value)
        {
            _type = type;
            _value = value;
        }

        public override NodeState Evaluate()
        {
            SetData(_type, _value);
            state = NodeState.SUCCESS;
            return state;
        }
    }
}