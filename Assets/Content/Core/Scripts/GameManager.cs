using UnityEngine;

/// <summary>
/// Manager for low level game states
/// </summary>
public static class GameManager
{
    public static bool GamePaused { get; private set; }

    public static void TogglePause()
    {
        Time.timeScale = GamePaused ? 1f : 0f;
        GamePaused = !GamePaused;
        EventManager.OnPauseGame.Trigger(GamePaused);
    }
}