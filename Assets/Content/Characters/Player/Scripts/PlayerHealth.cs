using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    private float _currentHealth;

    /// <summary>
    /// Resets the player's health. The currently active upgrades are taken into account.
    /// </summary>
    public void ResetHealth()
    {
        _currentHealth = maxHealth * UpgradeManager.GetHealthMultiplier();
    }

    /// <summary>
    /// Decreases the player's health by the specified value and checks if the player dies. If so, affecting upgrades are performed and further actions are initiated.
    /// </summary>
    /// <param name="damageAmount">Amount of damage</param>
    /// <param name="characterController">Character that deals the damage (could be player itself)</param>
    public void InflictDamage(float damageAmount, ICharacterController characterController)
    {
        _currentHealth -= damageAmount;

        EventManager.OnPlayerDamage.Trigger(damageAmount, characterController);

        // if player dies
        if (_currentHealth <= 0)
        {
            // possible revival through upgrades
            UpgradeManager.OnPlayerDeath(gameObject.GetComponent<PlayerController>());

            // if player dies anyway
            if (_currentHealth <= 0)
            {
                EventManager.OnPlayerDeath.Trigger();
                Destroy(gameObject);
            }
        }
    }
}