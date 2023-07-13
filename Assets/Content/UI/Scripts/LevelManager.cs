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

    public void GameOver()
    {
        UIManager ui = GetComponent<UIManager>();
        if (ui == null) return;
        ui.ToggleDeathPanel();
        Time.timeScale = 0;
    }
}
