using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioController))]
[RequireComponent(typeof(AudioSource))]
public class SFXController : MonoBehaviour
{
    [Header("Player Sound Clips")] public Sound playerPhoenix;
    public Sound playerHit, playerShot, playerShotEmpty, playerReloadStart, playerReloadEnd, playerDash;

    [Header("Enemy Sound Clips")] public Sound enemyHit;
    public Sound enemyShot, enemyDeath;

    [Header("Other Sound Clips")] public Sound bulletDestroyed;
    public Sound currencyCollectSound, explosion, bulletBounce, healingField, shockwave, stimpack, timefreeze;

    // Audio Source to play sounds
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        // Player sounds
        Action playPlayerPhoenix = () => { _SchedulePlaySound(playerPhoenix); };
        Action playPlayerHit = () => { _SchedulePlaySound(playerHit); };
        Action playPlayerShot = () => { _SchedulePlaySound(playerShot); };
        Action playPlayerShotEmpty = () => { _SchedulePlaySound(playerShotEmpty); };
        Action playPlayerReloadStart = () => { _SchedulePlaySound(playerReloadStart); };
        Action playPlayerReloadEnd = () => { _SchedulePlaySound(playerReloadEnd); };
        Action playPlayerDash = () => { _SchedulePlaySound(playerDash); };

        // Enemy sounds
        Action<float> playEnemyHit = (damage) => { _SchedulePlaySound(enemyHit); };
        Action playEnemyShot = () => { _SchedulePlaySound(enemyShot); };
        Action<Vector3> playEnemyDeath = deathPosition => { _SchedulePlaySound(enemyDeath); };

        // Other sounds
        Action playBulletDestroyed = () => { _SchedulePlaySound(bulletDestroyed); };
        Action playCurrencyCollectSound = () => { _SchedulePlaySound(currencyCollectSound); };
        Action playExplosion = () => { _SchedulePlaySound(explosion); };
        Action playBulletBounce = () => { _SchedulePlaySound(bulletBounce); };
        Action playHealingField = () => { _SchedulePlaySound(healingField); };
        Action playShockwave = () => { _SchedulePlaySound(shockwave); };
        Action playStimpack = () => { _SchedulePlaySound(stimpack); };
        Action playTimefreeze = () => { _SchedulePlaySound(timefreeze, true); };

        // TODO: Potentially unnecessary to continuously subscribe/unsubscribe?
        SceneManager.sceneLoaded += (scene, mode) =>
        {
            EventManager.OnPhoenixRevive.Subscribe(playPlayerPhoenix);
            EventManager.OnPlayerHit.Subscribe(playPlayerHit);
            EventManager.OnPlayerShot.Subscribe(playPlayerShot);
            EventManager.OnPlayerShotEmpty.Subscribe(playPlayerShotEmpty);
            EventManager.OnWeaponReloadStart.Subscribe(playPlayerReloadStart);
            EventManager.OnWeaponReloadEnd.Subscribe(playPlayerReloadEnd);
            EventManager.OnPlayerDash.Subscribe(playPlayerDash);
            EventManager.OnEnemyDamage.Subscribe(playEnemyHit);
            EventManager.OnEnemyShotFired.Subscribe(playEnemyShot);
            EventManager.OnEnemyDeath.Subscribe(playEnemyDeath);
            EventManager.OnPlayerBulletDestroyed.Subscribe(playBulletDestroyed);
            EventManager.OnEnemyBulletDestroyed.Subscribe(playBulletDestroyed);
            EventManager.OnPlayerCollectCurrency.Subscribe(playCurrencyCollectSound);
            EventManager.OnExplosiveBulletExplosion.Subscribe(playExplosion);
            EventManager.OnBulletBounce.Subscribe(playBulletBounce);
            EventManager.OnHealingFieldStart.Subscribe(playHealingField);
            EventManager.OnShockwave.Subscribe(playShockwave);
            EventManager.OnStimpack.Subscribe(playStimpack);
            EventManager.OnTimefreeze.Subscribe(playTimefreeze);
        };

        SceneManager.sceneUnloaded += scene =>
        {
            EventManager.OnPhoenixRevive.Unsubscribe(playPlayerPhoenix);
            EventManager.OnPlayerHit.Unsubscribe(playPlayerHit);
            EventManager.OnPlayerShot.Unsubscribe(playPlayerShot);
            EventManager.OnPlayerShotEmpty.Unsubscribe(playPlayerShotEmpty);
            EventManager.OnWeaponReloadStart.Unsubscribe(playPlayerReloadStart);
            EventManager.OnWeaponReloadEnd.Unsubscribe(playPlayerReloadEnd);
            EventManager.OnPlayerDash.Unsubscribe(playPlayerDash);
            EventManager.OnEnemyDamage.Unsubscribe(playEnemyHit);
            EventManager.OnEnemyShotFired.Unsubscribe(playEnemyShot);
            EventManager.OnEnemyDeath.Unsubscribe(playEnemyDeath);
            EventManager.OnPlayerBulletDestroyed.Unsubscribe(playBulletDestroyed);
            EventManager.OnEnemyBulletDestroyed.Unsubscribe(playBulletDestroyed);
            EventManager.OnPlayerCollectCurrency.Unsubscribe(playCurrencyCollectSound);
            EventManager.OnExplosiveBulletExplosion.Unsubscribe(playExplosion);
            EventManager.OnBulletBounce.Unsubscribe(playBulletBounce);
            EventManager.OnHealingFieldStart.Unsubscribe(playHealingField);
            EventManager.OnShockwave.Unsubscribe(playShockwave);
            EventManager.OnStimpack.Unsubscribe(playStimpack);
            EventManager.OnTimefreeze.Unsubscribe(playTimefreeze);
        };
    }

    /// <summary>
    /// Schedules the playing of a given sound.
    /// </summary>
    /// <param name="sound">The given sound to play</param>
    /// <param name="ignoreTimescale">Whether the current game time scale should be ignored when pitching the sound</param>
    private void _SchedulePlaySound(Sound sound, bool ignoreTimescale = false)
    {
        if (sound.initialDelay > 0)
            StartCoroutine(PlaySoundDelayed(sound, ignoreTimescale));
        else
            _PlaySound(sound, ignoreTimescale);
    }

    /// <summary>
    /// Plays a given sound after its delay.
    /// </summary>
    /// <param name="sound">The given sound to play</param>
    /// <param name="ignoreTimescale">Whether the current game time scale should be ignored when pitching the sound</param>
    private IEnumerator PlaySoundDelayed(Sound sound, bool ignoreTimescale = false)
    {
        yield return new WaitForSeconds(sound.initialDelay);
        _PlaySound(sound, ignoreTimescale);
    }

    /// <summary>
    /// Plays a given sound.
    /// </summary>
    /// <param name="sound">The given sound to play</param>
    /// <param name="ignoreTimescale">Whether the current game time scale should be ignored when pitching the sound</param>
    private void _PlaySound(Sound sound, bool ignoreTimescale = false)
    {
        if (Time.time >= sound.NextPossiblePlayTimestamp)
        {
            sound.NextPossiblePlayTimestamp = Time.time + sound.minTimeBetweenPlays;

            float randomPitch = Random.Range(1f - sound.pitchVariationRange, 1f + sound.pitchVariationRange);
            if (!ignoreTimescale)
                randomPitch *= TimeController.GetTimeScale();
            _audioSource.pitch = randomPitch;

            _audioSource.PlayOneShot(sound.audioClip, sound.volumeScale);
        }
    }
}