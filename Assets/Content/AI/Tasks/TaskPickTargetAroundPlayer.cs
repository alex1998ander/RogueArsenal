using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Pathfinding;
using BehaviorTree;
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
        // Create big and small bounding boxes at player position using playerBounds
        Bounds outerBounds = new Bounds();
        Bounds innerBounds = new Bounds();
        outerBounds.center = _playerTransform.position;
        innerBounds.center = _playerTransform.position;
        outerBounds.size =
            new Vector3(_maxDistanceFromPlayer, _maxDistanceFromPlayer, 1);
        innerBounds.size =
            new Vector3(_minDistanceFromPlayer, _minDistanceFromPlayer, 1);

        // Save all nodes near the Player in two separate lists
        // TODO: "pool the list" according to 'GetNodesInRegion' summary
        List<GraphNode> outerNodesNearPlayer = AstarPath.active.data.gridGraph.GetNodesInRegion(outerBounds);
        List<GraphNode> innerNodesNearPlayer = AstarPath.active.data.gridGraph.GetNodesInRegion(innerBounds);

        // Remove the inner nodes from the outer nodes list so the enemy doesn't move closer than allowed to the player
        List<GraphNode> allowedNodes = outerNodesNearPlayer.Except(innerNodesNearPlayer).ToList();

        // Remove all nodes that aren't walkable
        allowedNodes.RemoveAll(node => !node.Walkable);

        // Pick a random node from the allowed nodes
        GraphNode randomNode = allowedNodes[Random.Range(0, allowedNodes.Count)];

        // Save position of the random node in the shared context
        SetDataInRoot("target", (Vector3) randomNode.position);

        // TODO: If no random node found, state = failure, do something else

        state = NodeState.SUCCESS;
        return state;
    }
}