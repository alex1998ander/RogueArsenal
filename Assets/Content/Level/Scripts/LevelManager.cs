using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

/// <summary>
/// Manages loading the different levels and the upgrade selection screen.
/// </summary>
public static class LevelManager
{
    private const int BossLevelThreshold = 18;
    private const int TotalLevelCount = 14;

    public static int levelCounter;

    private static int lastSceneIdx = -1;
    private static int secondLastSceneIdx = -2;

    private static Scene _currentActiveScene;
    private static GameObject _pauseSceneRoot;
    private static GameObject _upgradeSelectionRoot;
    private static GameObject _settingsRoot;
    private static BlurController _currentBlurController;

    private static bool _pauseMenuActive;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void Initialize()
    {
        EventManager.OnPauseGame.Subscribe(ShowPauseMenu);
        _currentActiveScene = SceneManager.GetActiveScene();

        var pauseSceneLoad = SceneManager.LoadSceneAsync("Assets/Content/Scenes/NewUI/UIPause.unity", LoadSceneMode.Additive);
        pauseSceneLoad.completed += _ =>
        {
            _pauseSceneRoot = SceneManager.GetSceneByPath("Assets/Content/Scenes/NewUI/UIPause.unity").GetRootGameObjects()[0];
            _pauseSceneRoot.SetActive(false);
        };

        var upgradeSelectionLoad = SceneManager.LoadSceneAsync("Assets/Content/Scenes/NewUI/UIUpgradeSelection.unity", LoadSceneMode.Additive);
        upgradeSelectionLoad.completed += _ =>
        {
            _upgradeSelectionRoot = SceneManager.GetSceneByPath("Assets/Content/Scenes/NewUI/UIUpgradeSelection.unity").GetRootGameObjects()[0];
            _upgradeSelectionRoot.SetActive(false);
        };

        var settingsLoad = SceneManager.LoadSceneAsync("Assets/Content/Scenes/NewUI/UISettings.unity", LoadSceneMode.Additive);
        settingsLoad.completed += _ =>
        {
            _settingsRoot = SceneManager.GetSceneByPath("Assets/Content/Scenes/NewUI/UISettings.unity").GetRootGameObjects()[0];
            _settingsRoot.SetActive(false);
        };
    }

    public static void StartGame()
    {
        UpgradeManager.ResetUpgrades();
        LoadRandomLevel();
    }

    public static void Continue()
    {
        if (levelCounter > BossLevelThreshold)
        {
            LoadMainMenu();
        }

        if (ProgressionManager.UpgradeReady)
        {
            ShowUpgradeSelection(true);
        }
        else if (levelCounter == BossLevelThreshold)
        {
            LoadBossLevel();
        }
        else
        {
            ShowUpgradeSelection(false);
            ShowPauseMenu(false);
            LoadRandomLevel();
        }
    }

    public static void ShowUpgradeSelection(bool enabled)
    {
        TimeController.PauseGame(enabled);
        _upgradeSelectionRoot.SetActive(enabled);
        _currentBlurController.EnableBlur(enabled);
    }

    public static void ShowPauseMenu(bool enabled)
    {
        _settingsRoot.SetActive(false);
        _pauseSceneRoot.SetActive(enabled);
        _currentBlurController.EnableBlur(enabled);
        _pauseMenuActive = enabled;
    }
    

    public static void ShowSettingsMenu(bool enabled)
    {
        if (_pauseMenuActive)
        {
            _pauseSceneRoot.SetActive(!enabled);
        }

        _settingsRoot.SetActive(enabled);
    }

    private static void LoadBossLevel()
    {
        levelCounter++;
        SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1);
        EventManager.OnLevelEnter.Trigger();
    }

    private static void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
        levelCounter = 0;
    }

    private static void LoadRandomLevel()
    {
        int nextSceneIdx;

        do
        {
            nextSceneIdx = Random.Range(1, TotalLevelCount + 1);
        } while (nextSceneIdx == lastSceneIdx || nextSceneIdx == secondLastSceneIdx);

        secondLastSceneIdx = lastSceneIdx;
        lastSceneIdx = nextSceneIdx;
        levelCounter++;

        var scenePath = $"Assets/Content/Scenes/Levels/Level{nextSceneIdx}.unity";
        var oldScene = _currentActiveScene;
        
        var asyncOperation = SceneManager.LoadSceneAsync(scenePath, LoadSceneMode.Additive);
        asyncOperation.completed += _ =>
        {
            TimeController.PauseGame(false);
            _currentActiveScene = SceneManager.GetSceneByPath(scenePath);
            _currentBlurController = Object.FindFirstObjectByType<BlurController>();
            SceneManager.SetActiveScene(_currentActiveScene);
            SceneManager.UnloadSceneAsync(oldScene);
            
            SpawnController.SpawnEnemies();
        };

        ProgressionManager.IncreaseDifficultyLevel();
        EventManager.OnLevelEnter.Trigger();
    }
}