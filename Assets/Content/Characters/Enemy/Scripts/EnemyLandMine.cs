using System.Collections;
using UnityEngine;

public class EnemyLandMine : MonoBehaviour
{
    [SerializeField] private SpriteRenderer mineSprite;

    private bool _playerIsInRange = false;
    private GameObject _player;

    private Animator _animator;

    private float _mineCountDownDifference;

    void Start()
    {
        _mineCountDownDifference = Random.Range(-1f, 1f);
        _animator = GetComponentInChildren<Animator>();
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
        yield return new WaitForSeconds(Configuration.Boss_MineCountdown + _mineCountDownDifference);

        mineSprite.enabled = false;
        _animator.SetBool("Exploded", true);
        // TODO: Play sound when exploding

        if (_playerIsInRange)
        {
            _player.GetComponentInParent<PlayerHealth>().InflictDamage(Configuration.Boss_MineDamage, true);
            EventManager.OnPlayerHealthUpdate.Trigger(-Configuration.Boss_MineDamage);
        }

        Destroy(gameObject, 1f);
    }
}