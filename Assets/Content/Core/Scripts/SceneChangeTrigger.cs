using UnityEngine;

public class SceneChangeTrigger : MonoBehaviour
{
    /// <summary>
    /// Loading card choosing scene.
    /// </summary>
    /// <param name="other">Object that hit the trigger box</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !SpawnController.CheckEnemiesAlive())
        {
            LevelManager.LoadNextLevel();
            EventManager.OnLevelExit.Trigger();
        }
    }
}