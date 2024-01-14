// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
//
// public class UIManager : MonoBehaviour
// {
//     [SerializeField] private GameObject deathPanel;
//     [SerializeField] private GameObject pausePanel;
//
//     private void Awake()
//     {
//         EventManager.OnPlayerDeath.Subscribe(ShowDeathPanel);
//     }
//
//     private void OnDestroy()
//     {
//         EventManager.OnPlayerDeath.Unsubscribe(ShowDeathPanel);
//     }
//
//     public void ShowDeathPanel()
//     {
//         deathPanel.SetActive(true);
//     }
//
//     public void TogglePausePanel()
//     {
//         pausePanel.SetActive(!pausePanel.activeSelf);
//         GameManager.TogglePause();
//     }
//
//     public static void RestartLevelFromDeathScreen()
//     {
//         LevelManager.ReloadCurrentLevel();
//     }
//
//     public static void LoadMainMenuFromDeathScreen()
//     {
//         LevelManager.LoadMainMenu();
//         //EventManager.OnMainMenuEnter.Trigger();
//     }
//     
//     public static void LoadMainMenuFromPauseMenu()
//     {
//         LevelManager.LoadMainMenu();
//         GameManager.TogglePause();
//         //EventManager.OnMainMenuEnter.Trigger();
//     }
//
//     private void OnPause()
//     {
//         TogglePausePanel();
//     }
// }