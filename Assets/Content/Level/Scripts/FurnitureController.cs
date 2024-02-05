using UnityEngine;
using Random = UnityEngine.Random;

public class FurnitureController : MonoBehaviour
{
    [SerializeField] private GameObject debrisPrefab;
    [SerializeField] private int splitGridSize = 3;
    [SerializeField] private AudioClip breakSound;

    private Sprite[] _debrisSprites;

    private void Start()
    {
        Sprite furnitureSprite = GetComponent<SpriteRenderer>().sprite;
        _debrisSprites = SpriteSplitter.SplitSprite(furnitureSprite, splitGridSize);
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
}