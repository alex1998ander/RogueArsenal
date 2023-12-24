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
    public Sound playerHit, playerShot, playerDash;

    [Header("Enemy Sound Clips")] public Sound enemyHit;
    public Sound enemyShot;

    [Header("Other Sound Clips")] public Sound bulletDestroyed;

    // Audio Source to play sounds
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        // Player sounds
        Action playPlayerPhoenix = () => { _PlaySound(playerPhoenix); };
        Action playPlayerHit = () => { _PlaySound(playerHit); };
        Action playPlayerShot = () => { _PlaySound(playerShot); };
        Action playPlayerDash = () => { _PlaySound(playerDash); };

        // Enemy sounds
        Action<float> playEnemyHit = (damage) => { _PlaySound(enemyHit); };
        Action playEnemyShot = () => { _PlaySound(enemyShot); };

        // Other sounds
        Action playBulletDestroyed = () => { _PlaySound(bulletDestroyed); };

        // TODO: Potentially unnecessary to continuously subscribe/unsubscribe?
        SceneManager.sceneLoaded += (scene, mode) =>
        {
            EventManager.OnPlayerPhoenixed.Subscribe(playPlayerPhoenix);
            EventManager.OnPlayerHit.Subscribe(playPlayerHit);
            EventManager.OnPlayerShotFired.Subscribe(playPlayerShot);
            EventManager.OnPlayerDash.Subscribe(playPlayerDash);
            EventManager.OnEnemyDamage.Subscribe(playEnemyHit);
            EventManager.OnEnemyShotFired.Subscribe(playEnemyShot);
            EventManager.OnPlayerBulletDestroyed.Subscribe(playBulletDestroyed);
            EventManager.OnEnemyBulletDestroyed.Subscribe(playBulletDestroyed);
        };

        SceneManager.sceneUnloaded += scene =>
        {
            EventManager.OnPlayerPhoenixed.Unsubscribe(playPlayerPhoenix);
            EventManager.OnPlayerHit.Unsubscribe(playPlayerHit);
            EventManager.OnPlayerShotFired.Unsubscribe(playPlayerShot);
            EventManager.OnPlayerDash.Unsubscribe(playPlayerDash);
            EventManager.OnEnemyDamage.Unsubscribe(playEnemyHit);
            EventManager.OnEnemyShotFired.Unsubscribe(playEnemyShot);
            EventManager.OnPlayerBulletDestroyed.Unsubscribe(playBulletDestroyed);
            EventManager.OnEnemyBulletDestroyed.Unsubscribe(playBulletDestroyed);
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
        _audioSource.pitch = Random.Range(1f - sound.pitchVariationRange, 1f + sound.pitchVariationRange);
        _audioSource.PlayOneShot(sound.audioClip, sound.volumeScale);
    }
}