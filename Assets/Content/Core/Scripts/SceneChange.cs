using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class SceneChange : MonoBehaviour
{
    //Counter for amount of upgrades needed for scene change
    private int _maxSceneCount = 0;

    private static int lastScene = -1;
    private static int preLastScene = -2;

    private void Start()
    {
        _maxSceneCount = SceneManager.sceneCountInBuildSettings - 1;
    }

    /// <summary>
    /// Loading card choosing scene.
    /// </summary>
    /// <param name="other">Object that hit the trigger box</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hello");
        if (other.CompareTag("Player") && !SpawnController.StillEnemiesLeft())
        {
            SceneManager.LoadScene(1); 
        }
    }

    /// <summary>
    /// This function is called when a button is pressed.
    /// Loads one of the levels randomly.
    /// (changes scene after 3 upgrades have been chosen)
    /// </summary>
    public void ChangeToLevel()
    {
        //if (!SpawnController.StillEnemiesLeft())
        //{
            int nextScene = Random.Range(2, _maxSceneCount);
            Debug.Log("Found Scene: " + nextScene);
            if (nextScene == lastScene)
            {
                if (preLastScene > lastScene)
                {
                    nextScene--;
                }
                else
                {
                    nextScene++;
                }
            }

            nextScene %= (_maxSceneCount + 1);
            if (nextScene == 0)
            {
                nextScene++;
            }
            preLastScene = lastScene;
            lastScene = nextScene;
            Debug.Log("Load Scene: " + nextScene);
            SceneManager.LoadScene(nextScene);
            //}
        //else _chosenUpgradeCount++;

    }
}
