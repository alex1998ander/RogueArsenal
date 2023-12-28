using System;
using System.Collections;
using UnityEngine;

public class PlayerVisualsController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer playerSprite;

    private void Start()
    {
        EventManager.OnPlayerHealthUpdate.Subscribe(DamageHealFlicker);
        EventManager.OnPhoenixRevive.Subscribe(OnPhoenixed);
    }

    private void DamageHealFlicker(float healthChange)
    {
        if (healthChange < 0)
            StartCoroutine(SetSpriteColorForTime(Color.red, 0.1f));
        else
            StartCoroutine(SetSpriteColorForTime(Color.green, 0.1f));
    }

    private IEnumerator SetSpriteColorForTime(Color color, float delay)
    {
        playerSprite.color = color;
        yield return new WaitForSeconds(delay);
        playerSprite.color = Color.white;
    }

    private void OnPhoenixed()
    {
        StartCoroutine(PlayPhoenixedAnimation());
    }

    // TODO: Replace with actual animation later on
    private IEnumerator PlayPhoenixedAnimation()
    {
        playerSprite.enabled = false;
        yield return new WaitForSeconds(Configuration.Phoenix_WarmUpTime);

        playerSprite.enabled = true;
        playerSprite.color = Color.yellow;
        yield return new WaitForSeconds(Configuration.Phoenix_InvincibilityTime);

        playerSprite.color = Color.white;
    }

    private void OnDestroy()
    {
        EventManager.OnPlayerHealthUpdate.Unsubscribe(DamageHealFlicker);
        EventManager.OnPhoenixRevive.Unsubscribe(OnPhoenixed);
    }
}