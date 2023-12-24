using UnityEngine;

public class AudioClipLibrary : MonoBehaviour
{
    [Header("Music Clips")] public Music mainTheme;
    public Music mainThemeShort, intro, introShort, upgradeLoop1, upgradeLoop2, upgradeLoop3;

    [Header("Player Sound Clips")] public Sound playerPhoenix;
    public Sound playerHit, playerDash, laserShotPlayer;

    [Header("Enemy Sound Clips")] public Sound enemyHit;
    public Sound laserShotEnemy;

    [Header("Other Sound Clips")] public Sound bulletDestroyed;
}