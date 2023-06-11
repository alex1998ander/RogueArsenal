using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class SceneChange : MonoBehaviour
{
    private string _nextScene;
    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(3);
        
    }

    public void ChangeToLevel()
    {
        int nextScene = Random.Range(0,2);
        SceneManager.LoadScene(nextScene);
    }
}
