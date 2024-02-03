using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelCounterView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelCounter;

    void Start()
    {
        if (LevelManager.levelCounter <= LevelManager.BossLevelThreshold)
            levelCounter.text = $"Level {LevelManager.levelCounter + 1} / {LevelManager.BossLevelThreshold + 1}";
        else
            levelCounter.text = "Boss";
    }
}