using UnityEngine;

public class MainMenuViewManager : MonoBehaviour
{
    [SerializeField] private StringButtonView startButton;
    [SerializeField] private StringButtonView settingsButton;
    [SerializeField] private StringButtonView sandboxButton;

    private void Start()
    {
        startButton.Initialize(LevelManager.StartRound);
        settingsButton.Initialize(() => LevelManager.ShowSettingsMenu(true));
        sandboxButton.Initialize(LevelManager.LoadSandboxLevel);
    }
}