using UnityEngine;
using BehaviorTree;

public class CheckPlayerVisible : Node
{
    private Rigidbody2D _rb;
    private Transform _playerTransform;
    private LayerMask _wallLayer;

    public CheckPlayerVisible(Rigidbody2D rb, Transform playerTransform, LayerMask wallLayer) : base()
    {
        _rb = rb;
        _playerTransform = playerTransform;
        _wallLayer = wallLayer;
    }

    public override NodeState Evaluate()
    {
        Vector2 enemyPosition = _rb.position;
        Vector2 enemyToPlayer = (Vector2) _playerTransform.position - enemyPosition;
        // Check if there is a wall between the picked node and the player
        // Only set target location if there isn't a wall in between (the enemy has a clear line of sight)
        if (!Physics2D.Raycast(enemyPosition, enemyToPlayer, enemyToPlayer.magnitude, _wallLayer))
        {
            state = NodeState.SUCCESS;
            return state;
        }

        // Clear the target since the enemy has to find a new position now
        ClearData("target");
        SetDataInRoot("targetReached", false);

        state = NodeState.FAILURE;
        return state;
    }
}