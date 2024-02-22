using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class CurrencyController : MonoBehaviour
{
    private enum CurrencyState
    {
        Inactive, // Can't be collected
        Stable, // Can now be collected
        PreCritical, // Transition state between Stable and Critical
        Critical, // Still can be collected, but not for long
        Unobtainable // Can't be collected anymore
    }

    // Color the currency starts with, fades into stableColor during inactive lifetime
    [SerializeField] private Color fadeInFromColor = Color.clear;

    // Color during stable lifetime
    [SerializeField] private Color stableColor = Color.yellow;

    // Color during critical lifetime
    [SerializeField] private Color criticalColor = Color.red;

    // Color the currency fades into after critical lifetime during unobtainable lifetime
    [SerializeField] private Color fadeOutToColor = Color.clear;

    [SerializeField] private CircleCollider2D obstacleCollider;

    private const float InitialMoveForce = 0.8f;
    private const float MoveForceGain = 1.1f;
    private const float MaxMoveForce = 3.0f;

    private const float InactiveLifetime = 0.2f;
    private const float MinStableLifetime = 1.6f;
    private const float MaxStableLifetime = 2.0f;
    private const float PreCriticalLifetime = 0.2f;
    private const float CriticalLifetime = 1.0f;
    private const float UnobtainableLifetime = 0.1f;

    private const float SqrCollectDistance = 0.5f * 0.5f; // TODO: Replace with static power of 2 function call, can't find it right now

    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private Light2D _light;

    private Transform _playerTransform;

    private float _moveForce = InitialMoveForce;
    private float _lifetimeEndTimestamp;
    private bool _collected;
    private CurrencyState _state = CurrencyState.Inactive;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponentInChildren<SpriteRenderer>();
        _light = GetComponentInChildren<Light2D>();

        _sr.color = fadeInFromColor;
        _light.color = fadeInFromColor;

        _playerTransform = GameObject.FindWithTag("Player").transform;

        // set timestamp to current time for 'inactive' lifetime to correctly lerp colors
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
                    if (_FadeCurrencyColorByTime(fadeInFromColor,
                            stableColor,
                            _lifetimeEndTimestamp,
                            _lifetimeEndTimestamp + InactiveLifetime))
                    {
                        _lifetimeEndTimestamp = Time.time + Random.Range(MinStableLifetime, MaxStableLifetime);
                        _state = CurrencyState.Stable;
                    }

                    break;
                }
                case CurrencyState.Stable:
                {
                    // DON'T set _lifetimeEndTimestamp like before so the next case gets called every fixed update
                    _state = CurrencyState.PreCritical;

                    break;
                }
                case CurrencyState.PreCritical:
                {
                    if (_FadeCurrencyColorByTime(
                            stableColor,
                            criticalColor,
                            _lifetimeEndTimestamp,
                            _lifetimeEndTimestamp + PreCriticalLifetime))
                    {
                        _lifetimeEndTimestamp = Time.time + CriticalLifetime;
                        _state = CurrencyState.Critical;
                    }

                    break;
                }
                case CurrencyState.Critical:
                {
                    if (_collected)
                        obstacleCollider.enabled = false;
                    else
                    {
                        // DON'T set _lifetimeEndTimestamp like before so the next case gets called every fixed update
                        _state = CurrencyState.Unobtainable;
                    }

                    break;
                }
                case CurrencyState.Unobtainable:
                {
                    if (_FadeCurrencyColorByTime(criticalColor,
                            fadeOutToColor,
                            _lifetimeEndTimestamp,
                            _lifetimeEndTimestamp + UnobtainableLifetime))
                    {
                        Destroy(gameObject);
                    }

                    break;
                }
            }
        }
    }

    private bool _FadeCurrencyColorByTime(Color fadeFrom, Color fadeInto, float startTime, float endTime)
    {
        float fadeProgress = Mathf.InverseLerp(startTime, endTime, Time.time);

        Color fadedColor = Color.Lerp(fadeFrom, fadeInto, fadeProgress);
        _sr.color = fadedColor;
        _light.color = fadedColor;

        if (fadeProgress >= 1f)
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