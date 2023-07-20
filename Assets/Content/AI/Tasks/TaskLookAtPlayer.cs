using UnityEngine;
using BehaviorTree;

/// <summary>
/// Task for the enemy to look at the player.
/// </summary>
public class TaskLookAtPlayer : Node
{
    // Rigidbody of the enemy
    private Rigidbody2D _rb;

    // Transform of the player
    private Transform _playerTransform;

    public TaskLookAtPlayer(Rigidbody2D rb, Transform playerTransform)
    {
        _rb = rb;
        _playerTransform = playerTransform;
    }

    public override NodeState Evaluate()
    {
        Vector2 toPlayerDirection = ((Vector2) _playerTransform.position - _rb.position).normalized;
        if (toPlayerDirection != Vector2.zero)
        {
            // Calculate angle
            float angle = Mathf.Atan2(toPlayerDirection.y, toPlayerDirection.x) * Mathf.Rad2Deg;
            // -90f to account for "forwards" of the enemy being the up vector and not the right vector
            angle -= 90f;
            _rb.rotation = angle;
        }

        state = NodeState.SUCCESS;
        return state;
    }
}