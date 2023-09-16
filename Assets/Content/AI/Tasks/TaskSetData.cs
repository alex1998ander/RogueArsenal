using BehaviorTree;
using UnityEngine;

public class TaskSetData<T> : Node
{
    private SharedDataKey<T> _key;
    private object _value;

    public TaskSetData(SharedDataKey<T> key, object value)
    {
        _key = key;
        _value = value;
    }

    public override NodeState Evaluate()
    {
        SetData(_key, _value);
        state = NodeState.SUCCESS;
        return state;
    }
}