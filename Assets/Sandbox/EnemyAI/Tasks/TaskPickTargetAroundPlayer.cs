using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Pathfinding;
using BehaviorTree;
using Random = UnityEngine.Random;

public class TaskPickTargetAroundPlayer : Node
{
    private Transform _playerTransform;

    public TaskPickTargetAroundPlayer(Transform playerTransform) : base()
    {
        _playerTransform = playerTransform;
    }

    public override NodeState Evaluate()
    {
        // Create big and small bounding boxes at player position using playerBounds
        Bounds outerBounds = new Bounds();
        Bounds innerBounds = new Bounds();
        outerBounds.center = _playerTransform.position;
        innerBounds.center = _playerTransform.position;
        outerBounds.size =
            new Vector3(FollowingEnemyBT.MaxDistanceFromPlayer, FollowingEnemyBT.MaxDistanceFromPlayer, 1);
        innerBounds.size =
            new Vector3(FollowingEnemyBT.MinDistanceFromPlayer, FollowingEnemyBT.MinDistanceFromPlayer, 1);

        // Save all nodes near the Player in two separate lists
        // TODO: "pool the list" according to 'GetNodesInRegion' summary
        List<GraphNode> outerNodesNearPlayer = AstarPath.active.data.gridGraph.GetNodesInRegion(outerBounds);
        List<GraphNode> innerNodesNearPlayer = AstarPath.active.data.gridGraph.GetNodesInRegion(innerBounds);

        // Remove the inner nodes from the outer nodes list so the enemy doesn't move closer than allowed to the player
        List<GraphNode> allowedNodes = outerNodesNearPlayer.Except(innerNodesNearPlayer).ToList();

        // Pick a random node from the allowed nodes
        GraphNode randomNode = allowedNodes[Random.Range(0, allowedNodes.Count)];

        // Exit if node isn't walkable
        if (!randomNode.Walkable)
        {
            return NodeState.FAILURE;
        }

        return NodeState.SUCCESS;
    }
}