using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    private string _nextScene;
    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(_nextScene);
    }
}
