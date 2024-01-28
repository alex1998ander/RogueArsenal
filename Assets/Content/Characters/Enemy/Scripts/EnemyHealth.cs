using UnityEngine;

public class EnemyHealth : MonoBehaviour, ICharacterHealth
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private int currencyDropAmount;
    private float _currentHealth;

    private void Awake()
    {
        _currentHealth = maxHealth;
    }

    /// <summary>
    /// Decreases the enemy's health by the specified value, checks if the enemy dies and triggers corresponding events. 
    /// </summary>
    /// <param name="damageAmount">Amount of damage</param>
    /// <param name="fatal">ignored</param>
    public void InflictDamage(float damageAmount, bool fatal = false, bool ignoreInvulnerability = false)
    {
        if (IsDead())
            return;

        _currentHealth -= damageAmount;

        EventManager.OnEnemyDamage.Trigger(damageAmount);

        // if enemy dies
        if (IsDead())
        {
            EventManager.OnEnemyDeath.Trigger(transform.position);
            EventManager.OnEnemyCurrencyDropped.Trigger(transform.position, currencyDropAmount);
            Destroy(gameObject.transform.root.gameObject);
        }
    }

    public bool IsDead()
    {
        return _currentHealth <= 0;
    }

    public Vector2 GetHealth()
    {
        return new Vector2(_currentHealth, maxHealth);
    }
}