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
        _nextDamageTimestamp = Time.time + 1f / Configuration.DemonicPact_DamageBurstsPerSecond;
    }

    public override void PlayerUpdate(PlayerController playerController)
    {
        if (Time.time >= _nextDamageTimestamp && SpawnController.CheckEnemiesAlive())
        {
            float currentHealthPercentage = PlayerData.health / PlayerData.maxHealth;
            if (currentHealthPercentage >= Configuration.DemonicPact_MinHealthPercentage)
            {
                _playerController.playerHealth.InflictDamage(Configuration.DemonicPact_DamagePerSecond / Configuration.DemonicPact_DamageBurstsPerSecond, false, true);
                _nextDamageTimestamp = Time.time + 1f / Configuration.DemonicPact_DamageBurstsPerSecond;
            }
        }
    }

    public override bool OnBulletTrigger(PlayerBullet playerBullet, Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            float healMultiplierBase = Mathf.InverseLerp(Configuration.DemonicPact_MinBulletDamageBase, Configuration.DemonicPact_MaxBulletDamageBase, playerBullet.Damage);
            float healAmount = Mathf.Lerp(Configuration.DemonicPact_MinHealAmount, Configuration.DemonicPact_MaxHealAmount, healMultiplierBase);

            _playerController.playerHealth.Heal(healAmount);
            _nextDamageTimestamp = Time.time + (float) Configuration.DemonicPact_IgnoredDamageBurstsAfterHeal / Configuration.DemonicPact_DamageBurstsPerSecond;
        }

        return base.OnBulletTrigger(playerBullet, other);
    }
}