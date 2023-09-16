using BehaviorTree;

/// <summary>
/// Checks if specific data has been defined inside the behavior tree.
/// </summary>
public class CheckHasData<T> : Node
{
    private SharedDataKey<T> _key;

    public CheckHasData(SharedDataKey<T> key)
    {
        _key = key;
    }

    public override NodeState Evaluate()
    {
        if (HasData(_key))
            state = NodeState.SUCCESS;
        else
            state = NodeState.FAILURE;

        return state;
    }
}