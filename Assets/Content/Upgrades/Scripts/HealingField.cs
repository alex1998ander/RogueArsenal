using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingField : MonoBehaviour
{
    [SerializeField] private float healingPowerPer100MS = 5f;
    [SerializeField] private float radius = 1.5f;
    [SerializeField] private LayerMask targetLayer;

    private void Start()
    {
        StartCoroutine(HealCharacter());
        EventManager.OnHealingFieldStart.Trigger();
    }

    private IEnumerator HealCharacter()
    {
        while (true)
        {
            Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);

            foreach (Collider2D character in rangeCheck)
            {
                character.GetComponent<PlayerHealth>().Heal(healingPowerPer100MS);
                EventManager.OnPlayerHealthUpdate.Trigger(healingPowerPer100MS);
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
}