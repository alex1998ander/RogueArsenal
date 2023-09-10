using UnityEngine;


public class PlayerHealth : MonoBehaviour, ICharacterHealth
{
    [SerializeField] private float maxHealth = 100f;
    private float _currentHealth;

    /// <summary>
    /// Resets the player's health. The currently active upgrades are taken into account.
    /// </summary>
    public void ResetHealth()
    {
        _currentHealth = (maxHealth + UpgradeManager.MaxHealthIncrease.Value) * UpgradeManager.GetHealthMultiplier();
    }

    /// <summary>
    /// Decreases the player's health by the specified value and checks if the player dies. If so, affecting upgrades are performed and further actions are initiated.
    /// </summary>
    /// <param name="damageAmount">Amount of damage</param>
    /// <param name="fatal">Indicates whether the player can die from this damage. If the damage is greater than the current HP and the damage is not fatal, the player keeps 1 HP.</param>
    public void InflictDamage(float damageAmount, bool fatal)
    {
        _currentHealth -= damageAmount;

        EventManager.OnPlayerDamage.Trigger(damageAmount);

        // if player dies
        if (_currentHealth <= 0)
        {
            // if player can die
            if (fatal)
            {
                // possible revival through upgrades
                UpgradeManager.OnPlayerDeath(gameObject.GetComponent<PlayerController>());

                // if player dies anyway
                if (_currentHealth <= 0)
                {
                    EventManager.OnPlayerDeath.Trigger();
                    //Destroy(gameObject);
                    PlayerDied();
                }
            }
            else
            {
                _currentHealth = 1;
            }
        }
    }

    void PlayerDied()
    {
        LevelManager.Instance.GameOver();
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Increases the player's health by the specified value and checks if the health is full.
    /// </summary>
    /// <param name="healingAmount">Amount of healing</param>
    public void Heal(float healingAmount)
    {
        _currentHealth = Mathf.Min(_currentHealth + healingAmount, maxHealth);
        
    }

    /// <summary>
    /// Return the current and max life of the player in a Vector2
    /// </summary>
    /// <returns>Current and max life of the player</returns>
    public Vector2 GetHealth()
    {
        return new Vector2(_currentHealth, maxHealth * UpgradeManager.GetHealthMultiplier());
    }
}