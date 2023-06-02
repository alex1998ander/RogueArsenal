using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Pathfinding;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

/// <summary>
/// Controls enemy behaviour.
/// </summary>
public class EnemyController : MonoBehaviour
{
    // Walking speed.
    public float speed = 200f;

    // HP of enemy.
    public int hitPoints;

    // How many times a second the enemy path is updated
    public float pathUpdateRate = 0.5f;

    // Distance how close the enemy needs to be to a waypoint until he moves on to the next one.
    public float nextWaypointDistance = 1f;

    public float playerBounds = 5f;

    // Target location for pathfinding.
    private Vector3 _targetLocation;

    // Transform of the Player
    private Transform _playerTransform;

    // Vector representing the direction from the enemy to the player
    private Vector2 _toPlayerDirection;

    private GridNode[] _walkableNodes;

    // Current path that the enemy is following.
    private Path _path;

    // Current waypoint along the targeted path.
    private int _currentWaypoint = 0;

    // Whether the enemy reached the end of the path or not.
    private bool _reachedEndOfPath = false;

    // Seeker script which is responsible for creating paths.
    private Seeker _seeker;

    // Rigidbody of enemy.
    private Rigidbody2D _rb;

    private void Start()
    {
        _playerTransform = GameObject.Find("AimPlayer").GetComponent<Transform>();
        _seeker = GetComponent<Seeker>();
        _rb = GetComponent<Rigidbody2D>();

        // Get the gridGraph of the level
        GridGraph gridGraph = AstarPath.active.data.gridGraph;
        List<GridNode> nodes = gridGraph.nodes.ToList();

        List<GridNode> walkableNodes = new List<GridNode>();
        foreach (GridNode node in nodes)
        {
            if (node.Walkable)
            {
                walkableNodes.Add(node);
            }
        }

        _walkableNodes = walkableNodes.ToArray();

        // update the path every 'pathUpdateRate' seconds (default 0.5)
        InvokeRepeating(nameof(UpdatePath), 0f, pathUpdateRate);
    }

    private void FixedUpdate()
    {
        if (_path == null)
        {
            return;
        }

        // Is the end of the path reached?
        if (_currentWaypoint >= _path.vectorPath.Count)
        {
            PickNewTargetLocationNearPlayer();
            return;
        }
        else
        {
            _reachedEndOfPath = false;
        }

        // Direction of the player to the next waypoint
        Vector2 direction = ((Vector2) _path.vectorPath[_currentWaypoint] - _rb.position).normalized;

        // Move the enemy in the direction of the next waypoint
        Vector2 force = direction * (speed * Time.fixedDeltaTime);
        // _rb.AddForce(force, ForceMode2D.Force);
        _rb.velocity = force;

        // Calculate distance between enemy and next waypoint, if close enough, go to next waypoint
        float distance = Vector2.Distance(_rb.position, _path.vectorPath[_currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            _currentWaypoint++;
        }
    }

    /// <summary>
    /// Updates the path from the enemy position to the target
    /// </summary>
    private void UpdatePath()
    {
        if (_seeker.IsDone())
        {
            _seeker.StartPath(_rb.position, _targetLocation, OnPathComplete);
        }
    }

    /// <summary>
    /// Is called when the path from start to finish was successfully calculated.
    /// </summary>
    /// <param name="p">Calculated path</param>
    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            _path = p;
            _currentWaypoint = 0;
        }
    }

    /// <summary>
    /// Picks a new target location in an area around the player
    /// </summary>
    private void PickNewTargetLocationNearPlayer()
    {
        _reachedEndOfPath = true;
        // GridNode randomWalkableNode = _walkableNodes[Random.Range(0, _walkableNodes.Length)];
        // _targetLocation = (Vector3) randomWalkableNode.position;

        // Create bounding box at player position using playerBounds
        Bounds bounds = new Bounds();
        bounds.center = _playerTransform.position;
        bounds.size = new Vector3(playerBounds, playerBounds, 1);

        // Save all nodes near the Player
        List<GraphNode> nodesNearPlayer = AstarPath.active.data.gridGraph.GetNodesInRegion(bounds);
        
        // Pick a random node
        // If this node is also walkable, choose its location as the next target
        GraphNode randomNode = nodesNearPlayer[Random.Range(0, nodesNearPlayer.Count)];
        if (randomNode.Walkable)
        {
            _targetLocation = (Vector3) nodesNearPlayer[Random.Range(0, nodesNearPlayer.Count)].position;
        }
    }

    /// <summary>
    /// When enemy collides with other object, check if it's a player bullet.
    /// If so, reduce HP.
    /// </summary>
    /// <param name="other">collision info</param>
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            hitPoints--;
            if (hitPoints <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}