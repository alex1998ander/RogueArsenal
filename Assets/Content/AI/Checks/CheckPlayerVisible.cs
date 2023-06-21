using UnityEngine;
using BehaviorTree;

/// <summary>
/// Checks if the player is visible from the enemy's current position (No walls between enemy and player)
/// </summary>
public class CheckPlayerVisible : Node
{
    // Rigidbody of the enemy
    private Rigidbody2D _rb;
    
    // Transform of the player
    private Transform _playerTransform;
    
    // Wall layer to check for when raycasting
    private LayerMask _wallLayer;

    public CheckPlayerVisible(Rigidbody2D rb, Transform playerTransform, LayerMask wallLayer)
    {
        _rb = rb;
        _playerTransform = playerTransform;
        _wallLayer = wallLayer;
    }

    public override NodeState Evaluate()
    {
        Vector2 enemyPosition = _rb.position;
        Vector2 enemyToPlayer = (Vector2) _playerTransform.position - enemyPosition;
        if (!Physics2D.Raycast(enemyPosition, enemyToPlayer, enemyToPlayer.magnitude, _wallLayer))
        {
            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.FAILURE;
        return state;
    }
}