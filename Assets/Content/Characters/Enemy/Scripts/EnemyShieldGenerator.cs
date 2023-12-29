using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyShieldGenerator : MonoBehaviour, ICharacterHealth
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private Collider2D bossCollider2D;
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

        // if enemy dies
        if (IsDead())
        {
            gameObject.SetActive(false);
            bossCollider2D.enabled = true;
        }
    }

    public bool IsDead()
    {
        return _currentHealth <= 0;
    }

    void Update()
    {
        transform.RotateAround(transform.parent.transform.position - new Vector3(0,0.7f,0), 
            new Vector3(0, 0, 1), 50 * Time.deltaTime);
    }
}