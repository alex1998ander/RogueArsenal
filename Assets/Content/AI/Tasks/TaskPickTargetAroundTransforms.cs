using UnityEngine;
using BehaviorTree;
using Random = UnityEngine.Random;

/// <summary>
/// Task which lets the enemy pick a position around the player as a pathfinding target
/// </summary>
public class TaskPickTargetAroundTransforms : Node
{
    // Transform of the player
    private Transform[] _targetTransforms;

    private float _minDistanceFromTarget = 4f;

    private float _maxDistanceFromTarget = 6f;

    public TaskPickTargetAroundTransforms(Transform targetTransform, float minDistanceFromTarget,
        float maxDistanceFromTarget)
    {
        _targetTransforms = new Transform[] {targetTransform};
        _minDistanceFromTarget = minDistanceFromTarget;
        _maxDistanceFromTarget = maxDistanceFromTarget;
    }

    public TaskPickTargetAroundTransforms(Transform[] targetTransforms, float minDistanceFromTarget,
        float maxDistanceFromTarget)
    {
        _targetTransforms = targetTransforms;
        _minDistanceFromTarget = minDistanceFromTarget;
        _maxDistanceFromTarget = maxDistanceFromTarget;
    }

    public override NodeState Evaluate()
    {
        int random = Random.Range(0, _targetTransforms.Length);
        Vector2 randomDirection = Random.insideUnitCircle;
        Vector3 randomPositionAroundPlayer = _targetTransforms[random].position
                                             + (Vector3) (_minDistanceFromTarget * randomDirection
                                                          + Random.Range(0f, 1f) * _maxDistanceFromTarget *
                                                          randomDirection);

        SetData(sharedData.TargetReached, false);
        SetData(sharedData.Target, randomPositionAroundPlayer);

        state = NodeState.SUCCESS;
        return NodeState.SUCCESS;
    }
}