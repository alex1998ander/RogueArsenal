using UnityEngine;

public static class TimeController
{
    private static float _timeScale = 1f;

    public static void PauseGame()
    {
        _timeScale = 0f;
        Time.timeScale = _timeScale;
    }

    public static void ResumeGame()
    {
        _timeScale = 1f;
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

    public static float GetTimeScale()
    {
        return _timeScale;
    }
}