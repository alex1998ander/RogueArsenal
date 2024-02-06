using UnityEngine;

public class DebrisController : MonoBehaviour
{
    [SerializeField] private Sprite[] debrisMasks;

    // The size increases of the sprite masks (with 4, 4x4, then 8x8, then 12x12, etc.)
    private const int MaskStepSize = 4;

    private const float DropForce = 15f;
    private const float RotationForce = 500f;

    public void Init(Sprite sprite)
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        SpriteMask sm = GetComponent<SpriteMask>();
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        BoxCollider2D bc = GetComponent<BoxCollider2D>();

        sr.sprite = sprite;

        int debrisMaskIdx = Mathf.Clamp((int) sprite.rect.width / MaskStepSize - 1, 0, debrisMasks.Length);
        sm.sprite = debrisMasks[debrisMaskIdx];

        bc.size = sprite.bounds.size;

        Vector2 randomDropForce = Random.insideUnitCircle * DropForce;
        rb.velocity = randomDropForce;
        rb.angularVelocity = Random.Range(-RotationForce, RotationForce);
    }
}