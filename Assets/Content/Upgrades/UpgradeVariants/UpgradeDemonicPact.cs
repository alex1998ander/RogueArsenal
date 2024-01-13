using UnityEngine;

public class UpgradeDemonicPact : Upgrade
{
    public override string Name => "Demonic Pact";
    public override UpgradeIdentification UpgradeIdentification => UpgradeIdentification.DemonicPact;
    public override UpgradeType UpgradeType => UpgradeType.Weapon;
    public override string FlavorText => "Embrace the dark arts of bullet wizardry and trade a bit of your life essence for instant trigger happiness.";
    public override string Description => "Embrace the dark arts of bullet wizardry and steal some life essence.";

    private PlayerController _playerController;

    public override void Init(PlayerController playerController)
    {
        _playerController = playerController;
    }

    public override void PlayerUpdate(PlayerController playerController)
    {
        if (SpawnController.CheckEnemiesAlive())
            _playerController.playerHealth.InflictDamage(Configuration.DemonicPact_HealthLossPerSecond * Time.fixedDeltaTime, true, true);
    }

    public override bool OnBulletTrigger(PlayerBullet playerBullet, Collider2D other)
    {
        if (!other.CompareTag("Player"))
            _playerController.playerHealth.Heal(Configuration.DemonicPact_BaseHealAmount * playerBullet.Damage);
        return base.OnBulletTrigger(playerBullet, other);
    }
}