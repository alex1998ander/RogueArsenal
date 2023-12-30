using System;
using UnityEngine;

public class GameOverViewManager : MonoBehaviour
{
    [SerializeField] private StringButtonView continueButton;

    private void Start()
    {
        continueButton.Initialize(LevelManager.LoadMainMenu);
    }
}
