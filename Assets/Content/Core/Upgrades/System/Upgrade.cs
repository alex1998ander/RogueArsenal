using UnityEngine;

public class Upgrade
{
    public  virtual string Name { get; } = "";
    public virtual string Description { get; private set; } = "";

    public virtual float BulletRange { get; private set; } = 0f;
    public virtual int BulletCount { get; private set; } = 0;
    public virtual float BulletDamage { get; private set; } = 0f;
    public virtual float BulletSize { get; private set; } = 0f;
    public virtual float FireDelay { get; private set; } = 0f;
    public virtual float BlockDelay { get; private set; } = 0f;
    public virtual float Health { get; private set; } = 0f;
    public virtual float MovementSpeed { get; private set; } = 0f;

    /// <summary>
    /// Optional functionality for initialization
    /// </summary>
    /// <param name="upgradeablePlayer">Player reference</param>
    public virtual void Init(IUpgradeablePlayer upgradeablePlayer)
    {
    }

    /// <summary>
    /// Optional functionality for initialization
    /// </summary>
    /// <param name="upgradeableBullet">Bullet reference</param>
    public virtual void Init(IUpgradeableBullet upgradeableBullet)
    {
    }

    /// <summary>
    /// Optional functionality that is performed when the player fires
    /// </summary>
    /// <param name="upgradeablePlayer">Player reference</param>
    public virtual void OnFire(IUpgradeablePlayer upgradeablePlayer)
    {
    }

    /// <summary>
    /// Optional functionality that is performed when the player blocks
    /// </summary>
    /// <param name="upgradeablePlayer">Player reference</param>
    public virtual void OnBlock(IUpgradeablePlayer upgradeablePlayer)
    {
    }

    /// <summary>
    /// Optional functionality that is executed every frame while the bullet is flying
    /// </summary>
    /// <param name="upgradeableBullet">Bullet reference</param>
    public virtual void BulletUpdate(IUpgradeableBullet upgradeableBullet)
    {
    }

    /// <summary>
    /// Optional functionality that is executed for the player every frame 
    /// </summary>
    /// <param name="upgradeablePlayer">Player reference</param>
    public virtual void PlayerUpdate(IUpgradeablePlayer upgradeablePlayer)
    {
    }

    /// <summary>
    /// Optional functionality that is executed when the bullet hits something
    /// </summary>
    /// <param name="upgradeableBullet">Bullet reference</param>
    /// <param name="collision">Collision information</param>
    /// <returns>Bool, whether the bullet should NOT be destroyed afterwards</returns>
    public virtual bool OnBulletImpact(IUpgradeableBullet upgradeableBullet, Collision2D collision)
    {
        return false;
    }

    /// <summary>
    /// Optional functionality that is executed when the player dies
    /// </summary>
    /// <param name="upgradeablePlayer">Player reference</param>
    public virtual void OnPlayerDeath(IUpgradeablePlayer upgradeablePlayer)
    {
    }
}