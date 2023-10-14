using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages loading the different levels and the upgrade selection screen.
/// </summary>
public static class LevelManager
{
    public static int levelCounter;

    // Counter for amount of upgrades needed for scene change
    private static int _maxSceneCount = SceneManager.sceneCountInBuildSettings;

    private static int lastSceneIdx = -1;
    private static int secondLastSceneIdx = -2;

    /// <summary>
    /// Laedt unsere erste Scene
    /// </summary>
    public static void LoadUpgradeSelectionScene()
    {
        if (levelCounter > 18)
            LoadMainMenu();
        else
            SceneManager.LoadScene(1);
    }

    private static void LoadRandomLevel()
    {
        int nextSceneIdx = Random.Range(2, SceneManager.sceneCountInBuildSettings - 1);

        while (nextSceneIdx == lastSceneIdx || nextSceneIdx == secondLastSceneIdx)
        {
            nextSceneIdx = Random.Range(2, SceneManager.sceneCountInBuildSettings - 1);
        }

        secondLastSceneIdx = lastSceneIdx;
        lastSceneIdx = nextSceneIdx;
        SceneManager.LoadScene(nextSceneIdx);
        levelCounter++;

        EventManager.OnLevelEnter.Trigger();
    }

    public static void LoadNextLevel()
    {
        if (levelCounter == 18)
        {
            LoadBossLevel();
        }
        else
        {
            LoadRandomLevel();
        }
    }

    private static void LoadBossLevel()
    {
        levelCounter++;
        SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1);
        EventManager.OnLevelEnter.Trigger();
    }

    public static void ReloadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        EventManager.OnLevelEnter.Trigger();
    }

    public static void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
        levelCounter = 0;
    }
}