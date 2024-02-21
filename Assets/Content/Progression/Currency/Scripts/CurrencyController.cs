using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CurrencyController : MonoBehaviour
{
    private enum CurrencyState
    {
        Inactive,
        Stable,
        PreCritical,
        Critical,
        Unobtainable
    }

    [SerializeField] private Color fadeInToColor = Color.clear;
    [SerializeField] private Color stableColor = Color.yellow;
    [SerializeField] private Color criticalColor = Color.red;
    [SerializeField] private Color fadeOutToColor = Color.clear;

    [SerializeField] private CircleCollider2D obstacleCollider;

    private const float InitialMoveForce = 0.8f;
    private const float MoveForceGain = 1.1f;
    private const float MaxMoveForce = 3.0f;

    private const float InactiveLifetime = 0.2f;
    private const float StableLifetime = 1.2f;
    private const float PreCriticalLifetime = 0.2f;
    private const float CriticalLifetime = 1.0f;
    private const float UnobtainableLifetime = 0.2f;
    private const float SqrCollectDistance = 0.1f;

    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private Light2D _light;

    private Transform _playerTransform;

    private float _moveForce = InitialMoveForce;
    private float _lifetimeEndTimestamp;
    private bool _collected;
    private CurrencyState _state = CurrencyState.Inactive;

    void Awake()
    {
        Mathf.Pow(SqrCollectDistance, 2f);
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponentInChildren<SpriteRenderer>();
        _light = GetComponentInChildren<Light2D>();
    }

    private void Start()
    {
        _playerTransform = GameObject.FindWithTag("Player").transform;

        _lifetimeEndTimestamp = Time.time;
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

        if (Time.time >= _lifetimeEndTimestamp)
        {
            switch (_state)
            {
                case CurrencyState.Inactive:
                {
                    if (_FadeCurrencyColorByTime(fadeInToColor, stableColor, _lifetimeEndTimestamp, _lifetimeEndTimestamp + InactiveLifetime))
                    {
                        Debug.Log("finish inactive");
                        _lifetimeEndTimestamp = Time.time + StableLifetime;
                        _state = CurrencyState.Stable;
                    }

                    // _lifetimeEndTimestamp = Time.time + StableLifetime;
                    // _state = CurrencyState.Stable;
                    break;
                }
                case CurrencyState.Stable:
                {
                    Debug.Log("finish stable");
                    // DON'T set _lifetimeEndTimestamp like before so the next case gets called every fixed update
                    _state = CurrencyState.PreCritical;

                    break;
                }
                case CurrencyState.PreCritical:
                {
                    if (_FadeCurrencyColorByTime(stableColor, criticalColor, _lifetimeEndTimestamp, _lifetimeEndTimestamp + PreCriticalLifetime))
                    {
                        Debug.Log("finish precritical");
                        _lifetimeEndTimestamp = Time.time + CriticalLifetime;
                        _state = CurrencyState.Critical;
                    }


                    // float fadeToCriticalProgress = Mathf.InverseLerp(_lifetimeEndTimestamp, _lifetimeEndTimestamp + PreCriticalLifetime, Time.time);
                    //
                    // Color fadedColor = Color.Lerp(Color.yellow, Color.red, fadeToCriticalProgress);
                    // _sr.color = fadedColor;
                    // _light.color = fadedColor;
                    //
                    // if (fadeToCriticalProgress >= 1f)
                    // {
                    //     _lifetimeEndTimestamp = Time.time + CriticalLifetime;
                    //     _state = CurrencyState.Critical;
                    // }

                    break;
                }
                case CurrencyState.Critical:
                {
                    if (_collected)
                        obstacleCollider.enabled = false;
                    else
                    {
                        Debug.Log("finish critical");
                        // DON'T set _lifetimeEndTimestamp like before so the next case gets called every fixed update
                        _state = CurrencyState.Unobtainable;
                    }

                    break;
                }
                case CurrencyState.Unobtainable:
                {
                    if (_FadeCurrencyColorByTime(criticalColor, fadeOutToColor, _lifetimeEndTimestamp, _lifetimeEndTimestamp + UnobtainableLifetime))
                    {
                        Debug.Log("finish unobtainable");
                        Destroy(gameObject);
                    }

                    // float remainingLifetime = 1f - Mathf.InverseLerp(_lifetimeEndTimestamp, _lifetimeEndTimestamp + UnobtainableLifetime, Time.time);
                    //
                    // _sr.color = new Color(1, 0, 0, remainingLifetime);
                    // _light.color = new Color(1, 0, 0, remainingLifetime);
                    //
                    // if (remainingLifetime <= 0f)
                    //     Destroy(gameObject);

                    break;
                }
            }
        }
    }

    private bool _FadeCurrencyColorByTime(Color fadeFrom, Color fadeInto, float startTime, float endTime)
    {
        float progress = Mathf.InverseLerp(startTime, endTime, Time.time);

        Color fadedColor = Color.Lerp(fadeFrom, fadeInto, progress);
        _sr.color = fadedColor;
        _light.color = fadedColor;

        if (progress >= 1f)
            return true;

        return false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (_state != CurrencyState.Inactive && other.CompareTag("Player"))
        {
            _collected = true;
        }
    }
}