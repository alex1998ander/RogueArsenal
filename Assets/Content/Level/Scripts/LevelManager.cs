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
    private static GameObject _mainMenuUIRoot;
    private static GameObject _gameOverRoot;
    private static GameObject _pauseSceneRoot;
    private static GameObject _settingsRoot;
    private static GameObject _upgradeSelectionRoot;
    private static BlurController _currentBlurController;

    private static GameState gameState = GameState.MainMenu;

    private static bool _pauseAllowed;

    private static void Initialize()
    {
        EventManager.OnPauseGame.Subscribe(ShowPauseMenu);
        EventManager.OnPlayerDeath.Subscribe(ShowGameOver);

        _currentActiveScene = SceneManager.GetActiveScene();

        var settingsLoad = SceneManager.LoadSceneAsync("Assets/Content/Scenes/NewUI/UISettings.unity", LoadSceneMode.Additive);
        settingsLoad.completed += _ =>
        {
            _settingsRoot = SceneManager.GetSceneByPath("Assets/Content/Scenes/NewUI/UISettings.unity").GetRootGameObjects()[0];
            _settingsRoot.SetActive(false);
        };
    }

    public static void LoadGame()
    {
        Initialize();
        var asyncOperation = SceneManager.LoadSceneAsync("Assets/Content/Scenes/NewUI/UIMainMenu.unity", LoadSceneMode.Additive);
        asyncOperation.completed += _ =>
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneByPath("Assets/Content/Scenes/Others/SplashScene.unity"));
            _currentActiveScene = SceneManager.GetSceneByPath("Assets/Content/Scenes/NewUI/UIMainMenu.unity");
            SceneManager.SetActiveScene(_currentActiveScene);
            _currentBlurController = Object.FindFirstObjectByType<BlurController>();
            _mainMenuUIRoot = Object.FindFirstObjectByType<MainMenuViewManager>().gameObject;
        };

        EventManager.OnMainMenuEnter.Trigger();
    }

    public static void StartRound()
    {
        UpgradeManager.ResetUpgrades();
        LoadLobbyLevel();
        _pauseAllowed = true;
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
            gameState = GameState.Boss;
        }
        else
        {
            ShowUpgradeSelection(false);
            ShowPauseMenu(false);
            LoadRandomLevel();
        }
    }

    private static void ShowGameOver()
    {
        if (gameState == GameState.Sandbox)
        {
            return;
        }

        _gameOverRoot.SetActive(true);
        _currentBlurController.EnableBlur(true);
        _pauseAllowed = false;
    }

    private static void ShowUpgradeSelection(bool enabled)
    {
        TimeController.PauseGame(enabled);
        _upgradeSelectionRoot.SetActive(enabled);
        _currentBlurController.EnableBlur(enabled);
        _pauseAllowed = false;
    }

    private static void ShowPauseMenu(bool enabled)
    {
        if (!_pauseAllowed)
        {
            return;
        }

        _settingsRoot.SetActive(false);
        _pauseSceneRoot.SetActive(enabled);
        _currentBlurController.EnableBlur(enabled);
    }

    public static void ShowSettingsMenu(bool enabled)
    {
        if (gameState == GameState.MainMenu)
        {
            _currentBlurController.EnableBlur(enabled);
            _mainMenuUIRoot.SetActive(!enabled);
        }

        if (GameManager.GamePaused)
        {
            _pauseSceneRoot.SetActive(!enabled);
        }

        _settingsRoot.SetActive(enabled);
    }

    public static void LoadSandboxLevel()
    {
        UpgradeManager.ResetUpgrades();
        _pauseAllowed = true;
        SwitchLevelAsync("Assets/Content/Scenes/Others/SandboxScene.unity", _currentActiveScene);
        EventManager.OnLevelEnter.Trigger();

        var sandboxUILoad = SceneManager.LoadSceneAsync("Assets/Content/Scenes/NewUI/UISandbox.unity", LoadSceneMode.Additive);
        sandboxUILoad.completed += _ =>
        {
            _pauseSceneRoot = SceneManager.GetSceneByPath("Assets/Content/Scenes/NewUI/UISandbox.unity").GetRootGameObjects()[0];
            _pauseSceneRoot.SetActive(false);
        };

        gameState = GameState.Sandbox;
    }

    private static void LoadLobbyLevel()
    {
        SwitchLevelAsync("Assets/Content/Scenes/Levels/LevelLobby.unity", _currentActiveScene);
        EventManager.OnLevelEnter.Trigger();
        _pauseAllowed = true;

        var gameOverLoad = SceneManager.LoadSceneAsync("Assets/Content/Scenes/NewUI/UIGameOver.unity", LoadSceneMode.Additive);
        gameOverLoad.completed += _ =>
        {
            _gameOverRoot = SceneManager.GetSceneByPath("Assets/Content/Scenes/NewUI/UIGameOver.unity").GetRootGameObjects()[0];
            _gameOverRoot.SetActive(false);
        };

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

        gameState = GameState.Ingame;
    }

    private static void LoadBossLevel()
    {
        SwitchLevelAsync("Assets/Content/Scenes/Levels/LevelBoss.unity", _currentActiveScene);
        gameState = GameState.Boss;
        EventManager.OnLevelEnter.Trigger();
        _pauseAllowed = true;
    }

    public static void LoadMainMenu()
    {
        GameManager.Resume();
        var asyncOperation = SceneManager.LoadSceneAsync("Assets/Content/Scenes/NewUI/UIMainMenu.unity", LoadSceneMode.Additive);
        asyncOperation.completed += _ =>
        {
            TimeController.PauseGame(false);
            SceneManager.UnloadSceneAsync(_currentActiveScene);
            _currentActiveScene = SceneManager.GetSceneByPath("Assets/Content/Scenes/NewUI/UIMainMenu.unity");
            SceneManager.SetActiveScene(_currentActiveScene);
            _currentBlurController = Object.FindFirstObjectByType<BlurController>();
            _mainMenuUIRoot = Object.FindFirstObjectByType<MainMenuViewManager>().gameObject;
        };

        if (gameState == GameState.Sandbox)
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneByPath("Assets/Content/Scenes/NewUI/UISandbox.unity"));
        }
        else
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneByPath("Assets/Content/Scenes/NewUI/UIGameOver.unity"));
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneByPath("Assets/Content/Scenes/NewUI/UIPause.unity"));
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneByPath("Assets/Content/Scenes/NewUI/UIUpgradeSelection.unity"));
        }

        gameState = GameState.MainMenu;
        _pauseAllowed = false;
        levelCounter = 0;

        EventManager.OnMainMenuEnter.Trigger();
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

        SwitchLevelAsync($"Assets/Content/Scenes/Levels/Level{nextSceneIdx}.unity", _currentActiveScene);

        _pauseAllowed = true;
        gameState = GameState.Ingame;

        ProgressionManager.IncreaseDifficultyLevel();
        EventManager.OnLevelEnter.Trigger();
    }

    private static void SwitchLevelAsync(string scenePath, Scene oldScene)
    {
        var asyncOperation = SceneManager.LoadSceneAsync(scenePath, LoadSceneMode.Additive);
        asyncOperation.completed += _ =>
        {
            TimeController.PauseGame(false);
            _currentActiveScene = SceneManager.GetSceneByPath(scenePath);
            SceneManager.UnloadSceneAsync(oldScene);
            SceneManager.SetActiveScene(_currentActiveScene);
            _currentBlurController = Object.FindFirstObjectByType<BlurController>();

            SpawnController.SpawnEnemies();
        };
    }
}