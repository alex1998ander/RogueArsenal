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
        SceneManager.LoadScene(1);
    }

    public static void LoadRandomLevel()
    {
        int nextSceneIdx = Random.Range(2, SceneManager.sceneCountInBuildSettings);

        while (nextSceneIdx == lastSceneIdx || nextSceneIdx == secondLastSceneIdx)
        {
            nextSceneIdx = Random.Range(2, SceneManager.sceneCountInBuildSettings);
        }

        secondLastSceneIdx = lastSceneIdx;
        lastSceneIdx = nextSceneIdx;
        SceneManager.LoadScene(nextSceneIdx);
        levelCounter++;
    }

    public static void ReloadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}