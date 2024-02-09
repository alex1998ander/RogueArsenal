using UnityEngine;
using Random = UnityEngine.Random;

public class DebrisController : MonoBehaviour
{
    [SerializeField] private Sprite[] debrisMasks;

    // The size increases of the sprite masks (with 4, 4x4, then 8x8, then 12x12, etc.)
    private const int MaskStepSize = 4;

    private const float RandomDropForceMinAmount = 5f;
    private const float RandomDropForceMaxAmount = 30f;

    private const float RandomAngularVelocityRange = 500f;

    private const float RandomLifetimeMinSeconds = 0.5f;
    private const float RandomLifetimeMaxSeconds = 2f;

    private float _lifetimeStartTimestamp;
    private float _lifetimeEndTimestamp;

    private SpriteRenderer _sr;

    private void Update()
    {
        float lifetimeProgress = Mathf.InverseLerp(_lifetimeStartTimestamp, _lifetimeEndTimestamp, Time.time);
        _sr.color = new Color(1f, 1f, 1f, 1f - lifetimeProgress);

        if (lifetimeProgress >= 1f)
        {
            Destroy(gameObject);
        }
    }

    public void Init(Sprite sprite)
    {
        _sr = GetComponent<SpriteRenderer>();
        SpriteMask sm = GetComponent<SpriteMask>();
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        BoxCollider2D bc = GetComponent<BoxCollider2D>();

        _sr.sprite = sprite;

        int debrisMaskIdx = Mathf.Clamp((int) sprite.rect.width / MaskStepSize - 1, 0, debrisMasks.Length);
        sm.sprite = debrisMasks[debrisMaskIdx];

        bc.size = sprite.bounds.size;

        Vector2 randomDropForce = Random.insideUnitCircle * Random.Range(RandomDropForceMinAmount, RandomDropForceMaxAmount);
        rb.velocity = randomDropForce;
        rb.angularVelocity = Random.Range(-RandomAngularVelocityRange, RandomAngularVelocityRange);

        _lifetimeStartTimestamp = Time.time;
        _lifetimeEndTimestamp = Time.time + Random.Range(RandomLifetimeMinSeconds, RandomLifetimeMaxSeconds);

        // Enable this script to start calling Update and counting down lifetime
        enabled = true;
    }
}