using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(SFXController))]
[RequireComponent(typeof(MusicController))]
public class AudioController : MonoBehaviour
{
    private static AudioController _instance;

    [SerializeField] private AudioMixer audioMixerMaster;

    private void Awake()
    {
        if (_instance != null)
            Destroy(_instance.gameObject);

        _instance = this;
        DontDestroyOnLoad(gameObject);
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