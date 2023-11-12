using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SandboxSceneInputManager : MonoBehaviour
{
    [SerializeField] private SpawnController spawnController;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            ReloadSandboxScene();
        for (int i = 0; i <= 9; i++)
        {
            if (Input.GetKeyDown((KeyCode) Enum.Parse(typeof(KeyCode), "Alpha" + i)))
            {
                spawnController.SpawnEnemies(i != 0 ? i * 0.1f : 1.0f);
            }
        }
    }

    private void ReloadSandboxScene()
    {
        UpgradeManager.PrepareUpgrades();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}