using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject deathPanel;
    [SerializeField] private GameObject pausePanel;

    public void ToggleDeathPanel()
    {
        deathPanel.SetActive(!deathPanel.activeSelf);
        // UIManager ui = instance.GetComponent<UIManager>();
        // if (ui == null) return;
        // ui.ToggleDeathPanel();
        // Time.timeScale = 0;
    }
    
    public void TogglePausePanel()
    {
        pausePanel.SetActive(!pausePanel.activeSelf);
        // UIManager ui = instance.GetComponent<UIManager>();
        // if (ui == null) return;
        // ui.TogglePausePanel();
        // //Time.timeScale = 0;
        // if (Time.timeScale == 0)
        //     Time.timeScale = 1f;
        // else
        //     Time.timeScale = 0f;
    }
}
