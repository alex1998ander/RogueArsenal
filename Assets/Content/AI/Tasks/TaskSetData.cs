using BehaviorTree;
using UnityEngine;

public class TaskSetData : Node
{
    private string _dataName;
    private object _data;
    
    public TaskSetData(string dataName, object data)
    {
        _dataName = dataName;
        _data = data;
    }
    public override NodeState Evaluate()
    {
        SetData(_dataName, _data);
        state = NodeState.SUCCESS;
        return state;
    }
}
