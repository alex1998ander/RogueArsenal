using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains public functions which an Animator can access to play/stop audio clips.
/// </summary>
public class AudioAnimationDelegator : MonoBehaviour
{
    [SerializeField] private List<KeyAudioPair> clips;

    private readonly Dictionary<string, AudioSource> _clips = new();

    private void Awake()
    {
        foreach (KeyAudioPair pair in clips)
        {
            _clips[pair.Key] = pair.Value;
        }
    }

    public void PlayClip(string clipName)
    {
        _clips[clipName]?.Play();
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