using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    // Erste Taste des Menüs
    [SerializeField] private GameObject pauseFirstButton;

    // Erste Taste des Optionsmenüs
    [SerializeField] private GameObject optionFirstButton;

    // Optionsmenü-Objekt
    [SerializeField] private GameObject optionsScreen;

    void Awake()
    {
        EventSystem.current.SetSelectedGameObject(pauseFirstButton);
    }

    /// <summary>
    /// Wird vor dem ersten Frame Update ausgeführt
    /// </summary>
    void Start()
    {
        Application.targetFrameRate = 144;
        QualitySettings.vSyncCount = 1;

        UpgradeManager.ResetUpgrades();
        EventSystem.current.SetSelectedGameObject(pauseFirstButton);
    }

    /// <summary>
    /// Beendet das Spiel
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("Quitting");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    /// <summary>
    /// Laedt unsere erste Scene
    /// </summary>
    public void StartGame()
    {
        LevelManager.LoadNextLevel();
        EventManager.OnStartGame.Trigger();
    }

    public void StartGameAtTheEnd()
    {
        LevelManager.levelCounter = 17;
        LevelManager.LoadNextLevel();
    }

    public void OpenOptions()
    {
        optionsScreen.SetActive(true);
        EventSystem.current.SetSelectedGameObject(optionFirstButton);
    }

    public void CloseOptions()
    {
        optionsScreen.SetActive(false);
        EventSystem.current.SetSelectedGameObject(pauseFirstButton);
    }
}