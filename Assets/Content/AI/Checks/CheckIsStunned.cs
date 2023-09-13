using BehaviorTree;
using UnityEngine;

public class CheckIsStunned : Node
{
    // Amount of time the enemy is stunned
    private readonly float _stunTime;

    // Counter for stun time
    private float _stunCounter;

    public CheckIsStunned(float stunTime)
    {
        _stunTime = stunTime;
    }

    public override NodeState Evaluate()
    {
        
        // Is enemy stunned and stun hasn't run out yet?
        if (GetData<bool>(SharedData.IsStunned) && _stunCounter < _stunTime)
        {
            Debug.Log("Enemy stunned");
            _stunCounter += Time.fixedDeltaTime;
            state = NodeState.SUCCESS;
        }
        else
        {
            SetDataInRoot(SharedData.IsStunned, false);
            _stunCounter = 0f;
            state = NodeState.FAILURE;
        }

        return state;
    }
}