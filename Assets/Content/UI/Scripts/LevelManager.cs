using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    private void Awake()
    {
        if (LevelManager.Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
           Pause(); 
        }
    }

    public void GameOver()
    {
        UIManager ui = GetComponent<UIManager>();
        if (ui == null) return;
        ui.ToggleDeathPanel();
        Time.timeScale = 0;
    }
    
    public void Pause()
    {
        UIManager ui = GetComponent<UIManager>();
        if (ui == null) return;
        ui.TogglePausePanel();
        //Time.timeScale = 0;
        if (Time.timeScale == 0)
            Time.timeScale = (float)1;
        else
            Time.timeScale = (float)0;
    }
}
