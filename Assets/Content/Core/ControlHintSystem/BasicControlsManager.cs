using System;
using System.Collections;
using Content.Core.ControlHintSystem;
using UnityEngine;

public class BasicControlsManager : MonoBehaviour
{
    private const int NoInputDuration = 5;

    private bool playerMoved;

    void Awake()
    {
        EventManager.OnPlayerMovement.Subscribe(OnPlayerMoved);
    }

    private void OnDestroy()
    {
        EventManager.OnPlayerMovement.Unsubscribe(OnPlayerMoved);
    }

    private void OnPlayerMoved()
    {
        playerMoved = true;
        ControlHintSystem.TriggerShootPrompt();
    }

    private void Start()
    {
        StartCoroutine(CheckPlayerInputs());
    }

    private IEnumerator CheckPlayerInputs()
    {
        yield return new WaitForSeconds(NoInputDuration);

        if (!playerMoved)
        {
            ControlHintSystem.ShowMovementControlPrompt();
            StartCoroutine(HideMovementControlPrompt());
        }
    }

    private IEnumerator HideMovementControlPrompt()
    {
        yield return new WaitForSeconds(ControlHintManager.FadeDuration);

        while (!playerMoved)
        {
            yield return null;
        }

        ControlHintSystem.HideMovementControlPrompt();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ControlHintSystem.TriggerDashPrompt();
        }
    }
}