using System;
using System.Collections;
using UnityEngine;

public class PlayerVisualsController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer playerSprite;

    private void Start()
    {
        EventManager.OnPlayerHealthUpdate.Subscribe(DamageHealFlicker);
    }

    private void DamageHealFlicker(float healthChange)
    {
        if (healthChange < 0)
            SetSpriteColor(Color.red);
        else
            SetSpriteColor(Color.green);
    }

    private void SetSpriteColor(Color color)
    {
        playerSprite.color = color;
        StartCoroutine(ResetSpriteColor(0.1f));
    }

    private IEnumerator ResetSpriteColor(float delay)
    {
        yield return new WaitForSeconds(delay);
        playerSprite.color = Color.white;
        yield break;
    }

    private void OnDestroy()
    {
        EventManager.OnPlayerHealthUpdate.Unsubscribe(DamageHealFlicker);
    }
}