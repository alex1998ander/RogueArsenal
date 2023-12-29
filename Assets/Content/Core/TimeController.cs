using UnityEngine;

public static class TimeController
{
    private static float _timeScale = 1f;

    public static void PauseGame(bool enabled)
    {
        _timeScale = enabled ? 0f : 1f;
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
    }
}