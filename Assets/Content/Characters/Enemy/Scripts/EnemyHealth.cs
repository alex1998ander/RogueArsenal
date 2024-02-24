using BehaviorTree;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, ICharacterHealth
{
    [SerializeField] private float maxHealth = 100f;

    private EnemyBehaviourTree _bt;
    
    private ObjectDropController _currencyDropController;
    private float _currentHealth;

    private void Awake()
    {
        _bt = GetComponent<EnemyBehaviourTree>();
        
        _currencyDropController = GetComponent<ObjectDropController>();
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
        
        _bt?.NoticePlayer();

        // if enemy dies
        if (IsDead())
        {
            if (_currencyDropController)
                _currencyDropController.DropObjects();

            EventManager.OnEnemyDeath.Trigger(transform.position);
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