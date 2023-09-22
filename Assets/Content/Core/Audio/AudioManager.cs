using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 1f;

    private void Awake()
    {
        EventManager.OnLevelEnter.Subscribe(PlayMainLoop);
        EventManager.OnLevelExit.Subscribe(PlayUpgradeLoop);
        EventManager.OnPauseGame.Subscribe(MuffleMusic);
    }

    private void Start()
    {
        AudioController.Play(AudioController.library.upgradeLoop3);
    }

    private void PlayUpgradeLoop()
    {
        AudioController.FadeMusic(AudioController.library.upgradeLoop1, fadeDuration);
    }

    private void PlayMainLoop()
    {
        AudioController.FadeMusic(AudioController.library.mainTheme, fadeDuration);
    }

    private void MuffleMusic(bool muffle)
    {
        AudioController.MuffleMusic(muffle);
    }

    private void OnDestroy()
    {
        EventManager.OnLevelEnter.Unsubscribe(PlayMainLoop);
        EventManager.OnLevelExit.Unsubscribe(PlayUpgradeLoop);
        EventManager.OnPauseGame.Unsubscribe(MuffleMusic);
    }
}