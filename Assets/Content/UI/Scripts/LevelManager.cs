using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
           Pause(); 
        }
    }

    public static void GameOver()
    {
        UIManager ui = instance.GetComponent<UIManager>();
        if (ui == null) return;
        ui.ToggleDeathPanel();
        Time.timeScale = 0;
    }
    
    public static void Pause()
    {
        UIManager ui = instance.GetComponent<UIManager>();
        if (ui == null) return;
        ui.TogglePausePanel();
        //Time.timeScale = 0;
        if (Time.timeScale == 0)
            Time.timeScale = (float)1;
        else
            Time.timeScale = (float)0;
    }
}
