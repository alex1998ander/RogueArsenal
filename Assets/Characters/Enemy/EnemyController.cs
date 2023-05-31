using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Pathfinding;

/// <summary>
/// Controls enemy behaviour.
/// </summary>
public class EnemyController : MonoBehaviour
{
    // Target for pathfinding.
    public Transform target;

    // Walking speed.
    public float speed = 200f;

    // Distance how close the enemy needs to be to a waypoint until he moves on to the next one.
    public float nextWaypointDistance = 3f;

    // HP of enemy.
    public int hitPoints;

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
        _seeker = GetComponent<Seeker>();
        _rb = GetComponent<Rigidbody2D>();

        InvokeRepeating(nameof(UpdatePath), 0f, 0.5f);
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
            _reachedEndOfPath = true;
            return;
        }
        else
        {
            _reachedEndOfPath = false;
        }

        // Direction of the player to the next waypoint
        Vector2 direction = ((Vector2)_path.vectorPath[_currentWaypoint] - _rb.position).normalized;

        // Move the enemy in the direction of the next waypoint
        Vector2 force = direction * (speed * Time.fixedDeltaTime);
        _rb.AddForce(force, ForceMode2D.Force);

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
            _seeker.StartPath(_rb.position, target.position, OnPathComplete);
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