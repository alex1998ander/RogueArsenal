using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

/// <summary>
/// Manages loading the different levels and the upgrade selection screen.
/// </summary>
public static class LevelManager
{
    public const int BossLevelThreshold = 14;
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

        SceneManager.LoadSceneAsync("Assets/Content/Scenes/NewUI/UIControlHint.unity", LoadSceneMode.Additive);
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
            _mainMenuUIRoot = Object.FindFirstObjectByType<MainMenuViewManager>().gameObject;
        };

        EventManager.OnMainMenuEnter.Trigger();
    }

    public static void StartRound()
    {
        UpgradeManager.ResetUpgrades();
        PlayerData.ResetData();
        LoadLobbyLevel();
        _pauseAllowed = true;
    }

    public static void Continue()
    {
        if (levelCounter > BossLevelThreshold)
        {
            LoadMainMenu();
            return;
        }

        if (ProgressionManager.UpgradeReady)
        {
            ShowUpgradeSelection(true);
        }
        else if (levelCounter == BossLevelThreshold)
        {
            ShowUpgradeSelection(false);
            ShowPauseMenu(false);
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
        _gameOverRoot.SetActive(true);
        _pauseAllowed = false;
    }

    private static void ShowUpgradeSelection(bool enabled)
    {
        GameManager.FreezeGamePlay(enabled);
        _upgradeSelectionRoot.SetActive(enabled);
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
    }

    public static void ShowSettingsMenu(bool enabled)
    {
        if (gameState == GameState.MainMenu)
        {
            _mainMenuUIRoot.SetActive(!enabled);
        }

        if (gameState is GameState.Ingame or GameState.Boss or GameState.Sandbox)
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

        var gameOverLoad = SceneManager.LoadSceneAsync("Assets/Content/Scenes/NewUI/UIGameOver.unity", LoadSceneMode.Additive);
        gameOverLoad.completed += _ =>
        {
            _gameOverRoot = SceneManager.GetSceneByPath("Assets/Content/Scenes/NewUI/UIGameOver.unity").GetRootGameObjects()[0];
            _gameOverRoot.SetActive(false);
        };

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

        SceneManager.LoadSceneAsync("Assets/Content/Scenes/NewUI/UITransition.unity", LoadSceneMode.Additive);

        gameState = GameState.Ingame;
        EventManager.OnLevelEnter.Trigger();
    }

    private static void LoadBossLevel()
    {
        SwitchLevelAsync("Assets/Content/Scenes/Levels/LevelBoss.unity", _currentActiveScene);
        gameState = GameState.Boss;
        EventManager.OnLevelEnter.Trigger();
        _pauseAllowed = true;
        levelCounter++;
    }

    public static void LoadMainMenu()
    {
        TimeController.ResetTimeScale();
        var loadAsyncOperation = SceneManager.LoadSceneAsync("Assets/Content/Scenes/NewUI/UIMainMenu.unity", LoadSceneMode.Additive);
        loadAsyncOperation.completed += _ =>
        {
            GameManager.PauseGame(false);
            GameManager.FreezeGamePlay(false);

            var unloadAsyncOperation = SceneManager.UnloadSceneAsync(_currentActiveScene);
            _currentActiveScene = SceneManager.GetSceneByPath("Assets/Content/Scenes/NewUI/UIMainMenu.unity");
            SceneManager.SetActiveScene(_currentActiveScene);

            unloadAsyncOperation.completed += _ =>
            {
                _mainMenuUIRoot = Object.FindFirstObjectByType<MainMenuViewManager>().gameObject;
            };
        };

        if (gameState == GameState.Sandbox)
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneByPath("Assets/Content/Scenes/NewUI/UIGameOver.unity"));
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneByPath("Assets/Content/Scenes/NewUI/UISandbox.unity"));
        }
        else
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneByPath("Assets/Content/Scenes/NewUI/UIGameOver.unity"));
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneByPath("Assets/Content/Scenes/NewUI/UIPause.unity"));
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneByPath("Assets/Content/Scenes/NewUI/UIUpgradeSelection.unity"));
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneByPath("Assets/Content/Scenes/NewUI/UITransition.unity"));
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

        LevelTransitionController.Singleton.TriggerLevelTransition(() =>
        {
            var asyncUnloadOperation = SceneManager.UnloadSceneAsync(_currentActiveScene);
            asyncUnloadOperation.completed += _ =>
            {
                var asyncLoadOperation = SceneManager.LoadSceneAsync($"Assets/Content/Scenes/Levels/Level{nextSceneIdx}.unity", LoadSceneMode.Additive);
                asyncLoadOperation.completed += _ =>
                {
                    GameManager.PauseGame(false);
                    GameManager.FreezeGamePlay(false);

                    _currentActiveScene = SceneManager.GetSceneByPath($"Assets/Content/Scenes/Levels/Level{nextSceneIdx}.unity");
                    SceneManager.SetActiveScene(_currentActiveScene);

                    // Spawn enemies AFTER old level is fully unloaded so enemy AI won't find player of old level
                    SpawnController.SpawnEnemies();
                };
            };

            _pauseAllowed = true;
            gameState = GameState.Ingame;

            ProgressionManager.IncreaseDifficultyLevel();
            EventManager.OnLevelEnter.Trigger();
        });
    }

    private static void SwitchLevelAsync(string scenePath, Scene oldScene)
    {
        var asyncOperation = SceneManager.LoadSceneAsync(scenePath, LoadSceneMode.Additive);
        asyncOperation.completed += _ =>
        {
            GameManager.PauseGame(false);
            GameManager.FreezeGamePlay(false);

            _currentActiveScene = SceneManager.GetSceneByPath(scenePath);
            var unloadAsyncOperation = SceneManager.UnloadSceneAsync(oldScene);
            SceneManager.SetActiveScene(_currentActiveScene);

            unloadAsyncOperation.completed += _ =>
            {
                // Spawn enemies AFTER old level is fully unloaded so enemy AI won't find player of old level
                SpawnController.SpawnEnemies();
            };
        };
    }
}