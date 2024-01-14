using UnityEngine;

public class MainMenuViewManager : MonoBehaviour
{
    [SerializeField] private StringButtonView startButton;
    [SerializeField] private StringButtonView settingsButton;
    [SerializeField] private StringButtonView sandboxButton;
    [SerializeField] private GameObject root;

    private void Start()
    {
        startButton.Initialize(() =>
        {
            root.SetActive(false);
            LevelManager.StartRound();
        });
        settingsButton.Initialize(() =>
        {
            root.SetActive(false);
            LevelManager.ShowSettingsMenu(true);
        });
        sandboxButton.Initialize(() =>
        {
            root.SetActive(false);
            LevelManager.LoadSandboxLevel();
        });
    }
}