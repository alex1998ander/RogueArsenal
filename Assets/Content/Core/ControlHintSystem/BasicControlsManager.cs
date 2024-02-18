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
        EventManager.OnPlayerMovement.Subscribe(() => playerMoved = true);
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
            ControlHintSystem.ShowBasicControlPrompt();
            StartCoroutine(HideBasicControlPrompt());
        }
    }

    private IEnumerator HideBasicControlPrompt()
    {
        yield return new WaitForSeconds(ControlHintManager.FadeDuration);

        while (!playerMoved)
        {
            yield return null;
        }

        ControlHintSystem.HideBasicControlPrompt();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ControlHintSystem.TriggerDashPrompt();
        }
    }
}