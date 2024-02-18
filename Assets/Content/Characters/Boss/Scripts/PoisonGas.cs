using System;
using System.Collections;
using UnityEngine;

namespace Content.Characters.Boss.Scripts
{
    public class PoisonGas: MonoBehaviour
    {
        private IEnumerator _poisonGas;
        private void OnTriggerEnter2D(Collider2D other)
        {
            _poisonGas = DamagePlayer();
            StartCoroutine(_poisonGas);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            StopCoroutine(_poisonGas);
        }

        IEnumerator DamagePlayer()
        {
            yield return new WaitForSeconds(1);
            DamagePlayer();
        }
    }
}