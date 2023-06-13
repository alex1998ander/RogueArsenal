using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskLookAtMovementDirection : Node
{
    private Rigidbody2D _rb;

    public TaskLookAtMovementDirection(Rigidbody2D rb) : base()
    {
        _rb = rb;
    }

    public override NodeState Evaluate()
    {
        Vector2 lookDirection = _rb.velocity.normalized;
        if (lookDirection != Vector2.zero)
        {
            // Calculate angle
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
            // -90f to account for "forwards" of the enemy being the up vector and not the right vector
            angle -= 90f;
            _rb.rotation = angle;
        }

        state = NodeState.SUCCESS;
        return state;
    }
}