using UnityEngine;

public class EnemyContactDamage : MonoBehaviour
{
    [SerializeField] private float contactDamage = 10f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Enemy deals contact damage to Player
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponentInParent<PlayerHealth>().InflictContactDamage(contactDamage);
            EventManager.OnPlayerHit.Trigger();
        }
    }
}