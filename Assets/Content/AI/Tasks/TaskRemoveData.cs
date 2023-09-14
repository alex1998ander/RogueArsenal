using BehaviorTree;

/// <summary>
/// Removes a data field in the shared context.
/// </summary>
public class TaskRemoveData : Node
{
    private string _dataName;

    public TaskRemoveData(string dataName)
    {
        _dataName = dataName;
    }

    public override NodeState Evaluate()
    {
        ClearData(_dataName);
        state = NodeState.SUCCESS;
        return state;
    }
}