using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreenMenu : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    public void RestartLevel()
    {
        LevelManager.ReloadCurrentLevel();
        EventManager.OnLevelEnter.Trigger();
    }

    public void LoadMainMenu()
    {
        LevelManager.LoadMainMenu();
    }
}
