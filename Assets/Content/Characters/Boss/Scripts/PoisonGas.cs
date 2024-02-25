using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Content.Characters.Boss.Scripts
{
    public class PoisonGas: MonoBehaviour
    {
        private IEnumerator _poisonGas;
        private GameObject _player;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _poisonGas = DamagePlayer();
                _player = other.gameObject;
                StartCoroutine(_poisonGas);
            } 
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                StopCoroutine(_poisonGas);
            }
        }

        IEnumerator DamagePlayer()
        {
            yield return new WaitForSeconds(1);
            _player.GetComponentInParent<PlayerHealth>().InflictDamage(5, true);
            EventManager.OnPlayerHealthUpdate.Trigger(-5);
            _poisonGas = DamagePlayer();
            StartCoroutine(_poisonGas);
        }
    }
}