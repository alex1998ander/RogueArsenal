using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    // Audio sources for the intro, the main loop and the upgrade selection loop
    [SerializeField] private AudioSource intro, mainLoop, upgradeSelectionLoop;

    // the BPM (Beats per minute) and time signature of the music
    // All used song loops should have the same BPM and time signature
    [SerializeField] private int musicBpm, timeSignature;

    // Length of a bar of music in seconds
    private double _barLengthInSeconds;

    // The point in time where the next bar of the music will start playing
    private double _nextBarStartTime;

    // Should the music loop start to fade into another the next bar? 
    private bool _startFadeOnNextBar;

    // Is the main loop currently playing?
    private bool _mainLoopPlaying;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        EventManager.OnLevelEnter.Subscribe(InitializeMusicFadeOnSceneChange);
        EventManager.OnLevelExit.Subscribe(InitializeMusicFadeOnSceneChange);

        _mainLoopPlaying = true;
        _startFadeOnNextBar = false;
        upgradeSelectionLoop.volume = 0f;

        // 60 seconds divided by bpm equals the length of a single beat in seconds
        // multiply with time signature to get length of a full bar
        _barLengthInSeconds = (60d / musicBpm) * timeSignature;

        // Play the intro, schedule the main und upgrade loops to start playing at the point in time where
        // the intro stops playing to make sure they are synchronized
        // intro.PlayScheduled(AudioSettings.dspTime);
        intro.PlayScheduled(AudioSettings.dspTime);
        intro.SetScheduledEndTime(AudioSettings.dspTime + intro.clip.length);
        _nextBarStartTime = AudioSettings.dspTime + intro.clip.length;
        mainLoop.PlayScheduled(_nextBarStartTime);
        upgradeSelectionLoop.PlayScheduled(_nextBarStartTime);
    }

    private void Update()
    {
        if (AudioSettings.dspTime >= _nextBarStartTime)
        {
            Debug.Log("<color=red>Bar Play</color>");

            _nextBarStartTime = AudioSettings.dspTime + _barLengthInSeconds;

            // if (_startFadeOnNextBar)
            // {
            //     if (_mainLoopPlaying)
            //     {
            //         StartCoroutine(StartVolumeFade(mainLoop, (float) _barLengthInSeconds, 0f));
            //         StartCoroutine(StartVolumeFade(upgradeSelectionLoop, (float) _barLengthInSeconds, 1f));
            //     }
            //     else
            //     {
            //         StartCoroutine(StartVolumeFade(upgradeSelectionLoop, (float) _barLengthInSeconds, 0f));
            //         StartCoroutine(StartVolumeFade(mainLoop, (float) _barLengthInSeconds, 1f));
            //     }
            //
            //     _mainLoopPlaying = !_mainLoopPlaying;
            //     _startFadeOnNextBar = false;
            // }
        }
    }

    /// <summary>
    /// Starts a volume fade on the given audio source after a certain delay.
    /// </summary>
    /// <param name="audioSource">The audio to fade</param>
    /// <param name="duration">The duration of the fade</param>
    /// <param name="targetVolume">The target volume the audio volume fades to</param>
    /// <param name="playAudioSourceBeforeFade">Starts playing the audio source before the fade starts</param>
    /// <param name="stopAudioSourceAfterFade">Stops playing the audio source after the fade has finished</param>
    public static IEnumerator StartVolumeFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0f;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
    }


    /// <summary>
    /// Initializes the fade of the music when the scene is changed.
    /// Happens when level was exited and upgrade selection screen is entered or
    /// upgrade selection screen was exited and new level was entered
    /// </summary>
    private void InitializeMusicFadeOnSceneChange()
    {
        Debug.Log("init music fade");

        if (_mainLoopPlaying)
        {
            StartCoroutine(StartVolumeFade(mainLoop, (float) _barLengthInSeconds, 0f));
            StartCoroutine(StartVolumeFade(upgradeSelectionLoop, (float) _barLengthInSeconds, 1f));
        }
        else
        {
            StartCoroutine(StartVolumeFade(upgradeSelectionLoop, (float) _barLengthInSeconds, 0f));
            StartCoroutine(StartVolumeFade(mainLoop, (float) _barLengthInSeconds, 1f));
        }

        _mainLoopPlaying = !_mainLoopPlaying;
        _startFadeOnNextBar = false;
    }
}