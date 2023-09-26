using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 1f;

    private void Awake()
    {
        EventManager.OnLevelEnter.Subscribe(PlayGamePlayLoop);
        EventManager.OnLevelExit.Subscribe(PlayUpgradeLoop);
        EventManager.OnMainMenuEnter.Subscribe(PlayMainMenuLoop);
        EventManager.OnPauseGame.Subscribe(MuffleMusic);

        PlayMainMenuLoop();
    }
    
    private void PlayMainMenuLoop()
    {
        AudioController.Play(AudioController.library.upgradeLoop3);
    }
    
    private void PlayUpgradeLoop()
    {
        AudioController.FadeMusic(AudioController.library.upgradeLoop1, fadeDuration);
    }

    private void PlayGamePlayLoop()
    {
        AudioController.FadeMusic(AudioController.library.mainTheme, fadeDuration);
    }

    private void MuffleMusic(bool muffle)
    {
        AudioController.MuffleMusic(muffle);
    }

    private void OnDestroy()
    {
        EventManager.OnLevelEnter.Unsubscribe(PlayGamePlayLoop);
        EventManager.OnLevelExit.Unsubscribe(PlayUpgradeLoop);
        EventManager.OnPauseGame.Unsubscribe(MuffleMusic);
    }
}