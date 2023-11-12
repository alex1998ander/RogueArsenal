using System;
using UnityEngine;

public class Phoenix : MonoBehaviour
{
    private ParticleSystem _ps;

    private ParticleSystem.Particle[] _particles = new ParticleSystem.Particle[3];

    private Vector3[] _particleStartPositions;

    private float _warmUpEndTimestamp;

    private float _preReviveTimeCounter;

    private void Start()
    {
        _ps = GetComponent<ParticleSystem>();
        ParticleSystem.MainModule main = _ps.main;

        _warmUpEndTimestamp = Time.time + Configuration.Phoenix_WarmUpTime;

        float totalParticleSystemLifetime = Configuration.Phoenix_WarmUpTime + Configuration.Phoenix_PreReviveTime;

        main.duration = totalParticleSystemLifetime;
        main.startLifetime = totalParticleSystemLifetime;

        _ps.Emit(3);
        Debug.Log("particle count: " + _ps.GetParticles(_particles));
    }

    private void LateUpdate()
    {
        if (Time.time >= _warmUpEndTimestamp && Time.time <= _warmUpEndTimestamp + Configuration.Phoenix_PreReviveTime)
        {
            _ps.GetParticles(_particles, 3);

            // If start positions uninitialized, copy over current particle positions
            if (_particleStartPositions == null)
            {
                _particleStartPositions = new Vector3[3];
                for (int i = 0; i < _particles.Length; i++)
                {
                    _particleStartPositions[i] = _particles[i].position;
                }
            }
            else
            {
                _preReviveTimeCounter += Time.deltaTime;
                float temp = Mathf.Lerp(0f, Configuration.Phoenix_PreReviveTime, _preReviveTimeCounter);
                Debug.Log("temp: " + temp);

                for (int i = 0; i < _particles.Length; i++)
                {
                    _particles[i].position = Vector3.Lerp(_particleStartPositions[i], Vector3.zero, temp);
                }
            }

            _ps.SetParticles(_particles, 3);
        }
    }
}