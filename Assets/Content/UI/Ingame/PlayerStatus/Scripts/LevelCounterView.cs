using TMPro;
using UnityEngine;

public class LevelCounterView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelCounterDisplay;
    [SerializeField] private TextMeshProUGUI bossDisplay;
    [SerializeField] private TextMeshProUGUI sandboxDisplay;
    [SerializeField] private TextMeshProUGUI maxLevelDisplay;

    void Start()
    {
        switch (LevelManager.GameState)
        {
            case GameState.Boss:
                levelCounterDisplay.gameObject.SetActive(false);
                maxLevelDisplay.gameObject.SetActive(false);
                sandboxDisplay.gameObject.SetActive(false);

                bossDisplay.gameObject.SetActive(true);
                break;

            case GameState.Ingame:
                bossDisplay.gameObject.SetActive(false);
                sandboxDisplay.gameObject.SetActive(false);

                levelCounterDisplay.gameObject.SetActive(true);
                maxLevelDisplay.gameObject.SetActive(true);

                levelCounterDisplay.text = (LevelManager.levelCounter + 1).ToString();
                break;

            case GameState.Sandbox:
                levelCounterDisplay.gameObject.SetActive(false);
                maxLevelDisplay.gameObject.SetActive(false);
                bossDisplay.gameObject.SetActive(false);

                sandboxDisplay.gameObject.SetActive(true);
                break;
        }
    }
}