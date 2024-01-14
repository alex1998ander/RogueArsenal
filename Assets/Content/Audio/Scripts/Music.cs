using System;
using UnityEngine;

[Serializable]
public class Music
{
    [SerializeField] public float bpm;
    [SerializeField] public AudioClip intro;
    [SerializeField] public AudioClip[] mainLoops;
    [SerializeField] public AudioClip[] upgradeLoops;
    [SerializeField] public float fadeDuration;
}