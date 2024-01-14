/// <summary>
/// Manager for low level game states
/// </summary>
public static class GameManager
{
    private static bool _gamePaused;
    
    public static bool GamePaused => _gamePaused || GamePlayFrozen;

    public static bool GamePlayFrozen { get; private set; }
    
    
    public static void PauseGame(bool paused)
    {
        if (_gamePaused == paused)
        {
            return;
        }
        
        TimeController.PauseTime(paused);
        _gamePaused = paused;
        EventManager.OnPauseGame.Trigger(paused);
    }
    
    public static void TogglePause()
    {
        PauseGame(!_gamePaused);
    }    

    public static void FreezeGamePlay(bool frozen)
    {
        if (GamePlayFrozen == frozen)
        {
            return;
        }
        
        TimeController.PauseTime(frozen);
        GamePlayFrozen = frozen;
        EventManager.OnFreezeGamePlay.Trigger(frozen);
    }
}