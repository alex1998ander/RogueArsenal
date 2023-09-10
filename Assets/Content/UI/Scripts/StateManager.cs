using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{
    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void ChangeSceneByName()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
        SceneChange.stageCount = 1;
    }
}
