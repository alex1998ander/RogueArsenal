using UnityEngine;
public class MainMenu : MonoBehaviour
{
    
    void Start()
    {
        UpgradeManager.ResetUpgrades();
    }
    
    public void QuitGame()
    {
        Debug.Log("Quitting");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}