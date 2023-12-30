using UnityEngine;

public static class TimeController
{
    public static bool GamePaused { get; private set; }
    
    private static bool _locked;
    private static float _timeScale = 1f;

    public static void TogglePause()
    {
        PauseGame(!GamePaused);
    }    
    
    public static void PauseGame(bool enabled)
    {
        if (GamePaused == enabled)
        {
            return;
        }
        
        if (_locked)
        {
            return;
        }
        
        GamePaused = enabled;
        _timeScale = enabled ? 0f : 1f;
        Time.timeScale = _timeScale;
        EventManager.OnPauseGame.Trigger(GamePaused);
    }

    public static void ForcePauseGame(bool enabled)
    {
        if (GamePaused == enabled)
        {
            return;
        }
        
        _locked = enabled;
        GamePaused = enabled;
        _timeScale = enabled ? 0f : 1f;
        Time.timeScale = _timeScale;
        EventManager.OnPauseGame.Trigger(GamePaused);
    }

    public static void ChangeTimeScale(float timeScale)
    {
        _timeScale = timeScale;
        Time.timeScale = timeScale;
    }

    public static void ResetTimeScale()
    {
        _timeScale = 1f;
        Time.timeScale = _timeScale;
        GamePaused = false;
        EventManager.OnPauseGame.Trigger(false);
    }

    public static float GetTimeScale()
    {
        return _timeScale;
    }
}