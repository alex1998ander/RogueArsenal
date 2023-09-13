using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Pathfinding;
using BehaviorTree;
using UnityEngine.AI;
using Random = UnityEngine.Random;

/// <summary>
/// Task which lets the enemy pick a position around the player as a pathfinding target
/// </summary>
public class TaskPickTargetAroundPlayer : Node
{
    // Transform of the player
    private Transform _playerTransform;

    private float _minDistanceFromPlayer = 4f;

    private float _maxDistanceFromPlayer = 6f;

    public TaskPickTargetAroundPlayer(Transform playerTransform, float minDistanceFromPlayer,
        float maxDistanceFromPlayer)
    {
        _playerTransform = playerTransform;
        _minDistanceFromPlayer = minDistanceFromPlayer;
        _maxDistanceFromPlayer = maxDistanceFromPlayer;
    }

    public override NodeState Evaluate()
    {
        Vector2 randomDirection = Random.insideUnitCircle;
        Vector3 randomPositionAroundPlayer = _playerTransform.position
                                             + (Vector3) (_minDistanceFromPlayer * randomDirection
                                                          + Random.Range(0f, 1f) * _maxDistanceFromPlayer *
                                                          randomDirection);
        SetDataInRoot("target", randomPositionAroundPlayer);
        return NodeState.SUCCESS;
    }
}