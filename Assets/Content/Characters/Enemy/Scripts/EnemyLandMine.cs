using System.Collections;
using UnityEngine;

public class EnemyLandMine : MonoBehaviour
{
    [SerializeField] private SpriteRenderer mineSprite;
    [SerializeField] private AudioSource mineExplosionSound;

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
            _player = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _player = null;
        }
    }

    IEnumerator StartCountdown()
    {
        yield return new WaitForSeconds(Configuration.Boss_MineCountdown + _mineCountDownDifference);

        mineSprite.enabled = false;
        _animator.SetBool("Exploded", true);
        mineExplosionSound.Play();

        if (_player)
        {
            _player.GetComponentInParent<PlayerHealth>().InflictDamage(Configuration.Boss_MineDamage, true);
            EventManager.OnPlayerHealthUpdate.Trigger(-Configuration.Boss_MineDamage);
        }

        Destroy(gameObject, 1f);
    }
}