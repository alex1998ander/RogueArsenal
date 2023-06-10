using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
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
    public void InflictDamage(float damageAmount)
    {
        _currentHealth -= damageAmount;

        EventManager.OnEnemyDamage.Trigger(damageAmount);

        // if enemy dies
        if (_currentHealth <= 0)
        {
            EventManager.OnEnemyDeath.Trigger(gameObject.GetComponent<EnemyController>());
            Destroy(gameObject);
        }
    }
}