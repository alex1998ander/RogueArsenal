using UnityEngine;

public class EnemyShieldGenerator : MonoBehaviour, ICharacterHealth
{
    [SerializeField] private Collider2D bossCollider2D;

    private float _currentHealth;

    private void Awake()
    {
        _currentHealth = Configuration.Boss_ShieldMaxHealth;
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
            _currentHealth = Configuration.Boss_ShieldMaxHealth;
        }
    }

    public bool IsDead()
    {
        return _currentHealth <= 0;
    }

    void Update()
    {
        transform.RotateAround(transform.position, new Vector3(0, 0, 1), Configuration.Boss_ShieldRotationSpeed * Time.deltaTime);
    }
}