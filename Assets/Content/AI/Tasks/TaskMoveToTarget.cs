using UnityEngine;
using BehaviorTree;
using Pathfinding;

/// <summary>
/// Task to move at the pathfinding target
/// </summary>
public class TaskMoveToTarget : Node
{
    // Rigidbody of enemy
    private Rigidbody2D _rb;

    // Walking speed
    private float _walkingSpeed = 200f;

    // The path the enemy takes to reach its pathfinding target
    private Path _path;

    // Calculates the path from the enemy's current position to its target
    private Seeker _seeker;

    // Position of pathfinding target
    private Vector3 _target;

    // Old position of pathfinding target
    private Vector3 _oldTarget;

    // Whether a new target has been determined for pathfinding
    private bool _targetSaved;

    // Current waypoint of the path the enemy is following 
    private int _currentWaypoint = 0;

    private float _nextWaypointDistance = 0.5f;

    // Interval in seconds between path updates 
    private float _pathUpdateTime = 0.5f;

    // Time counter
    private float _pathUpdateCounter = 0f;

    public TaskMoveToTarget(Rigidbody2D rb, float walkingSpeed, Seeker seeker)
    {
        _rb = rb;
        _walkingSpeed = walkingSpeed;
        _seeker = seeker;
    }

    public override NodeState Evaluate()
    {
        Vector3 newTarget = (Vector3) GetData("target");
        if (!_targetSaved || _oldTarget != newTarget)
        {
            _oldTarget = _target;
            _target = newTarget;
            _targetSaved = true;
            // _seeker.StartPath(_rb.position, _target, OnPathComplete);
        }

        // Updates the path from the enemy position to the target
        _pathUpdateCounter += Time.fixedDeltaTime;
        if (_pathUpdateCounter >= _pathUpdateTime && _seeker.IsDone())
        {
            _seeker.StartPath(_rb.position, _target, OnPathComplete);
            _pathUpdateCounter = 0f;
        }

        // Only go further if a path has been calculated
        if (_path != null)
        {
            // Is the end of the path reached?
            if (_currentWaypoint >= _path.vectorPath.Count)
            {
                SetDataInRoot("targetReached", true);
                _targetSaved = false;
                _path = null;
                state = NodeState.SUCCESS;
                return state;
            }

            // Direction of the enemy to the next waypoint
            Vector2 direction = ((Vector2) _path.vectorPath[_currentWaypoint] - _rb.position).normalized;

            // Move the enemy in the direction of the next waypoint
            Vector2 force = direction * (_walkingSpeed * Time.fixedDeltaTime);
            // _rb.AddForce(force, ForceMode2D.Force);
            _rb.velocity = force;

            // Calculate distance between enemy and next waypoint, if close enough, go to next waypoint
            float distance = Vector2.Distance(_rb.position, _path.vectorPath[_currentWaypoint]);
            if (distance < _nextWaypointDistance)
            {
                _currentWaypoint++;
            }
        }

        state = NodeState.RUNNING;
        return state;
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
}