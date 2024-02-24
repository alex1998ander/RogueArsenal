using TMPro;
using UnityEngine;

public class LevelCounterView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelCounterDisplay;
    [SerializeField] private TextMeshProUGUI bossDisplay;
    [SerializeField] private TextMeshProUGUI maxLevelDisplay;

    void Start()
    {
        if (LevelManager.levelCounter <= LevelManager.BossLevelThreshold)
        {
            levelCounterDisplay.gameObject.SetActive(true);
            maxLevelDisplay.gameObject.SetActive(true);
            bossDisplay.gameObject.SetActive(false);
            
            levelCounterDisplay.text = (LevelManager.levelCounter + 1).ToString();
        }
        else
        {
            levelCounterDisplay.gameObject.SetActive(false);
            maxLevelDisplay.gameObject.SetActive(false);
            bossDisplay.gameObject.SetActive(true);
        }
    }
}