using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 1f;

    private void Awake()
    {
        EventManager.OnLevelEnter.Subscribe(PlayGamePlayLoop);
        EventManager.OnLevelExit.Subscribe(PlayUpgradeLoop);
        EventManager.OnMainMenuEnter.Subscribe(PlayMainMenuLoop);
        EventManager.OnPauseGame.Subscribe(MuffleMusic);
        EventManager.OnPlayerShotFired.Subscribe(PlayLaserShotPlayer);
        EventManager.OnEnemyShotFired.Subscribe(PlayLaserShotEnemy);
        EventManager.OnPlayerDash.Subscribe(PlayPlayerDash);
        EventManager.OnPlayerPhoenixed.Subscribe(PlayPlayerPhoenixed);
        EventManager.OnPlayerBulletDestroyed.Subscribe(PlayBulletDestroyed);
        EventManager.OnEnemyBulletDestroyed.Subscribe(PlayBulletDestroyed);

        PlayMainMenuLoop();
    }

    private void PlayMainMenuLoop()
    {
        AudioController.Play(AudioController.library.upgradeLoop3);
    }

    private void PlayUpgradeLoop()
    {
        AudioController.FadeMusic(AudioController.library.upgradeLoop1, fadeDuration);
    }

    private void PlayGamePlayLoop()
    {
        AudioController.FadeMusic(AudioController.library.mainTheme, fadeDuration);
    }

    private void MuffleMusic(bool muffle)
    {
        AudioController.MuffleMusic(muffle);
    }

    private void PlayPlayerDash()
    {
        AudioController.Play(AudioController.library.playerDash);
    }

    private void PlayPlayerPhoenixed()
    {
        StartCoroutine(PlaySoundDelayed(AudioController.library.playerPhoenix));
    }

    private void PlayLaserShotPlayer()
    {
        AudioController.Play(AudioController.library.laserShotPlayer);
    }

    private void PlayLaserShotEnemy()
    {
        AudioController.Play(AudioController.library.laserShotEnemy);
    }

    private void PlayBulletDestroyed()
    {
        AudioController.Play(AudioController.library.bulletDestroyed);
    }

    /// <summary>
    /// Plays a given sound after its delay.
    /// </summary>
    /// <param name="sound">The given sound to play</param>
    private IEnumerator PlaySoundDelayed(Sound sound)
    {
        yield return new WaitForSeconds(sound.initialDelay);
        AudioController.Play(sound);
    }

    private void OnDestroy()
    {
        EventManager.OnLevelEnter.Unsubscribe(PlayGamePlayLoop);
        EventManager.OnLevelExit.Unsubscribe(PlayUpgradeLoop);
        EventManager.OnPauseGame.Unsubscribe(MuffleMusic);
        EventManager.OnPlayerShotFired.Unsubscribe(PlayLaserShotPlayer);
        EventManager.OnEnemyShotFired.Unsubscribe(PlayLaserShotEnemy);
    }
}