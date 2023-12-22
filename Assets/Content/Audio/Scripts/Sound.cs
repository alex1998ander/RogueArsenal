using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Sound
{
    [SerializeField] public AudioClip audioClip;
    [SerializeField] public float volumeScale = 1f;
    [SerializeField] public float pitchVariationRange = 0f;
    [SerializeField] public float initialDelay = 0f;
}