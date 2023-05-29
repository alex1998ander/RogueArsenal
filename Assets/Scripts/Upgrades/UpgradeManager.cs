using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UpgradeManager {
    private static readonly Upgrade[] Upgrades = new Upgrade[5];
    private static byte _currentUpgradeIndex = 0;

    /// <summary>
    /// Binds an upgrade into the upgrade inventory on the oldest upgrade's position.
    /// </summary>
    /// <param name="upgrade">New Upgrade</param>
    public static void BindUpgrade(Upgrade upgrade) {
        Upgrades[_currentUpgradeIndex++] = upgrade;
    }

    public static float GetBulletSpeedMultiplier() {
        return Upgrades[0].BulletSpeedMultiplier *
               Upgrades[1].BulletSpeedMultiplier *
               Upgrades[2].BulletSpeedMultiplier *
               Upgrades[3].BulletSpeedMultiplier *
               Upgrades[4].BulletSpeedMultiplier;
    }

    public static float GetBulletCountMultiplier() {
        return Upgrades[0].BulletCountMultiplier *
               Upgrades[1].BulletCountMultiplier *
               Upgrades[2].BulletCountMultiplier *
               Upgrades[3].BulletCountMultiplier *
               Upgrades[4].BulletCountMultiplier;
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
    
    public static void Fire(IUpgradeable upgradeable) {
        Upgrades[0].Fire(upgradeable);
        Upgrades[1].Fire(upgradeable);
        Upgrades[2].Fire(upgradeable);
        Upgrades[3].Fire(upgradeable);
        Upgrades[4].Fire(upgradeable);
    }

    public static void Block(IUpgradeable upgradeable) {
        Upgrades[0].Block(upgradeable);
        Upgrades[1].Block(upgradeable);
        Upgrades[2].Block(upgradeable);
        Upgrades[3].Block(upgradeable);
        Upgrades[4].Block(upgradeable);
    }

    public static void BulletUpdate(IUpgradeable upgradeable) {
        Upgrades[0].BulletUpdate(upgradeable);
        Upgrades[1].BulletUpdate(upgradeable);
        Upgrades[2].BulletUpdate(upgradeable);
        Upgrades[3].BulletUpdate(upgradeable);
        Upgrades[4].BulletUpdate(upgradeable);
    }

    public static void MovementUpdate(IUpgradeable upgradeable) {
        Upgrades[0].PlayerUpdate(upgradeable);
        Upgrades[1].PlayerUpdate(upgradeable);
        Upgrades[2].PlayerUpdate(upgradeable);
        Upgrades[3].PlayerUpdate(upgradeable);
        Upgrades[4].PlayerUpdate(upgradeable);
    }
}