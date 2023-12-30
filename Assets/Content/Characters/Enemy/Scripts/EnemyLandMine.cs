using System.Collections;
using UnityEngine;

public class EnemyLandMine : MonoBehaviour
{
    private bool _playerIsInRange = false;
    private GameObject _player;

    void Start()
    {
        StartCoroutine(StartCountdown());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerIsInRange = true;
            _player = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerIsInRange = false;
        }
    }

    IEnumerator StartCountdown()
    {
        yield return new WaitForSeconds(Configuration.Boss_MineCountdown);
        if (_playerIsInRange)
        {
            _player.GetComponentInParent<PlayerHealth>().InflictDamage(Configuration.Boss_MineDamage, true);
            EventManager.OnPlayerHealthUpdate.Trigger(-Configuration.Boss_MineDamage);
        }

        Destroy(gameObject);
    }
}