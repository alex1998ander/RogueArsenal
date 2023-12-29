/// <summary>
/// Manager for low level game states
/// </summary>
public static class GameManager
{
    public static bool GamePaused { get; private set; }

    public static void Pause()
    {
        GamePaused = !GamePaused;
        TimeController.PauseGame(GamePaused);
        EventManager.OnPauseGame.Trigger(GamePaused);
    }
}