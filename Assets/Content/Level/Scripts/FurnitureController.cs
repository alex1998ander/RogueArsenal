using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class FurnitureController : MonoBehaviour, ICharacterHealth
{
    [SerializeField] private GameObject debrisPrefab;

    // Set sprite of furniture using serialized field to dynamically adjust collider and nav mesh sizes based on sprite
    [SerializeField] private Sprite furnitureSprite;

    // Sizes of the grid used to split the sprite (1 meaning one row/column = no splitting, 2 = splitting in half, etc.)
    [SerializeField, Range(1, 5)] private int splitGridX = 2;
    [SerializeField, Range(1, 5)] private int splitGridY = 2;

    [SerializeField] private AudioClip[] breakSounds;

    [SerializeField] private BoxCollider2D trigger;
    [SerializeField] private BoxCollider2D collider;

    private SpriteRenderer _sr;
    private AudioSource _as;

    // Split sprites of this piece of furniture representing debris 
    private Sprite[,] _debrisSprites;

    // FurnitureControllers of all child objects
    private FurnitureController[] _childFurniture;

    private Transform _playerTransform;

    private bool _broken;

    private void Start()
    {
        _as = GetComponent<AudioSource>();

        _sr = GetComponent<SpriteRenderer>();
        _sr.sprite = furnitureSprite;
        _debrisSprites = _SplitSprite(furnitureSprite, splitGridX, splitGridY);

        _childFurniture = GetComponentsInChildren<FurnitureController>();

        _playerTransform = FindObjectOfType<PlayerController>().GetComponent<Transform>();
    }

    /// <summary>
    /// Breaks this piece of furniture and all child furniture objects.
    /// </summary>
    private void _Break()
    {
        if (_broken)
            return;

        _broken = true;
        foreach (Sprite sprite in _debrisSprites)
        {
            // TODO? instantiate at centers of split regions 
            GameObject debris = Instantiate(debrisPrefab, transform.position, Quaternion.identity);
            DebrisController dc = debris.GetComponent<DebrisController>();
            dc.Init(sprite);
        }

        foreach (FurnitureController childFurniture in _childFurniture)
        {
            childFurniture._Break();
        }

        AudioClip randomClip = breakSounds[Random.Range(0, breakSounds.Length)];
        _as.PlayOneShot(randomClip);

        trigger.enabled = false;
        collider.enabled = false;
        _sr.enabled = false;

        Destroy(gameObject, randomClip.length);
    }

    /// <summary>
    /// Splits a given sprite into multiple, equally sized sprites using a certain grid size.
    /// Split sprites are also clamped to be square shaped to insure consistent masking later on.
    /// </summary>
    /// <param name="sprite">The original sprite to split.</param>
    /// <param name="gridX">The grid X size (number of columns)</param>
    /// <param name="gridY">The grid Y size (number of rows)</param>
    /// <returns>The array containing the split sprites, always of size gridX * gridY</returns>
    private static Sprite[,] _SplitSprite(Sprite sprite, int gridX, int gridY)
    {
        Sprite[,] splitSprites = new Sprite[gridX, gridY];

        // calculated size of a single split sprite
        float splitSpriteWidth = sprite.rect.width / gridX;
        float splitSpriteHeight = sprite.rect.height / gridY;

        // offsets to correct centers of sprites, needed because of squaring of sprites 
        float widthOffset = 0f;
        float heightOffset = 0f;

        // Take smaller width/height to use a sprite size, calculate sprite offset
        float splitSpriteSize;
        if (splitSpriteWidth < splitSpriteHeight)
        {
            heightOffset = (splitSpriteHeight - splitSpriteWidth) / 2f;
            splitSpriteSize = splitSpriteWidth;
        }
        else
        {
            widthOffset = (splitSpriteWidth - splitSpriteHeight) / 2f;
            splitSpriteSize = splitSpriteHeight;
        }

        // TODO? Extract rounding functionality, remove magic number
        // Round size down to nearest multiple of 4 (4 because of the debris masks increasing in steps of 4)
        // to insure consistency? honestly kinda overkill but it looks nicer for some sprites
        splitSpriteSize = ((int) splitSpriteSize / 4) * 4;

        for (int y = 0; y < gridY; y++)
        {
            for (int x = 0; x < gridX; x++)
            {
                Rect splitSpriteRect = new Rect(
                    sprite.rect.x + x * splitSpriteWidth + widthOffset,
                    sprite.rect.y + y * splitSpriteHeight + heightOffset,
                    splitSpriteSize,
                    splitSpriteSize
                );

                // Vector2(0.5, 0.5f) means the pivot of the sprite is in the center
                Sprite splitSprite = Sprite.Create(sprite.texture, splitSpriteRect, new Vector2(0.5f, 0.5f), sprite.pixelsPerUnit);
                splitSprites[x, y] = splitSprite;
            }
        }

        return splitSprites;
    }

    private void OnValidate()
    {
        if (!_sr)
            _sr = GetComponent<SpriteRenderer>();

        if (!furnitureSprite)
        {
            _sr.sprite = null;
            return;
        }

        NavMeshObstacle ob = GetComponent<NavMeshObstacle>();

        // Lazy hack: only do trigger/collider adjustments on initial setting of furniture sprite so they can be edited afterwards (to edit collider for easier movement)
        if (_sr.sprite != null)
        {
            // set nav mesh obstacle size and offset to collider dimensions for more accurate pathfinding
            // set z value to 1 so obstacle still carves out nav mesh
            ob.size = new Vector3(collider.size.x, collider.size.y, 1f);
            ob.center = collider.offset;
            return;
        }

        // OnValidate is called when adjusting values in the editor (e.g. changing a value, changing the furniture sprite, etc.)
        // use this to automatically adjust the trigger/collider and nav mesh obstacle sizes to fit the sprite exactly so manually adjusting those is not needed
        _sr.sprite = furnitureSprite;
        trigger.size = furnitureSprite.bounds.size;
        trigger.offset = Vector2.zero;
        collider.size = furnitureSprite.bounds.size;
        collider.offset = Vector2.zero;

        ob.size = furnitureSprite.bounds.size;
        ob.center = Vector3.zero;
    }

    public void InflictDamage(float damageAmount, bool fatal = false, bool ignoreInvulnerability = false)
    {
        _Break();
    }

    public bool IsDead()
    {
        return _broken;
    }
}