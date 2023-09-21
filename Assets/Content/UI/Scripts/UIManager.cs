using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject deathPanel;
    [SerializeField] private GameObject pausePanel;

    private void Awake()
    {
        EventManager.OnPlayerDeath.Subscribe(ShowDeathPanel);
    }

    private void OnDestroy()
    {
        EventManager.OnPlayerDeath.Unsubscribe(ShowDeathPanel);
    }

    public void ShowDeathPanel()
    {
        deathPanel.SetActive(true);
    }

    public void TogglePausePanel()
    {
        pausePanel.SetActive(!pausePanel.activeSelf);
        GameManager.TogglePause();
    }

    /// <summary>
    /// 
    /// </summary>
    public static void RestartLevel()
    {
        LevelManager.ReloadCurrentLevel();
        EventManager.OnLevelEnter.Trigger();
    }

    public static void LoadMainMenu()
    {
        LevelManager.LoadMainMenu();
    }

    private void OnPause()
    {
        TogglePausePanel();
    }
}