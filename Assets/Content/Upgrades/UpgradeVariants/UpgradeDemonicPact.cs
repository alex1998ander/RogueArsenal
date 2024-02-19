using System.Collections;
using UnityEngine;

public class UpgradeDemonicPact : Upgrade
{
    public override string Name => "Demonic Pact";
    public override UpgradeIdentification UpgradeIdentification => UpgradeIdentification.DemonicPact;
    public override UpgradeType UpgradeType => UpgradeType.Weapon;
    public override string FlavorText => "Embrace the dark arts of bullet wizardry and trade a bit of your life essence for instant trigger happiness.";
    public override string Description => "Dealing damage heals you, but you constantly take damage";

    private PlayerController _playerController;

    private float _nextDamageTimestamp;

    public override void Init(PlayerController playerController)
    {
        _playerController = playerController;
        _nextDamageTimestamp = Time.time + 1f / Configuration.DemonicPact_BurstsPerSecond;
    }

    public override void PlayerUpdate(PlayerController playerController)
    {
        if (Time.time >= _nextDamageTimestamp && SpawnController.CheckEnemiesAlive())
        {
            float currentHealthPercentage = PlayerData.health / PlayerData.maxHealth;
            if (currentHealthPercentage >= Configuration.DemonicPact_MinHealthPercentage)
            {
                _playerController.playerHealth.InflictDamage(Configuration.DemonicPact_HealthLossPerSecond / Configuration.DemonicPact_BurstsPerSecond, false, true);
                _nextDamageTimestamp = Time.time + 1f / Configuration.DemonicPact_BurstsPerSecond;
            }
        }
    }

    public override bool OnBulletTrigger(PlayerBullet playerBullet, Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            _playerController.playerHealth.Heal(Configuration.DemonicPact_BaseHealAmount * playerBullet.Damage);
            _nextDamageTimestamp = Time.time + 1f / Configuration.DemonicPact_BurstsPerSecond;
        }

        return base.OnBulletTrigger(playerBullet, other);
    }
}