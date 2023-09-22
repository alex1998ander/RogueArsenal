namespace BehaviorTree
{
    public class SetData<T> : Node
    {
        private SharedDataType<T> _type;
        private object _value;

        public SetData(SharedDataType<T> type, object value)
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