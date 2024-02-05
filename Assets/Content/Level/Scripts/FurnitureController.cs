using UnityEngine;
using Random = UnityEngine.Random;

public class FurnitureController : MonoBehaviour
{
    [SerializeField] private GameObject debrisPrefab;
    [SerializeField] private int splitGridSize = 3;
    [SerializeField] private bool squareDebrisPieces = false;
    [SerializeField] private AudioClip breakSound;

    private Sprite[] _debrisSprites;

    private void Start()
    {
        Sprite furnitureSprite = GetComponent<SpriteRenderer>().sprite;
        _debrisSprites = _SplitSprite(furnitureSprite, splitGridSize, squareDebrisPieces);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("PlayerBullet"))
            return;

        for (int i = 0; i < splitGridSize * splitGridSize; i++)
        {
            // TODO: instantiate at centers of split regions 
            GameObject debris = Instantiate(debrisPrefab, transform.position, Quaternion.identity);

            SpriteRenderer debrisSpriteRenderer = debris.GetComponent<SpriteRenderer>();
            debrisSpriteRenderer.sprite = _debrisSprites[i];

            Rigidbody2D debrisRb = debris.GetComponent<Rigidbody2D>();
            Vector2 randomDropForce = Random.insideUnitCircle * 15f;
            debrisRb.velocity = randomDropForce;
            // TODO: Random angular momentum on rigidbody
        }

        // TODO: break sound
        Destroy(gameObject);
    }

    /// <summary>
    /// Splits a given sprite into multiple, equally sized sprites using a certain grid size.
    /// </summary>
    /// <param name="sprite">The original sprite to split.</param>
    /// <param name="gridSize">The grid size used to split the sprite</param>
    /// <param name="forceSquare"></param>
    /// <param name="forcePowerOfTwo"></param>
    /// <returns>The array containing the split sprites, always of size gridSize * gridSize</returns>
    private static Sprite[] _SplitSprite(Sprite sprite, int gridSize, bool forceSquare, bool forcePowerOfTwo = false)
    {
        Sprite[] splitSprites = new Sprite[gridSize * gridSize];

        // calculated size of a single split sprite
        float splitSpriteWidth = sprite.rect.width / gridSize;
        float splitSpriteHeight = sprite.rect.height / gridSize;

        // adjusted size of a single split sprite (changed by forcing sprites to be square / be power of two)
        float adjustedSplitSpriteWidth = splitSpriteWidth;
        float adjustedSplitSpriteHeight = splitSpriteHeight;

        // if force sprites to be square shaped, clamp it to smaller size, calculate offset accordingly
        float widthOffset = 0f;
        float heightOffset = 0f;
        if (forceSquare)
        {
            if (splitSpriteWidth < splitSpriteHeight)
            {
                heightOffset = (splitSpriteHeight - splitSpriteWidth) / 2f;
                adjustedSplitSpriteHeight = splitSpriteWidth;
            }
            else
            {
                widthOffset = (splitSpriteWidth - splitSpriteHeight) / 2f;
                adjustedSplitSpriteWidth = splitSpriteHeight;
            }
        }

        int splitSpriteIdx = 0;
        for (int y = 0; y < gridSize; y++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                Rect splitSpriteRect = new Rect(
                    sprite.rect.x + x * splitSpriteWidth + widthOffset,
                    sprite.rect.y + y * splitSpriteHeight + heightOffset,
                    adjustedSplitSpriteWidth,
                    adjustedSplitSpriteHeight
                );

                Sprite splitSprite = Sprite.Create(sprite.texture, splitSpriteRect, new Vector2(0.5f, 0.5f), sprite.pixelsPerUnit);
                splitSprites[splitSpriteIdx] = splitSprite;

                splitSpriteIdx++;
            }
        }

        return splitSprites;
    }
}