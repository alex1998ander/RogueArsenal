using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelCounterView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelCounter;
    
    void Start()
    {
        levelCounter.text = $"Level {LevelManager.levelCounter + 1} / 20";
    }
}
