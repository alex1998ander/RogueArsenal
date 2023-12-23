using UnityEngine;
using UnityEngine.Serialization;

public class AudioClipLibrary : MonoBehaviour
{
    [Header("Music Clips")] public Music mainTheme;
    public Music mainThemeShort, intro, introShort, upgradeLoop1, upgradeLoop2, upgradeLoop3;

    [Header("Sound Clips")] public Sound playerPhoenix;
    public Sound playerHit, playerDash, laserShotPlayer, laserShotEnemy, bulletDestroyed;
}