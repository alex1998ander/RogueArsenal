using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UpgradeManager {
    private static readonly Upgrade[] Upgrades = {new UpgradeBurst(), new UpgradeBuckshot(), new(), new(), new()};
    private static byte _currentUpgradeIndex = 0;

    /// <summary>
    /// Binds an upgrade into the upgrade inventory on the oldest upgrade's position.
    /// </summary>
    /// <param name="upgrade">New Upgrade</param>
    public static void BindUpgrade(Upgrade upgrade) {
        Upgrades[_currentUpgradeIndex++] = upgrade;
    }

    public static float GetBulletRangeMultiplier() {
        return Upgrades[0].BulletRangeMultiplier *
               Upgrades[1].BulletRangeMultiplier *
               Upgrades[2].BulletRangeMultiplier *
               Upgrades[3].BulletRangeMultiplier *
               Upgrades[4].BulletRangeMultiplier;
    }

    public static int GetBulletCountAdjustment() {
        return Upgrades[0].BulletCountAdjustment +
               Upgrades[1].BulletCountAdjustment +
               Upgrades[2].BulletCountAdjustment +
               Upgrades[3].BulletCountAdjustment +
               Upgrades[4].BulletCountAdjustment;
    }

    public static float GetBulletDamageMultiplier() {
        return Upgrades[0].BulletDamageMultiplier *
               Upgrades[1].BulletDamageMultiplier *
               Upgrades[2].BulletDamageMultiplier *
               Upgrades[3].BulletDamageMultiplier *
               Upgrades[4].BulletDamageMultiplier;
    }

    public static float GetAttackSpeedMultiplier() {
        return Upgrades[0].AttackSpeedMultiplier *
               Upgrades[1].AttackSpeedMultiplier *
               Upgrades[2].AttackSpeedMultiplier *
               Upgrades[3].AttackSpeedMultiplier *
               Upgrades[4].AttackSpeedMultiplier;
    }

    public static float GetHealthMultiplier() {
        return Upgrades[0].HealthMultiplier *
               Upgrades[1].HealthMultiplier *
               Upgrades[2].HealthMultiplier *
               Upgrades[3].HealthMultiplier *
               Upgrades[4].HealthMultiplier;
    }

    public static float GetMovementSpeedMultiplier() {
        return Upgrades[0].MovementSpeedMultiplier *
               Upgrades[1].MovementSpeedMultiplier *
               Upgrades[2].MovementSpeedMultiplier *
               Upgrades[3].MovementSpeedMultiplier *
               Upgrades[4].MovementSpeedMultiplier;
    }

    public static void OnFire(IUpgradeablePlayer upgradeablePlayer) {
        Upgrades[0].OnFire(upgradeablePlayer);
        Upgrades[1].OnFire(upgradeablePlayer);
        Upgrades[2].OnFire(upgradeablePlayer);
        Upgrades[3].OnFire(upgradeablePlayer);
        Upgrades[4].OnFire(upgradeablePlayer);
    }

    public static void OnBlock(IUpgradeablePlayer upgradeablePlayer) {
        Upgrades[0].OnBlock(upgradeablePlayer);
        Upgrades[1].OnBlock(upgradeablePlayer);
        Upgrades[2].OnBlock(upgradeablePlayer);
        Upgrades[3].OnBlock(upgradeablePlayer);
        Upgrades[4].OnBlock(upgradeablePlayer);
    }

    public static void BulletUpdate(IUpgradableBullet upgradeableBullet) {
        Upgrades[0].BulletUpdate(upgradeableBullet);
        Upgrades[1].BulletUpdate(upgradeableBullet);
        Upgrades[2].BulletUpdate(upgradeableBullet);
        Upgrades[3].BulletUpdate(upgradeableBullet);
        Upgrades[4].BulletUpdate(upgradeableBullet);
    }

    public static void MovementUpdate(IUpgradeablePlayer upgradeablePlayer) {
        Upgrades[0].PlayerUpdate(upgradeablePlayer);
        Upgrades[1].PlayerUpdate(upgradeablePlayer);
        Upgrades[2].PlayerUpdate(upgradeablePlayer);
        Upgrades[3].PlayerUpdate(upgradeablePlayer);
        Upgrades[4].PlayerUpdate(upgradeablePlayer);
    }

    public static void OnBulletImpact(IUpgradableBullet upgradeableBullet) {
        Upgrades[0].OnBulletImpact(upgradeableBullet);
        Upgrades[1].OnBulletImpact(upgradeableBullet);
        Upgrades[2].OnBulletImpact(upgradeableBullet);
        Upgrades[3].OnBulletImpact(upgradeableBullet);
        Upgrades[4].OnBulletImpact(upgradeableBullet);
    }

    public static void OnPlayerDeath(IUpgradeablePlayer upgradeablePlayer) {
        Upgrades[0].OnPlayerDeath(upgradeablePlayer);
        Upgrades[1].OnPlayerDeath(upgradeablePlayer);
        Upgrades[2].OnPlayerDeath(upgradeablePlayer);
        Upgrades[3].OnPlayerDeath(upgradeablePlayer);
        Upgrades[4].OnPlayerDeath(upgradeablePlayer);
    }
}