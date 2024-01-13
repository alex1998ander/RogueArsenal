using UnityEngine;

public static class TimeController
{
    private static float _timeScale = 1f;
    
    /// <summary>
    /// Pauses the game time
    /// </summary>
    /// <remarks>
    /// If you want to pause the game, call GameManager.PauseGame() instead.
    /// </remarks>
    /// <param name="paused">Bool, whether the time is paused or not</param>
    public static void PauseTime(bool paused)
    {
        _timeScale = paused ? 0f : 1f;
        Time.timeScale = _timeScale;
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
        EventManager.OnPauseGame.Trigger(false);
    }

    public static float GetTimeScale()
    {
        return _timeScale;
    }
}