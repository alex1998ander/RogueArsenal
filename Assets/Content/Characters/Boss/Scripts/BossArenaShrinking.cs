using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArenaShrinking : MonoBehaviour
{
    [SerializeField] private Transform[] walls;
    private EnemyHealth _bossHealth;

    private Vector3 [] _startMarker = new Vector3[3];
    private Vector3 [] _endMarker = new Vector3[3];

    // Movement speed in units per second.
    public float speed = 0.1F;

    // Time when the movement started.
    private float _startTime;

    // Total distance between the markers.
    private float  _journeyLength;

    private Vector3 _direction;
    
    private void Start()
    {
        StartCoroutine(DelayedSearchForBossHealthScript());
        for (int i = 0; i < 3; i++)
        {
            _startMarker[i] = walls[i].transform.position;
            
            _direction = Vector3.Normalize(transform.position - walls[i].position);
            _endMarker[i] = _startMarker[i] + _direction * 5;
        }
        
        // Calculate the journey length.
        _journeyLength = Vector3.Distance(_startMarker[0], _endMarker[0]);
    }

    private void Update()
    {
        if (_bossHealth != null && (_bossHealth.GetHealth().x / _bossHealth.GetHealth().y) <= 1f / 3f)
        {
            _startTime = _startTime== 0? Time.time : _startTime;
            // Distance moved equals elapsed time times speed..
            float distCovered = (Time.time - _startTime) * speed;

            // Fraction of journey completed equals current distance divided by total distance.
            float fractionOfJourney = distCovered / _journeyLength;
            for (int i = 0; i < 3; i++)
            {
                walls[i].transform.position = Vector3.Lerp(_startMarker[i], _endMarker[i], fractionOfJourney);
            }
             
        }
    }

    IEnumerator  DelayedSearchForBossHealthScript()
    {
        yield return new WaitForSeconds(5f);
        _bossHealth = FindObjectOfType<EnemyHealth>();
    }
}