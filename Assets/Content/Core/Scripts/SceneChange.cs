using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class SceneChange : MonoBehaviour
{
    private int _choosenUpgradeCount = 0;
    
    /// <summary>
    /// Loading card choosing scene.
    /// </summary>
    /// <param name="other">Object that hit the trigger box</param>
    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(3);
        
    }

    /// <summary>
    /// This function is called when a button is pressed.
    /// Loads one of the levels randomly.
    /// (changes scene after 3 upgrades have been chosen)
    /// </summary>
    public void ChangeToLevel()
    {
        if (_choosenUpgradeCount == 2)
        {
            int nextScene = Random.Range(0, 2);
            SceneManager.LoadScene(nextScene);
        }
        else _choosenUpgradeCount++;

    }
}
