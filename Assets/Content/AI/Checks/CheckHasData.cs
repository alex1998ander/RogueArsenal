using BehaviorTree;

/// <summary>
/// Checks if specific data has been defined inside the behavior tree.
/// </summary>
public class CheckHasData : Node
{
    private string _dataName;

    public CheckHasData(string dataName)
    {
        _dataName = dataName;
    }

    public override NodeState Evaluate()
    {
        if (HasData(_dataName))
            state = NodeState.SUCCESS;
        else
            state = NodeState.FAILURE;

        return state;
    }
}