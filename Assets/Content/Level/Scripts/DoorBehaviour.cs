using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour, ICharacterHealth
{
    [SerializeField] private float maxHealth = 1f;
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

        //EventManager.OnEnemyDamage.Trigger(damageAmount);

        // if enemy dies
        if (IsDead())
        {
            Destroy(gameObject);
        }
    }

    public bool IsDead()
    {
        return _currentHealth <= 0;
    }
}