using UnityEngine;

public class SpriteOrderer : MonoBehaviour
{
    [SerializeField] public int orderOffset;
    [SerializeField] private SpriteOrderer parentSpriteOrderer;

    public int OrderId { get; set; }

    private const float AccuracyMultiplier = 100f;

    private SpriteRenderer _sr;

    private void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (_sr)
        {
            if (parentSpriteOrderer)
                OrderId = parentSpriteOrderer.OrderId + orderOffset;
            else
                // Multiplied with transform position to increase precision (With AccuracyMultiplier = 100, instead of rounding 0.423 -> 0, round 40.3 -> 40 to not lose too much information)
                OrderId = Mathf.RoundToInt(-transform.position.y * AccuracyMultiplier) + orderOffset;

            _sr.sortingOrder = OrderId;
        }
    }
}