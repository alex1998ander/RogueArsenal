using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingField : MonoBehaviour
{
    private LayerMask _targetLayer;

    private void Start()
    {
        _targetLayer = LayerMask.GetMask("Player_Trigger");
        StartCoroutine(HealCharacter());
        EventManager.OnHealingFieldStart.Trigger();
    }

    private IEnumerator HealCharacter()
    {
        int burstsLeft = Configuration.HealingField_Bursts;
        float timeBetweenBursts = Configuration.HealingField_Duration / Configuration.HealingField_Bursts;
        float healValuePerBurst = Configuration.HealingField_Amount / Configuration.HealingField_Bursts;

        while (burstsLeft > 0)
        {
            Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, Configuration.HealingField_Radius, _targetLayer);

            foreach (Collider2D character in rangeCheck)
            {
                character.GetComponentInParent<PlayerHealth>().Heal(healValuePerBurst);
                EventManager.OnPlayerHealthUpdate.Trigger(healValuePerBurst);
            }

            burstsLeft--;
            yield return new WaitForSeconds(timeBetweenBursts);
        }
    }
}