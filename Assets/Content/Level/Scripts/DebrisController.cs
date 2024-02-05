using UnityEngine;

public class DebrisController : MonoBehaviour
{
    [SerializeField] private Sprite debrisMask_4x4;
    [SerializeField] private Sprite debrisMask_8x8;
    [SerializeField] private Sprite debrisMask_16x16;
    [SerializeField] private Sprite debrisMask_32x32;
    [SerializeField] private Sprite debrisMask_64x64;

    private SpriteRenderer _sr;
    private SpriteMask _sm;
    private Rigidbody2D _rb;
    private BoxCollider2D _bc;

    public void Init(Sprite sprite)
    {
        _sr = GetComponent<SpriteRenderer>();
        _sm = GetComponent<SpriteMask>();
        _rb = GetComponent<Rigidbody2D>();
        _bc = GetComponent<BoxCollider2D>();

        _sr.sprite = sprite;

        switch (sprite.rect.width)
        {
            case < 8f:
                _sm.sprite = debrisMask_4x4;
                break;
            case < 16f:
                _sm.sprite = debrisMask_8x8;
                break;
            case < 32f:
                _sm.sprite = debrisMask_16x16;
                break;
            case < 64f:
                _sm.sprite = debrisMask_32x32;
                break;
            default:
                _sm.sprite = debrisMask_64x64;
                break;
        }

        _bc.size = sprite.bounds.size;

        Vector2 randomDropForce = Random.insideUnitCircle * 15f;
        _rb.velocity = randomDropForce;
        // TODO: Random angular momentum on rigidbody
    }
}