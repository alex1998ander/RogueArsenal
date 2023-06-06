using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UpgradeManager {
    private static readonly Upgrade[] Upgrades = { new UpgradeBurst(), new UpgradeBounce(), new UpgradeBuckshot(), new(), new() };
    private static byte _currentUpgradeIndex = 0;

    /// <summary>
    /// Binds an upgrade into the upgrade inventory on the oldest upgrade's position.
    /// </summary>
    /// <param name="upgrade">New Upgrade</param>
    public static void BindUpgrade(Upgrade upgrade) {
        Upgrades[_currentUpgradeIndex++] = upgrade;
    }

    /// <summary>
    /// Calculates the bullet range multiplier of all upgrades.
    /// </summary>
    /// <returns>Common bullet range multiplier</returns>
    public static float GetBulletRangeMultiplier() {
        return Upgrades[0].BulletRangeMultiplier *
               Upgrades[1].BulletRangeMultiplier *
               Upgrades[2].BulletRangeMultiplier *
               Upgrades[3].BulletRangeMultiplier *
               Upgrades[4].BulletRangeMultiplier;
    }

    /// <summary>
    /// Calculates the bullet count adjustment of all upgrades.
    /// </summary>
    /// <returns>Common bullet count adjustment</returns>
    public static int GetBulletCountAdjustment() {
        return Upgrades[0].BulletCountAdjustment +
               Upgrades[1].BulletCountAdjustment +
               Upgrades[2].BulletCountAdjustment +
               Upgrades[3].BulletCountAdjustment +
               Upgrades[4].BulletCountAdjustment;
    }

    /// <summary>
    /// Calculates the bullet damage multiplier of all upgrades.
    /// </summary>
    /// <returns>Common bullet damage multiplier</returns>
    public static float GetBulletDamageMultiplier() {
        return Upgrades[0].BulletDamageMultiplier *
               Upgrades[1].BulletDamageMultiplier *
               Upgrades[2].BulletDamageMultiplier *
               Upgrades[3].BulletDamageMultiplier *
               Upgrades[4].BulletDamageMultiplier;
    }

    /// <summary>
    /// Calculates the attack speed multiplier of all upgrades.
    /// </summary>
    /// <returns>Common attack speed multiplier</returns>
    public static float GetAttackSpeedMultiplier() {
        return Upgrades[0].AttackSpeedMultiplier *
               Upgrades[1].AttackSpeedMultiplier *
               Upgrades[2].AttackSpeedMultiplier *
               Upgrades[3].AttackSpeedMultiplier *
               Upgrades[4].AttackSpeedMultiplier;
    }

    /// <summary>
    /// Calculates the health multiplier of all upgrades.
    /// </summary>
    /// <returns>Common health multiplier</returns>
    public static float GetHealthMultiplier() {
        return Upgrades[0].HealthMultiplier *
               Upgrades[1].HealthMultiplier *
               Upgrades[2].HealthMultiplier *
               Upgrades[3].HealthMultiplier *
               Upgrades[4].HealthMultiplier;
    }

    /// <summary>
    /// Calculates the movement speed multiplier of all upgrades.
    /// </summary>
    /// <returns>Common movement speed multiplier</returns>
    public static float GetMovementSpeedMultiplier() {
        return Upgrades[0].MovementSpeedMultiplier *
               Upgrades[1].MovementSpeedMultiplier *
               Upgrades[2].MovementSpeedMultiplier *
               Upgrades[3].MovementSpeedMultiplier *
               Upgrades[4].MovementSpeedMultiplier;
    }

    /// <summary>
    /// Executes the player initialization functions of all assigned upgrades
    /// </summary>
    /// <param name="upgradeablePlayer">Player reference</param>
    public static void Init(IUpgradeablePlayer upgradeablePlayer) {
        Upgrades[0].Init(upgradeablePlayer);
        Upgrades[1].Init(upgradeablePlayer);
        Upgrades[2].Init(upgradeablePlayer);
        Upgrades[3].Init(upgradeablePlayer);
        Upgrades[4].Init(upgradeablePlayer);
    }

    /// <summary>
    /// Executes the bullet initialization functions of all assigned upgrades
    /// </summary>
    /// <param name="upgradeableBullet">Bullet reference</param>
    public static void Init(IUpgradeableBullet upgradeableBullet) {
        Upgrades[0].Init(upgradeableBullet);
        Upgrades[1].Init(upgradeableBullet);
        Upgrades[2].Init(upgradeableBullet);
        Upgrades[3].Init(upgradeableBullet);
        Upgrades[4].Init(upgradeableBullet);
    }

    /// <summary>
    /// Executes the functionalities of all assigned upgrades when the player fires
    /// </summary>
    /// <param name="upgradeablePlayer">Player reference</param>
    public static void OnFire(IUpgradeablePlayer upgradeablePlayer) {
        Upgrades[0].OnFire(upgradeablePlayer);
        Upgrades[1].OnFire(upgradeablePlayer);
        Upgrades[2].OnFire(upgradeablePlayer);
        Upgrades[3].OnFire(upgradeablePlayer);
        Upgrades[4].OnFire(upgradeablePlayer);
    }

    /// <summary>
    /// Executes the functionalities of all assigned upgrades when the player blocks
    /// </summary>
    /// <param name="upgradeablePlayer">Player reference</param>
    public static void OnBlock(IUpgradeablePlayer upgradeablePlayer) {
        Upgrades[0].OnBlock(upgradeablePlayer);
        Upgrades[1].OnBlock(upgradeablePlayer);
        Upgrades[2].OnBlock(upgradeablePlayer);
        Upgrades[3].OnBlock(upgradeablePlayer);
        Upgrades[4].OnBlock(upgradeablePlayer);
    }

    /// <summary>
    /// Executes the functionalities of all assigned upgrades every frame while the bullet is flying
    /// </summary>
    /// <param name="upgradeableBullet">Bullet reference</param>
    public static void BulletUpdate(IUpgradeableBullet upgradeableBullet) {
        Upgrades[0].BulletUpdate(upgradeableBullet);
        Upgrades[1].BulletUpdate(upgradeableBullet);
        Upgrades[2].BulletUpdate(upgradeableBullet);
        Upgrades[3].BulletUpdate(upgradeableBullet);
        Upgrades[4].BulletUpdate(upgradeableBullet);
    }

    /// <summary>
    /// Executes the functionalities of all assigned upgrades for the player every frame 
    /// </summary>
    /// <param name="upgradeablePlayer">Player reference</param>
    public static void PlayerUpdate(IUpgradeablePlayer upgradeablePlayer) {
        Upgrades[0].PlayerUpdate(upgradeablePlayer);
        Upgrades[1].PlayerUpdate(upgradeablePlayer);
        Upgrades[2].PlayerUpdate(upgradeablePlayer);
        Upgrades[3].PlayerUpdate(upgradeablePlayer);
        Upgrades[4].PlayerUpdate(upgradeablePlayer);
    }

    /// <summary>
    /// Executes the functionalities of all assigned upgrades when the bullet hits something
    /// </summary>
    /// <param name="upgradeableBullet">Bullet reference</param>
    /// <param name="collision">Collision information</param>
    /// <returns>Bool, whether the bullet should be destroyed afterwards</returns>
    public static bool OnBulletImpact(IUpgradeableBullet upgradeableBullet, Collision2D collision) {
        // binary unconditional logical OR ('|' not '||') needed to evaluate every operand (no short-circuiting)
        return Upgrades[0].OnBulletImpact(upgradeableBullet, collision) |
               Upgrades[1].OnBulletImpact(upgradeableBullet, collision) |
               Upgrades[2].OnBulletImpact(upgradeableBullet, collision) |
               Upgrades[3].OnBulletImpact(upgradeableBullet, collision) |
               Upgrades[4].OnBulletImpact(upgradeableBullet, collision);
    }

    /// <summary>
    /// Executes the functionalities of all assigned upgrades when the player dies
    /// </summary>
    /// <param name="upgradeablePlayer">Player reference</param>
    public static void OnPlayerDeath(IUpgradeablePlayer upgradeablePlayer) {
        Upgrades[0].OnPlayerDeath(upgradeablePlayer);
        Upgrades[1].OnPlayerDeath(upgradeablePlayer);
        Upgrades[2].OnPlayerDeath(upgradeablePlayer);
        Upgrades[3].OnPlayerDeath(upgradeablePlayer);
        Upgrades[4].OnPlayerDeath(upgradeablePlayer);
    }
}