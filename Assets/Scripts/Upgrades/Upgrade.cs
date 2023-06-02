public class Upgrade {

    public virtual string Name { get; } = "";
    public virtual string Description { get; private set; } = "";
    
    public virtual float BulletRangeMultiplier { get; private set; } = 1.0f;
    public virtual int BulletCountAdjustment { get; private set; } = 0;
    public virtual float BulletDamageMultiplier { get; private set; } = 1.0f;
    public virtual float AttackSpeedMultiplier { get; private set; } = 1.0f;
    public virtual float HealthMultiplier { get; private set; } = 1.0f;
    public virtual float MovementSpeedMultiplier { get; private set; } = 1.0f;

    /// <summary>
    /// Optional action that is performed when the player fires
    /// </summary>
    /// <param name="upgradeablePlayer">Player reference</param>
    public virtual void OnFire(IUpgradeablePlayer upgradeablePlayer) { }

    /// <summary>
    /// Optional action that is performed when the player blocks
    /// </summary>
    /// <param name="upgradeablePlayer">Player reference</param>
    public virtual void OnBlock(IUpgradeablePlayer upgradeablePlayer) { }

    /// <summary>
    /// Optional action that is executed every frame while the bullet is flying
    /// </summary>
    /// <param name="upgradeableBullet">Bullet reference</param>
    public virtual void BulletUpdate(IUpgradableBullet upgradeableBullet) { }

    /// <summary>
    /// Optional action that is executed every frame 
    /// </summary>
    /// <param name="upgradeablePlayer">Player reference</param>
    public virtual void PlayerUpdate(IUpgradeablePlayer upgradeablePlayer) { }

    /// <summary>
    /// Optional action that is executed when the bullet hits something
    /// </summary>
    /// <param name="upgradeableBullet">Bullet reference</param>
    public virtual void OnBulletImpact(IUpgradableBullet upgradeableBullet) { }

    /// <summary>
    /// Optional action that is executed when the player dies
    /// </summary>
    /// <param name="upgradeablePlayer">Player reference</param>
    public virtual void OnPlayerDeath(IUpgradeablePlayer upgradeablePlayer) { }

}