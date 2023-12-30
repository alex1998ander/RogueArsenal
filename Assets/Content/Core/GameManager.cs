/// <summary>
/// Manager for low level game states
/// </summary>
public static class GameManager
{
    public static bool GamePaused { get; private set; }

    public static void TogglePause()
    {
        GamePaused = !GamePaused;
        TimeController.PauseGame(GamePaused);
        EventManager.OnPauseGame.Trigger(GamePaused);
    }

    public static void Resume()
    {
        if (GamePaused)
        {
            GamePaused = false;
            TimeController.PauseGame(GamePaused);
            EventManager.OnPauseGame.Trigger(GamePaused);
        }
    }
}