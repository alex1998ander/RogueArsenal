using UnityEngine;
using UnityEngine.SceneManagement;

public class SandboxSceneReloaderController : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            UpgradeManager.PrepareUpgrades();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}