using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Upgrade {

    public abstract string Name { get; }
    public virtual string Description { get; private set; } = "";
    
    public virtual float BulletSpeedMultiplier { get; private set; } = 1.0f;
    public virtual float BulletCountMultiplier { get; private set; } = 1.0f;
    public virtual float BulletDamageMultiplier { get; private set; } = 1.0f;
    public virtual float AttackSpeedMultiplier { get; private set; } = 1.0f;
    public virtual float HealthMultiplier { get; private set; } = 1.0f;
    public virtual float MovementSpeedMultiplier { get; private set; } = 1.0f;

    /// <summary>
    /// Optional action that is performed when the player fires
    /// </summary>
    /// <param name="upgradeable">Player reference</param>
    public virtual void Fire(IUpgradeable upgradeable) { }

    /// <summary>
    /// Optional action that is performed when the player blocks
    /// </summary>
    /// <param name="upgradeable">Player reference</param>
    public virtual void Block(IUpgradeable upgradeable) { }

    /// <summary>
    /// Optional action that is executed every frame while the bullet is flying
    /// </summary>
    /// <param name="upgradeable">Player reference</param>
    public virtual void BulletUpdate(IUpgradeable upgradeable) { }

    /// <summary>
    /// Optional action that is executed every frame 
    /// </summary>
    /// <param name="upgradeable">Player reference</param>
    public virtual void PlayerUpdate(IUpgradeable upgradeable) { }

}