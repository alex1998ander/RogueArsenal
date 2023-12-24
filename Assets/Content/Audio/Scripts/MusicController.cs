using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioController))]
[RequireComponent(typeof(AudioSource))]
public class MusicController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSourceMusicFst;
    [SerializeField] private AudioSource audioSourceMusicSnd;

    // Gameplay Music selection
    [SerializeField] private Music[] gameplayMusicSelection;

    public static AudioClipLibrary Library;

    private AudioSource _audioSourceMusicCurrent;
    private AudioSource _audioSourceMusicUnused;

    private Music _currentMusic;

    private bool _currentlyPlaying;
    private bool _currentlyFading;

    private void Awake()
    {
        Library = GetComponent<AudioClipLibrary>();
        _audioSourceMusicCurrent = audioSourceMusicFst;
        _audioSourceMusicUnused = audioSourceMusicSnd;

        _currentMusic = gameplayMusicSelection[0];

        SceneManager.sceneLoaded += (scene, mode) =>
        {
            EventManager.OnLevelEnter.Subscribe(FadeRandomMainLoop);
            EventManager.OnLevelExit.Subscribe(FadeRandomUpgradeLoop);
            EventManager.OnMainMenuEnter.Subscribe(PlayRandomUpgradeLoop);
            EventManager.OnPauseGame.Subscribe(MuffleMusic);
        };

        SceneManager.sceneUnloaded += scene =>
        {
            EventManager.OnLevelEnter.Unsubscribe(FadeRandomMainLoop);
            EventManager.OnLevelExit.Unsubscribe(FadeRandomUpgradeLoop);
            EventManager.OnMainMenuEnter.Unsubscribe(PlayRandomUpgradeLoop);
            EventManager.OnPauseGame.Unsubscribe(MuffleMusic);
        };

        PlayIntro();
    }

    /// <summary>
    /// Plays the intro of the current music. This cancels the currently playing track or a current fade.
    /// </summary>
    public void PlayIntro()
    {
        _ResetAudioSources();
        _PlayAudioClipOnCurrentAudioSource(_currentMusic.intro);
    }

    /// <summary>
    /// Plays a main loop of the current music. This cancels the currently playing track or a current fade.
    /// </summary>
    public void PlayRandomMainLoop()
    {
        _ResetAudioSources();

        AudioClip[] mainLoops = _currentMusic.mainLoops;
        _PlayAudioClipOnCurrentAudioSource(mainLoops[Random.Range(0, mainLoops.Length)]);
    }

    /// <summary>
    /// Fades music into a random main loop of the current music.
    /// </summary>
    public void FadeRandomMainLoop()
    {
        AudioClip[] mainLoops = _currentMusic.mainLoops;
        FadeMusic(mainLoops[Random.Range(0, mainLoops.Length)]);
    }

    /// <summary>
    /// Plays a random upgrade loop of the current music. This cancels the currently playing track or a current fade.
    /// </summary>
    public void PlayRandomUpgradeLoop()
    {
        _ResetAudioSources();

        AudioClip[] upgradeLoops = _currentMusic.upgradeLoops;
        _PlayAudioClipOnCurrentAudioSource(upgradeLoops[Random.Range(0, upgradeLoops.Length)]);
    }

    /// <summary>
    /// Fades music into a random upgrade loop of the current music.
    /// </summary>
    public void FadeRandomUpgradeLoop()
    {
        AudioClip[] upgradeLoops = _currentMusic.upgradeLoops;
        FadeMusic(upgradeLoops[Random.Range(0, upgradeLoops.Length)]);
    }

    /// <summary>
    /// Apply or remove a damped effect to the current music track.
    /// </summary>
    /// <param name="enabled">Bool whether the effect is added or removed.</param>
    public void MuffleMusic(bool enabled)
    {
        if (!_currentlyPlaying) return;

        if (enabled)
        {
            _audioSourceMusicCurrent.outputAudioMixerGroup.audioMixer.SetFloat("lowPassCutOff", 200f);
        }
        else
        {
            _audioSourceMusicCurrent.outputAudioMixerGroup.audioMixer.SetFloat("lowPassCutOff", 22000f);
        }
    }

    /// <summary>
    /// Tries to start a fade between two music tracks. Only works if no other fade is currently active. If no other track is currently playing, the music will play normally.
    /// </summary>
    /// <param name="clip">Music that fades in</param>
    /// <returns>Whether the fade was started successfully</returns>
    private bool FadeMusic(AudioClip clip)
    {
        if (_currentlyFading)
        {
            return false;
        }

        if (_currentlyPlaying)
        {
            _currentlyFading = true;
            StartCoroutine(StartFade(clip, _currentMusic.fadeDuration));
        }
        else
        {
            _PlayAudioClipOnCurrentAudioSource(clip);
        }

        return true;
    }

    /// <summary>
    /// Starts a fade between two music tracks
    /// </summary>
    /// <param name="clip"></param>
    /// <param name="duration"></param>
    private IEnumerator StartFade(AudioClip clip, float duration)
    {
        float beatLength = 60f / _currentMusic.bpm * 4f;
        int lastBeat = Mathf.FloorToInt(_audioSourceMusicCurrent.timeSamples /
                                        (_audioSourceMusicCurrent.clip.frequency * beatLength));

        while (Mathf.FloorToInt(_audioSourceMusicCurrent.timeSamples /
                                (_audioSourceMusicCurrent.clip.frequency * beatLength)) == lastBeat)
        {
            yield return null;
        }

        StartCoroutine(StartAudioSourceVolumeFade(_audioSourceMusicCurrent, duration, false));
        StartCoroutine(StartAudioSourceVolumeFade(_audioSourceMusicUnused, duration, true));

        // Swap current used audio source reference
        (_audioSourceMusicCurrent, _audioSourceMusicUnused) = (_audioSourceMusicUnused, _audioSourceMusicCurrent);

        _PlayAudioClipOnCurrentAudioSource(clip);
    }

    /// <summary>
    /// Starts a volume fade coroutine from one audio source to the other.
    /// </summary>
    /// <param name="audioSource">Audio source to be faded to</param>
    /// <param name="duration">Fade duration</param>
    /// <param name="fadeIn">bool whether to be faded in or faded out</param>
    private IEnumerator StartAudioSourceVolumeFade(AudioSource audioSource, float duration, bool fadeIn)
    {
        float startVolume = fadeIn ? 0 : 1;
        float targetVolume = fadeIn ? 1 : 0;

        float volumeChangePerSecond = (targetVolume - startVolume) / duration;
        float currentTime = 0f;

        while (currentTime < duration)
        {
            currentTime += Time.unscaledDeltaTime;
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

        _currentlyFading = false;
    }

    /// <summary>
    /// Plays an audio clip after a delay.
    /// </summary>
    /// <param name="clip">Audio clip to play</param>
    /// <param name="duration">Delay in seconds</param>
    private IEnumerator PlayMusicAfterDelay(AudioClip clip, float duration)
    {
        yield return new WaitForSeconds(duration);
        _PlayAudioClipOnCurrentAudioSource(clip);
    }

    /// <summary>
    /// Plays audio clip.
    /// </summary>
    /// <param name="clip">Audio clip</param>
    private void _PlayAudioClipOnCurrentAudioSource(AudioClip clip)
    {
        AudioSource audioSource = _audioSourceMusicCurrent;
        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.Play();
        _currentlyPlaying = true;
    }

    /// <summary>
    /// Resets the potentially playing audio sources to their default states (not playing and not fading).
    /// </summary>
    private void _ResetAudioSources()
    {
        // Stops current tracks
        audioSourceMusicFst.Stop();
        audioSourceMusicSnd.Stop();

        // Resets current fading
        StopAllCoroutines();
        audioSourceMusicFst.volume = 1;
        audioSourceMusicSnd.volume = 1;
        _currentlyFading = false;

        _audioSourceMusicCurrent = audioSourceMusicFst;
        _audioSourceMusicUnused = audioSourceMusicSnd;
    }
}