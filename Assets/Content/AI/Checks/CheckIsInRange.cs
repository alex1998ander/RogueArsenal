using UnityEngine;
using BehaviorTree;

public class CheckIsInRange : Node
{
    // Rigidbody of the enemy
    private Rigidbody2D _rb;
    
    // Transform of the player
    private Transform _playerTransform;

    // Allowed distance to the player before reacting
    private float _distance;
    
    public CheckIsInRange(Rigidbody2D rb, Transform playerTransform, float distance)
    {
        _rb = rb;
        _playerTransform = playerTransform;
        _distance = distance;
    }

    public override NodeState Evaluate()
    {
        Vector2 enemyPosition = _rb.position;
        if(_distance >= Vector2.Distance(enemyPosition, _playerTransform.position))
        {
            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.FAILURE;
        return state;
    }
}
