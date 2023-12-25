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
    public Sound playerHit, playerShot, playerReloadStart, playerReloadEnd, playerDash;

    [Header("Enemy Sound Clips")] public Sound enemyHit;
    public Sound enemyShot, enemyDeath;

    [Header("Other Sound Clips")] public Sound bulletDestroyed;
    public Sound currencyCollectSound;

    // Audio Source to play sounds
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        // Player sounds
        Action playPlayerPhoenix = () => { _SchedulePlaySound(playerPhoenix); };
        Action playPlayerHit = () => { _SchedulePlaySound(playerHit); };
        Action playPlayerShot = () => { _SchedulePlaySound(playerShot); };
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

        // TODO: Potentially unnecessary to continuously subscribe/unsubscribe?
        SceneManager.sceneLoaded += (scene, mode) =>
        {
            EventManager.OnPlayerPhoenixed.Subscribe(playPlayerPhoenix);
            EventManager.OnPlayerHit.Subscribe(playPlayerHit);
            EventManager.OnPlayerShotFired.Subscribe(playPlayerShot);
            EventManager.OnWeaponReloadStart.Subscribe(playPlayerReloadStart);
            EventManager.OnWeaponReloadEnd.Subscribe(playPlayerReloadEnd);
            EventManager.OnPlayerDash.Subscribe(playPlayerDash);
            EventManager.OnEnemyDamage.Subscribe(playEnemyHit);
            EventManager.OnEnemyShotFired.Subscribe(playEnemyShot);
            EventManager.OnEnemyDeath.Subscribe(playEnemyDeath);
            EventManager.OnPlayerBulletDestroyed.Subscribe(playBulletDestroyed);
            EventManager.OnEnemyBulletDestroyed.Subscribe(playBulletDestroyed);
            EventManager.OnPlayerCollectCurrency.Subscribe(playCurrencyCollectSound);
        };

        SceneManager.sceneUnloaded += scene =>
        {
            EventManager.OnPlayerPhoenixed.Unsubscribe(playPlayerPhoenix);
            EventManager.OnPlayerHit.Unsubscribe(playPlayerHit);
            EventManager.OnPlayerShotFired.Unsubscribe(playPlayerShot);
            EventManager.OnWeaponReloadStart.Unsubscribe(playPlayerReloadStart);
            EventManager.OnWeaponReloadEnd.Unsubscribe(playPlayerReloadEnd);
            EventManager.OnPlayerDash.Unsubscribe(playPlayerDash);
            EventManager.OnEnemyDamage.Unsubscribe(playEnemyHit);
            EventManager.OnEnemyShotFired.Unsubscribe(playEnemyShot);
            EventManager.OnEnemyDeath.Unsubscribe(playEnemyDeath);
            EventManager.OnPlayerBulletDestroyed.Unsubscribe(playBulletDestroyed);
            EventManager.OnEnemyBulletDestroyed.Unsubscribe(playBulletDestroyed);
            EventManager.OnPlayerCollectCurrency.Unsubscribe(playCurrencyCollectSound);
        };
    }

    /// <summary>
    /// Schedules the playing of a given sound.
    /// </summary>
    /// <param name="sound">The given sound to play</param>
    private void _SchedulePlaySound(Sound sound)
    {
        if (sound.initialDelay > 0)
            StartCoroutine(PlaySoundDelayed(sound));
        else
            _PlaySound(sound);
    }

    /// <summary>
    /// Plays a given sound after its delay.
    /// </summary>
    /// <param name="sound">The given sound to play</param>
    private IEnumerator PlaySoundDelayed(Sound sound)
    {
        yield return new WaitForSeconds(sound.initialDelay);
        _PlaySound(sound);
    }

    /// <summary>
    /// Plays a given sound.
    /// </summary>
    /// <param name="sound">The given sound to play</param>
    private void _PlaySound(Sound sound)
    {
        if (Time.time >= sound.NextPossiblePlayTimestamp)
        {
            sound.NextPossiblePlayTimestamp = Time.time + sound.minTimeBetweenPlays;
            _audioSource.pitch = Random.Range(1f - sound.pitchVariationRange, 1f + sound.pitchVariationRange);
            _audioSource.PlayOneShot(sound.audioClip, sound.volumeScale);
        }
    }
}