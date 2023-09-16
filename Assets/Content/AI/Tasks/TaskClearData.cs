using BehaviorTree;

/// <summary>
/// Removes a data field in the shared context.
/// </summary>
public class TaskClearData<T> : Node
{
    private SharedDataKey<T> _key;

    public TaskClearData(SharedDataKey<T> key)
    {
        _key = key;
    }

    public override NodeState Evaluate()
    {
        ClearData(_key);
        state = NodeState.SUCCESS;
        return state;
    }
}