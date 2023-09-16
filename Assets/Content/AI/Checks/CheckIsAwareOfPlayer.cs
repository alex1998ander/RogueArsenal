using BehaviorTree;
using UnityEngine;

/// <summary>
/// Checks if the enemy is aware of the player
/// </summary>
public class CheckIsAwareOfPlayer : Node
{
    public override NodeState Evaluate()
    {
        state = GetData<bool>(sharedData.IsAwareOfPlayer) ? NodeState.SUCCESS : NodeState.FAILURE;

        Debug.Log("state: " + state);

        return state;
    }
}