using UnityEngine;

public class EnemyHealth : MonoBehaviour, ICharacterHealth
{
    [SerializeField] private float maxHealth = 100f;
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
    public void InflictDamage(float damageAmount, bool fatal = false)
    {
        _currentHealth -= damageAmount;

        EventManager.OnEnemyDamage.Trigger(damageAmount);

        // if enemy dies
        if (_currentHealth <= 0)
        {
            EventManager.OnEnemyDeath.Trigger(gameObject);
            Destroy(gameObject.transform.root.gameObject);
        }
    }

    public Vector2 GetHealth()
    {
        return new Vector2(_currentHealth, maxHealth);
    }
}