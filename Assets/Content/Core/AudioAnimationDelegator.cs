using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains public functions which an Animator can access to play/stop audio clips.
/// </summary>
public class AudioAnimationDelegator : MonoBehaviour
{
    [SerializeField] private List<KeyAudioPair> audioSources;

    private readonly Dictionary<string, AudioSource> _audioSources = new();

    private void Awake()
    {
        foreach (KeyAudioPair pair in audioSources)
        {
            _audioSources[pair.Key] = pair.Value;
        }
    }

    public void PlayClip(string clipName)
    {
        _audioSources[clipName]?.Play();
    }

    public void StopClip(string clipName)
    {
        _audioSources[clipName]?.Stop();
    }
}

[Serializable]
public class KeyAudioPair
{
    public string Key;
    public AudioSource Value;

    protected KeyAudioPair(string key, AudioSource value)
    {
        Key = key;
        Value = value;
    }
}