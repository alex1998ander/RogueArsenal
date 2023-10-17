using UnityEngine;
using Random = UnityEngine.Random;

public class CurrencyController : MonoBehaviour
{
    private const float DropForce = 15f;
    private const float InitialMoveForce = 80f;
    private const float MoveForceGain = 4f;
    private const float StableLifetimeInSeconds = 1.5f;
    private const float CriticalLifetimeInSeconds = 1f;

    private Rigidbody2D _rb;
    private Transform _playerTransform;

    private float _moveForce = InitialMoveForce;
    private float _lifetimeEndTimestamp;
    private bool _collected;
    private bool _critical;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Vector2 randomDir = Random.insideUnitCircle * DropForce;
        _rb.velocity = randomDir;

        _playerTransform = GameObject.FindWithTag("Player").transform;

        _lifetimeEndTimestamp = Time.time + StableLifetimeInSeconds;
    }

    private void FixedUpdate()
    {
        if (_collected)
        {
            Vector2 playerPosition = _playerTransform.position;
            Vector2 toPlayerDirection = (playerPosition - _rb.position).normalized;
            _rb.AddForce(toPlayerDirection * _moveForce, ForceMode2D.Force);
            _moveForce += MoveForceGain;
        }
        else if (Time.time > _lifetimeEndTimestamp)
        {
            if (_critical)
            {
                Destroy(gameObject);
            }
            else
            {
                _critical = true;
                _lifetimeEndTimestamp = Time.time + CriticalLifetimeInSeconds;

                // TODO: Temporary sprite recoloring, replace with "About to despawn" animation
                GetComponentInChildren<SpriteRenderer>().color = Color.red;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _collected = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ProgressionManager.CollectCurrency();
            EventManager.OnPlayerCollectCurrency.Trigger();
            Destroy(gameObject);
        }
    }
}