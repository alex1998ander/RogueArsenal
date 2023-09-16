using UnityEngine;
using BehaviorTree;
using UnityEngine.AI;

/// <summary>
/// Task to move at the pathfinding target
/// </summary>
public class TaskMoveToTarget : Node
{
    // Rigidbody of enemy
    private Rigidbody2D _rb;

    // Position of pathfinding target
    private Vector3 _target;

    // Old position of pathfinding target
    private Vector3 _oldTarget;

    // Nav Mesh Agent
    private NavMeshAgent _agent;

    // Distance to the pathfinding target to count as having reached it
    private float _targetReachedDistance;

    public TaskMoveToTarget(Rigidbody2D rb, NavMeshAgent agent, float targetReachedDistance)
    {
        _rb = rb;
        _agent = agent;
        _targetReachedDistance = targetReachedDistance;
    }

    public override NodeState Evaluate()
    {
        Vector3 newTarget = GetData<Vector3>(sharedData.Target);
        _agent.SetDestination(newTarget);

        state = NodeState.RUNNING;
        if ((_rb.position - (Vector2) newTarget).magnitude < _targetReachedDistance)
        {
            state = NodeState.SUCCESS;
            SetData(sharedData.TargetReached, true);
        }

        return state;
    }
}