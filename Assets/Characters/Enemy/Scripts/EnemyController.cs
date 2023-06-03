using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Linq;
using Pathfinding;
using UnityEngine.Serialization;
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

    // How many times a second the enemy path is updated.
    public float pathUpdateRate = 0.5f;

    // Distance how close the enemy needs to be to a waypoint until he moves on to the next one.
    public float nextWaypointDistance = 1f;

    // Size of the bounding box around the player where the enemy IS NOT allowed to move.
    public float minDistanceFromPlayer = 4f;

    // Size of the bounding box around the player where the enemy IS allowed to move.
    public float maxDistanceFromPlayer = 6f;

    // Time in seconds the enemy is aiming at the player before attacking
    public float aimTime = 0.5f;

    // Layer mask of the walls of the level.
    public LayerMask wallLayer;

    // Target location for pathfinding.
    private Vector3 _targetLocation;

    // Transform of the Player.
    private Transform _playerTransform;

    // Current path that the enemy is following.
    private Path _path;

    // Current waypoint along the targeted path.
    private int _currentWaypoint = 0;

    // Seeker script which is responsible for creating paths.
    private Seeker _seeker;

    // Rigidbody of enemy.
    private Rigidbody2D _rb;

    // Weapon of the enemy.
    private Weapon _weapon;

    // Current state of the enemy.
    private EnemyState _state = EnemyState.SEARCHING;

    private void Start()
    {
        _playerTransform = GameObject.Find("AimPlayer").GetComponent<Transform>();
        _seeker = GetComponent<Seeker>();
        _rb = GetComponent<Rigidbody2D>();
        _weapon = GetComponentInChildren<Weapon>();

        // update the path every 'pathUpdateRate' seconds (default 0.5)
        InvokeRepeating(nameof(UpdatePath), 0f, pathUpdateRate);
    }

    private void FixedUpdate()
    {
        if (_path == null)
        {
            return;
        }

        switch (_state)
        {
            case EnemyState.SEARCHING:
            {
                PickNewTargetLocationNearPlayer();
                break;
            }
            case EnemyState.MOVING:
            {
                FollowPath();
                break;
            }
            case EnemyState.AIMING:
            {
                AimAtPlayer();
                break;
            }
            case EnemyState.ATTACKING:
            {
                AttackPlayer();
                break;
            }
        }

        Debug.Log("state: " + _state);
    }

    /// <summary>
    /// Picks a new target location in an area around the player.
    /// Makes sure that the enemy has a line of sight to the player.
    /// </summary>
    private void PickNewTargetLocationNearPlayer()
    {
        // Create big and small bounding boxes at player position using playerBounds
        Bounds outerBounds = new Bounds();
        Bounds innerBounds = new Bounds();
        outerBounds.center = _playerTransform.position;
        innerBounds.center = _playerTransform.position;
        outerBounds.size = new Vector3(maxDistanceFromPlayer, maxDistanceFromPlayer, 1);
        innerBounds.size = new Vector3(minDistanceFromPlayer, minDistanceFromPlayer, 1);

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
            return;
        }

        Vector3 nodePosition = (Vector3) randomNode.position;
        Vector3 nodeToPlayer = _playerTransform.position - nodePosition;

        // Check if there is a wall between the picked node and the player
        // Only set target location if there isn't a wall in between (the enemy has a clear line of sight)
        if (!Physics.Raycast(nodePosition, nodeToPlayer, nodeToPlayer.magnitude, wallLayer))
        {
            _targetLocation = nodePosition;
            UpdatePath();
            _state = EnemyState.MOVING;
        }
    }


    /// <summary>
    /// The enemy follows the calculated path.
    /// If path end was reached, initiates attack state.
    /// </summary>
    private void FollowPath()
    {
        // Is the end of the path reached?
        if (_currentWaypoint >= _path.vectorPath.Count)
        {
            _state = EnemyState.AIMING;
            StartCoroutine(AimTime());
            return;
        }

        // Direction of the enemy to the next waypoint
        Vector2 direction = ((Vector2) _path.vectorPath[_currentWaypoint] - _rb.position).normalized;

        // Move the enemy in the direction of the next waypoint
        Vector2 force = direction * (speed * Time.fixedDeltaTime);
        // _rb.AddForce(force, ForceMode2D.Force);
        _rb.velocity = force;

        RotateTowardsDirection(direction);

        // Calculate distance between enemy and next waypoint, if close enough, go to next waypoint
        float distance = Vector2.Distance(_rb.position, _path.vectorPath[_currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            _currentWaypoint++;
        }
    }

    /// <summary>
    /// Switches the state to attacking after the enemy aimed at the player.
    /// </summary>
    private IEnumerator AimTime()
    {
        yield return new WaitForSeconds(aimTime);
        _state = EnemyState.ATTACKING;
    }

    /// <summary>
    /// Enemy aims (looks in the direction of) the player
    /// </summary>
    private void AimAtPlayer()
    {
        Vector2 toPlayerDirection = ((Vector2) _playerTransform.position - _rb.position).normalized;
        RotateTowardsDirection(toPlayerDirection);
    }

    /// <summary>
    /// Enemy attacks the player.
    /// </summary>
    private void AttackPlayer()
    {
        _weapon.Fire();
        _state = EnemyState.SEARCHING;
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
    /// Rotates the enemy so he looks towards the given direction.
    /// </summary>
    /// <param name="direction">Direction the enemy should look at.</param>
    private void RotateTowardsDirection(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            // -90f to account for "forwards" of the enemy being the up vector and not the right vector
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            _rb.rotation = angle;
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