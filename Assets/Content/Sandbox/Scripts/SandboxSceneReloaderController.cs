using UnityEngine;
using UnityEngine.SceneManagement;

public class SandboxSceneReloaderController : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            UpgradeManager.ResetUpgrades();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}