using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class CounterDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI enemyCounterText;
    [SerializeField] private TextMeshProUGUI levelCounterText;
    
    void Update()
    {
        enemyCounterText.text = "Enemy counter: " + GameObject.FindGameObjectsWithTag("Enemy").Length; //TODO: Bessere Methode?
        levelCounterText.text = "Level: " + LevelManager.levelCounter;
    }
}
