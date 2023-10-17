using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CurrencyController : MonoBehaviour
{
    private const float DropForce = 15f;
    private const float InitialMoveForce = 80f;
    private const float MoveForceGain = 4f;
    private float _moveForce = InitialMoveForce;
    private Rigidbody2D _rb;
    private Transform _playerTransform;
    private bool _playerFound;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Vector2 randomDir = Random.insideUnitCircle * DropForce;
        _rb.velocity = randomDir;

        _playerTransform = GameObject.FindWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        if (_playerFound)
        {
            Vector2 playerPosition = _playerTransform.position;
            Vector2 toPlayerDirection = (playerPosition - _rb.position).normalized;
            _rb.AddForce(toPlayerDirection * _moveForce, ForceMode2D.Force);
            _moveForce += MoveForceGain;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Trigger Enter");
            _playerFound = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collider Enter");
            EventManager.OnPlayerCollectCurrency.Trigger();
            Destroy(gameObject);
        }
    }
}