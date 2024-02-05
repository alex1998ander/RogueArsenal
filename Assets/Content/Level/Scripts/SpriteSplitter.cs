using UnityEngine;

public static class SpriteSplitter
{
    /// <summary>
    /// Splits a given sprite into multiple, equally sized sprites using a certain grid size.
    /// </summary>
    /// <param name="originalSprite">The original sprite to split.</param>
    /// <param name="gridSize">The grid size used to split the sprite</param>
    /// <returns>The array containing the split sprites, always of size gridSize * gridSize</returns>
    public static Sprite[] SplitSprite(Sprite originalSprite, int gridSize)
    {
        Sprite[] splitSprites = new Sprite[gridSize * gridSize];

        int splitSpriteIdx = 0;
        for (int y = 0; y < gridSize; y++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                float splitSpriteWidth = originalSprite.rect.width / gridSize;
                float splitSpriteHeight = originalSprite.rect.height / gridSize;
                Rect splitSpriteRect = new Rect(
                    originalSprite.rect.x + x * splitSpriteWidth,
                    originalSprite.rect.y + y * splitSpriteHeight,
                    splitSpriteWidth,
                    splitSpriteHeight
                );

                Sprite splitSprite = Sprite.Create(originalSprite.texture, splitSpriteRect, new Vector2(0.5f, 0.5f), originalSprite.pixelsPerUnit);
                splitSprites[splitSpriteIdx] = splitSprite;

                splitSpriteIdx++;
            }
        }

        return splitSprites;
    }
}