using UnityEngine;
using BehaviorTree;

/// <summary>
/// Lets the enemy wait for a given amount of time in preparation before shooting
/// </summary>
public class TaskAim : Node
{
    // Time to aim
    private static float _aimTime = 0.5f;

    // Time counter
    private static float _aimCounter;

    public override NodeState Evaluate()
    {
        _aimCounter += Time.fixedDeltaTime;
        if (_aimCounter >= _aimTime)
        {
            SetDataInRoot("isAiming", false);
            _aimCounter = 0f;
            state = NodeState.SUCCESS;
            Debug.Log("state: " + state);
            return state;
        }

        SetDataInRoot("isAiming", true);
        state = NodeState.FAILURE;

        return state;
    }
}