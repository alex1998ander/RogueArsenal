using BehaviorTree;
using UnityEngine;

/// <summary>
/// Saves the current player location to shared data
/// </summary>
public class TaskSavePlayerLocation : Node
{
    private Transform _playerTransform;

    public TaskSavePlayerLocation(Transform playerTransform)
    {
        _playerTransform = playerTransform;
    }

    public override NodeState Evaluate()
    {
        SetData(sharedData.PlayerLocation, _playerTransform.position);
        return NodeState.SUCCESS;
    }
}