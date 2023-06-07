using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    [SerializeField] private float maxHealth = 100f;
    private float _currentHealth;

    /// <summary>
    /// Resets the player's health. The currently active upgrades are taken into account.
    /// </summary>
    public void ResetHealth() {
        _currentHealth = maxHealth * UpgradeManager.GetHealthMultiplier();
    }

    /// <summary>
    /// Decreases the player's health by the specified value and checks if the player dies. If so, affecting upgrades are performed and further actions are initiated.
    /// </summary>
    /// <param name="damage">Damage data</param>
    public void InflictDamage(Damage damage) {
        _currentHealth -= damage.DamageAmount;

        EventManager.OnPlayerDamage.Trigger(damage);

        // if player dies
        if (_currentHealth <= 0) {
            // possible revival through upgrades
            UpgradeManager.OnPlayerDeath(damage.Player);

            // if player dies anyway
            if (_currentHealth <= 0) {
                EventManager.OnPlayerDeath.Trigger();
            }
        }
    }
}