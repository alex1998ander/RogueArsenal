using UnityEngine;
using Random = UnityEngine.Random;

public class CurrencyController : MonoBehaviour
{
    private enum CurrencyState
    {
        Inactive,
        Stable,
        Critical
    }

    private const float DropForce = 15f;
    private const float InitialMoveForce = 0.8f;
    private const float MoveForceGain = 0.04f;
    private const float InactiveLifetimeInSeconds = 0.2f;
    private const float StableLifetimeInSeconds = 1.2f;
    private const float CriticalLifetimeInSeconds = 1f;

    private Rigidbody2D _rb;
    private Transform _playerTransform;

    private float _moveForce = InitialMoveForce;
    private float _lifetimeEndTimestamp;
    private bool _collected;
    private CurrencyState _state = CurrencyState.Inactive;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Vector2 randomDir = Random.insideUnitCircle * DropForce;
        _rb.velocity = randomDir;

        _playerTransform = GameObject.FindWithTag("Player").transform;

        _lifetimeEndTimestamp = Time.time + InactiveLifetimeInSeconds;
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
            switch (_state)
            {
                case CurrencyState.Inactive:
                {
                    _lifetimeEndTimestamp = Time.time + StableLifetimeInSeconds;
                    _state = CurrencyState.Stable;
                    break;
                }
                case CurrencyState.Stable:
                {
                    _lifetimeEndTimestamp = Time.time + CriticalLifetimeInSeconds;
                    _state = CurrencyState.Critical;

                    // TODO: Temporary sprite recoloring, replace with "About to despawn" animation
                    GetComponentInChildren<SpriteRenderer>().color = Color.red;
                    break;
                }
                case CurrencyState.Critical:
                {
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (_state != CurrencyState.Inactive && other.CompareTag("Player"))
        {
            _collected = true;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (_state != CurrencyState.Inactive && other.gameObject.CompareTag("Player"))
        {
            ProgressionManager.CollectCurrency();
            EventManager.OnPlayerCollectCurrency.Trigger();
            Destroy(gameObject);
        }
    }
}