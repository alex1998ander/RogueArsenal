using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class DPSMeasurer : MonoBehaviour, ICharacterHealth
{
    [SerializeField] private TMP_Text dpsDisplay;

    private float _damageCounter;

    private void Start()
    {
        StartCoroutine(SetDpsText());
    }

    public void InflictDamage(float damageAmount, bool fatal = false, bool ignoreInvulnerability = false)
    {
        _damageCounter += damageAmount;
    }

    private IEnumerator SetDpsText()
    {
        while (true)
        {
            dpsDisplay.text = "DPS: " + _damageCounter;
            _damageCounter = 0f;
            yield return new WaitForSeconds(1f);
        }
    }
}