using System.Collections;
using UnityEngine;

public class EnemyLandMine : MonoBehaviour
{
    private bool _playerIsInRange = false;
    private GameObject _player;
    void Start()
    {
        StartCoroutine(startCountdown());
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

    IEnumerator startCountdown()
    {
        yield return new WaitForSeconds(2);
        if (_playerIsInRange)
        {
            _player.GetComponent<PlayerHealth>().InflictDamage(30, true);
            EventManager.OnPlayerHealthUpdate.Trigger(30);
        }
        Destroy(gameObject);
    }
}
