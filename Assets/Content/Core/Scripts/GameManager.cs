using UnityEngine;

/// <summary>
/// Manager for low level game states
/// </summary>
public static class GameManager
{
    public static bool GamePaused { get; private set; }

    public static void TogglePause()
    {
        if (GamePaused)
        {
            TimeController.ResumeGame();
        }
        else
        {
            TimeController.PauseGame();
        }

        GamePaused = !GamePaused;
        EventManager.OnPauseGame.Trigger(GamePaused);
    }
}