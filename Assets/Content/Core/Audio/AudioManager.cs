using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioClipLibrary))]
public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;

    public static AudioClipLibrary library;

    [SerializeField] private AudioMixer audioMixerMaster;

    [SerializeField] private AudioSource audioSourceMusicFst;
    [SerializeField] private AudioSource audioSourceMusicSnd;
    [SerializeField] private AudioSource audioSourceSound;

    private readonly float _bpm = 105f;

    private AudioSource _audioSourceMusicCurrent;
    private AudioSource _audioSourceMusicUnused;

    private bool _currentlyPlaying = false;
    private bool _currentlyFading = false;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _instance = this;
        library = GetComponent<AudioClipLibrary>();
        _audioSourceMusicCurrent = audioSourceMusicFst;
        _audioSourceMusicUnused = audioSourceMusicSnd;
    }

    /// <summary>
    /// Plays a sound.
    /// </summary>
    /// <param name="sound">Sound instance containing the audio clip</param>
    public static void Play(Sound sound)
    {
        _instance.audioSourceSound.PlayOneShot(sound.audioClip);
        _instance._currentlyPlaying = true;
    }

    /// <summary>
    /// Plays music. This cancels the currently playing track or a current fade.
    /// </summary>
    /// <param name="music">Music instance containing the audio clip</param>
    public static void Play(Music music)
    {
        // Stops current tracks
        _instance.audioSourceMusicFst.Stop();
        _instance.audioSourceMusicSnd.Stop();

        // Resets the volume if it has just been faded
        _instance.audioSourceMusicFst.volume = 1;
        _instance.audioSourceMusicSnd.volume = 1;

        _instance._audioSourceMusicCurrent = _instance.audioSourceMusicFst;
        _instance._audioSourceMusicUnused = _instance.audioSourceMusicSnd;

        PlayMusicOnCurrentAudioSource(music);
        _instance._currentlyPlaying = true;
    }

    /// <summary>
    /// Plays music.
    /// </summary>
    /// <param name="music">Music instance containing the audio clip</param>
    private static void PlayMusicOnCurrentAudioSource(Music music)
    {
        AudioSource audioSource = _instance._audioSourceMusicCurrent;

        audioSource.clip = music.audioClip;
        audioSource.loop = true;
        audioSource.Play();
    }

    /// <summary>
    /// Apply or remove a damped effect to the current music track.
    /// </summary>
    /// <param name="enabled">Bool whether the effect is added or removed.</param>
    public static void MuffleMusic(bool enabled)
    {
        if (!_instance._currentlyPlaying) return;

        if (enabled)
        {
            _instance._audioSourceMusicCurrent.outputAudioMixerGroup.audioMixer.SetFloat("lowPassCutOff", 200f);
        }
        else
        {
            _instance._audioSourceMusicCurrent.outputAudioMixerGroup.audioMixer.SetFloat("lowPassCutOff", 22000f);
        }
    }

    /// <summary>
    /// Fades between two music tracks. Only works if no other fade is currently active.
    /// </summary>
    /// <param name="targetMusic">Music that fades in</param>
    /// <param name="duration">Fade duration</param>
    /// <returns>Bool whether the fade was started successfully</returns>
    public static bool FadeMusic(Music targetMusic, float duration)
    {
        if (_instance._currentlyFading || !_instance._currentlyPlaying) return false;

        _instance._currentlyFading = true;
        _instance.StartCoroutine(StartFade(targetMusic, duration));

        return true;
    }

    private static IEnumerator StartFade(Music targetMusic, float duration)
    {
        float beatLenght = 60f / _instance._bpm * 4f;
        int lastBeat = Mathf.FloorToInt(_instance._audioSourceMusicCurrent.timeSamples / (_instance._audioSourceMusicCurrent.clip.frequency * beatLenght));

        while (Mathf.FloorToInt(_instance._audioSourceMusicCurrent.timeSamples / (_instance._audioSourceMusicCurrent.clip.frequency * beatLenght)) == lastBeat)
        {
            yield return null;
        }

        _instance.StartCoroutine(StartAudioSourceVolumeFade(_instance._audioSourceMusicCurrent, duration, false));
        _instance.StartCoroutine(StartAudioSourceVolumeFade(_instance._audioSourceMusicUnused, duration, true));

        // Swap current used audio source reference
        (_instance._audioSourceMusicCurrent, _instance._audioSourceMusicUnused) = (_instance._audioSourceMusicUnused, _instance._audioSourceMusicCurrent);

        PlayMusicOnCurrentAudioSource(targetMusic);
    }

    /// <summary>
    /// Fade coroutine.
    /// </summary>
    /// <param name="audioSource">Audio source to be faded to</param>
    /// <param name="duration">Fade duration</param>
    /// <param name="fadeIn">bool whether to be faded in or faded out</param>
    /// <returns></returns>
    private static IEnumerator StartAudioSourceVolumeFade(AudioSource audioSource, float duration, bool fadeIn)
    {
        float startVolume = fadeIn ? 0 : 1;
        float targetVolume = fadeIn ? 1 : 0;

        float volumeChangePerSecond = (targetVolume - startVolume) / duration;
        float currentTime = 0f;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float newVolume = startVolume + volumeChangePerSecond * currentTime;
            audioSource.volume = newVolume;
            yield return null;
        }

        // Ensure that the volume is set to the exact target value.
        audioSource.volume = targetVolume;

        // Stops the faded out track
        if (!fadeIn)
        {
            audioSource.Stop();
        }

        _instance._currentlyFading = false;
    }

    /// <summary>
    /// Master volume setter.
    /// </summary>
    /// <param name="volume">Master volume value</param>
    public static void SetMasterVolume(float volume)
    {
        _instance.audioMixerMaster.SetFloat("master", Mathf.Log10(volume) * 60);
    }

    /// <summary>
    /// Master volume getter
    /// </summary>
    /// <returns>Master volume value</returns>
    public static float GetMasterVolume()
    {
        _instance.audioMixerMaster.GetFloat("master", out float volume);
        return Mathf.Pow(10, (volume / 60.0f));
    }
}