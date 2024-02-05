using UnityEngine;

public class CurrencyController : MonoBehaviour
{
    private enum CurrencyState
    {
        Inactive,
        Stable,
        Critical
    }

    [SerializeField] private CircleCollider2D obstacleCollider;

    private const float InitialMoveForce = 0.8f;
    private const float MoveForceGain = 1.1f;
    private const float MaxMoveForce = 3.0f;
    private const float InactiveLifetimeInSeconds = 0.2f;
    private const float StableLifetimeInSeconds = 1.2f;
    private const float CriticalLifetimeInSeconds = 1f;
    private const float SqrCollectDistance = 0.1f;

    private Rigidbody2D _rb;
    private Transform _playerTransform;

    private float _moveForce = InitialMoveForce;
    private float _lifetimeEndTimestamp;
    private bool _collected;
    private CurrencyState _state = CurrencyState.Inactive;

    void Awake()
    {
        Mathf.Pow(SqrCollectDistance, 2f);
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _playerTransform = GameObject.FindWithTag("Player").transform;

        _lifetimeEndTimestamp = Time.time + InactiveLifetimeInSeconds;
    }

    private void FixedUpdate()
    {
        if (_collected)
        {
            Vector2 playerPosition = _playerTransform.position;
            Vector2 currencyToPlayer = playerPosition - _rb.position;
            _rb.AddForce(currencyToPlayer.normalized * _moveForce, ForceMode2D.Force);
            _moveForce = Mathf.Min(_moveForce * MoveForceGain, MaxMoveForce);

            if (currencyToPlayer.sqrMagnitude <= SqrCollectDistance)
            {
                ProgressionManager.CollectCurrency();
                EventManager.OnPlayerCollectCurrency.Trigger();
                Destroy(gameObject);
            }
        }

        if (Time.time > _lifetimeEndTimestamp)
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
                    if (_collected)
                        obstacleCollider.enabled = false;
                    else
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
}