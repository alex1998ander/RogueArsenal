using UnityEngine;

public class CurrencyCollect : MonoBehaviour
{
    private const float DropForce = 15f;
    private Rigidbody2D _rb;
    private Vector2 _playerPos;
    private bool _playerFound;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Vector2 randomDir = Random.insideUnitCircle * DropForce;
        _rb.velocity = randomDir;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            EventManager.OnPlayerCollectCurrency.Trigger();
            Destroy(gameObject);
        }
    }
}