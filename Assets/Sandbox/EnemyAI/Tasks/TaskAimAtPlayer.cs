using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

/// <summary>
/// Tasks which lets the enemy aim at the player.
/// </summary>
public class TaskAimAtPlayer : Node
{
    // Rigidbody of the enemy
    private Rigidbody2D _rb;

    // Transform of the player
    private Transform _playerTransform;

    private float _aimTime = 0.5f;
    private float _aimCounter = 0f;

    public TaskAimAtPlayer(Rigidbody2D rb, Transform playerTransform) : base()
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

        _aimCounter += Time.fixedDeltaTime;
        if (_aimCounter >= _aimTime)
        {
            _aimCounter = 0f;
            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.FAILURE;
        return state;
    }
}