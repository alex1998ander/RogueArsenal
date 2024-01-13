using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, ICharacterHealth
{
    [SerializeField] private float defaultContactDamageInvulnerabilityDelay = 1.0f;

    private float _contactDamageInvulnerabilityEndTimestamp;

    /// <summary>
    /// Decreases the player's health by the specified value and checks if the player dies. If so, affecting upgrades are performed and further actions are initiated.
    /// </summary>
    /// <param name="damageAmount">Amount of damage</param>
    /// <param name="fatal">Indicates whether the player can die from this damage. If the damage is greater than the current HP and the damage is not fatal, the player keeps 1 HP.</param>
    /// <param name="ignoreInvulnerability">Whether player invulnerability should be ignored</param>
    public void InflictDamage(float damageAmount, bool fatal, bool ignoreInvulnerability = false)
    {
        if (IsDead())
            return;

        if (PlayerData.invulnerable && !ignoreInvulnerability)
            return;

        PlayerData.health -= damageAmount;

        EventManager.OnPlayerHit.Trigger();
        EventManager.OnPlayerHealthUpdate.Trigger(-damageAmount);

        // if player dies
        if (IsDead())
        {
            // if player can die
            if (fatal)
            {
                // possible revival through upgrades
                UpgradeManager.OnPlayerDeath(gameObject.GetComponent<PlayerController>());

                // if player dies anyway
                if (PlayerData.health <= 0)
                {
                    gameObject.SetActive(false);

                    EventManager.OnPlayerDeath.Trigger();
                }
            }
            else
            {
                PlayerData.health = 1;
            }
        }
    }

    public bool IsDead()
    {
        return PlayerData.health <= 0;
    }

    public void InflictContactDamage(float damageAmount)
    {
        if (!PlayerData.invulnerable && Time.time > _contactDamageInvulnerabilityEndTimestamp)
        {
            _contactDamageInvulnerabilityEndTimestamp = Time.time + defaultContactDamageInvulnerabilityDelay;

            InflictDamage(damageAmount, true);
        }
    }

    /// <summary>
    /// Increases the player's health by the specified value and checks if the health is full.
    /// </summary>
    /// <param name="healingAmount">Amount of healing</param>
    public void Heal(float healingAmount)
    {
        PlayerData.health = Mathf.Min(PlayerData.health + healingAmount, PlayerController.GetMaxHealth());
        EventManager.OnPlayerHealthUpdate.Trigger(healingAmount);
    }

    /// <summary>
    /// Return the current and max life of the player in a Vector2
    /// </summary>
    /// <returns>Current and max life of the player</returns>
    [Obsolete]
    public Vector2 GetHealth()
    {
        return new Vector2(PlayerData.health, PlayerController.GetMaxHealth());
    }
}