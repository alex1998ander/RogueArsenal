using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class UpgradeManager
{
    private static readonly List<Upgrade> Upgrades = new() {  };

    private static int _nextReplacementIndex;

    private static Upgrade[] _currentUpgradeSelection = { };

    private static readonly List<Upgrade> UpgradePool = new()
    {
        new UpgradeHitman(),
        new UpgradeBuckshot(),
        new UpgradeBurst(),
        new UpgradeBounce(),
        new UpgradeCarefulPlanning(),
        new UpgradeTank(),
        //new UpgradeExplosiveBullet(),
        new UpgradeHealingField(),
        new UpgradeHoming(),
        new UpgradePhoenix(),
        new UpgradeBigBullet(),
        new UpgradeMentalMeltdown(),
        new UpgradeDemonicPact(),
        //new UpgradeDrill(),
        new UpgradeGlassCannon()
    };

    /// <summary>
    /// Returns the bound upgrade at the passed index.
    /// </summary>
    /// <param name="index">Upgrade index</param>
    /// <returns>Upgrade at index</returns>
    public static Upgrade GetUpgradeAtIndex(int index)
    {
        if (index < Upgrades.Count)
        {
            return Upgrades[index];
        }

        return null;
    }

    /// <summary>
    /// Binds an upgrade from the current upgrade selection to the upgrade inventory to the upgrade slot of the current oldest upgrade.
    /// </summary>
    /// <param name="selectionIdx">Index of the new upgrade in the upgrade selection</param>
    public static void BindUpgrade(int selectionIdx)
    {
        Upgrade newUpgrade = _currentUpgradeSelection[selectionIdx];

        // Replace upgrade
        Upgrades.Add(newUpgrade);

        // Remove new upgrade from upgrade pool
        UpgradePool.Remove(newUpgrade);
    }

    /// <summary>
    /// Generates a new random upgrade selection from all currently unused upgrades.
    /// </summary>
    /// <returns>New random upgrade selection</returns>
    public static Upgrade[] GenerateNewRandomUpgradeSelection()
    {
        System.Random rnd = new System.Random();
        _currentUpgradeSelection = UpgradePool.OrderBy(x => rnd.Next()).Take(5).ToArray();

        return _currentUpgradeSelection;
    }

    /// <summary>
    /// Clears all applied upgrades
    /// </summary>
    public static void ClearUpgrades()
    {
        Upgrades.Clear();
    }

    /// <summary>
    /// Calculates the multiplier from the passed values. It is ensured that no negative multipliers occur.
    /// </summary>
    /// <param name="attributeSelector">Upgrade attribute</param>
    /// <returns>Calculated Multiplier</returns>
    private static float GetAttributeMultiplier(Func<Upgrade, float> attributeSelector)
    {
        float multiplier = 1f;

        foreach (Upgrade upgrade in Upgrades)
        {
            multiplier += attributeSelector(upgrade);
        }

        return Mathf.Max(0, multiplier);
    }

    /// <summary>
    /// Calculates the bullet range multiplier of all upgrades.
    /// </summary>
    /// <returns>Common bullet range multiplier</returns>
    public static float GetBulletRangeMultiplier()
    {
        return GetAttributeMultiplier(upgrade => upgrade.BulletRange);
    }

    /// <summary>
    /// Calculates the bullet speed multiplier of all upgrades.
    /// </summary>
    /// <returns>Common bullet speed multiplier</returns>
    public static float GetBulletSpeedMultiplier()
    {
        return GetAttributeMultiplier(upgrade => upgrade.BulletSpeed);
    }

    /// <summary>
    /// Calculates the bullet count adjustment of all upgrades.
    /// </summary>
    /// <returns>Common bullet count adjustment</returns>
    public static int GetBulletCountAdjustment()
    {
        int bulletCountAdjustment = 0;

        foreach (Upgrade upgrade in Upgrades)
        {
            bulletCountAdjustment += upgrade.BulletCount;
        }

        return bulletCountAdjustment;
    }

    /// <summary>
    /// Calculates the bullet damage multiplier of all upgrades.
    /// </summary>
    /// <returns>Common bullet damage multiplier</returns>
    public static float GetBulletDamageMultiplier()
    {
        return GetAttributeMultiplier(upgrade => upgrade.BulletDamage);
    }

    /// <summary>
    /// Calculates the bullet size multiplier of all upgrades.
    /// </summary>
    /// <returns>Common bullet size multiplier</returns>
    public static float GetBulletSizeMultiplier()
    {
        return GetAttributeMultiplier(upgrade => upgrade.BulletSize);
    }

    /// <summary>
    /// Calculates the fire delay multiplier of all upgrades.
    /// </summary>
    /// <returns>Common fire delay multiplier</returns>
    public static float GetFireDelayMultiplier()
    {
        return GetAttributeMultiplier(upgrade => upgrade.FireDelay);
    }

    /// <summary>
    /// Calculates the block delay multiplier of all upgrades.
    /// </summary>
    /// <returns>Common block delay multiplier</returns>
    public static float GetBlockDelayMultiplier()
    {
        return GetAttributeMultiplier(upgrade => upgrade.BlockDelay);
    }

    /// <summary>
    /// Calculates the health multiplier of all upgrades.
    /// </summary>
    /// <returns>Common health multiplier</returns>
    public static float GetHealthMultiplier()
    {
        return GetAttributeMultiplier(upgrade => upgrade.Health);
    }

    /// <summary>
    /// Calculates the movement speed multiplier of all upgrades.
    /// </summary>
    /// <returns>Common movement speed multiplier</returns>
    public static float GetMovementSpeedMultiplier()
    {
        return GetAttributeMultiplier(upgrade => upgrade.MovementSpeed);
    }

    /// <summary>
    /// Executes the player initialization functions of all assigned upgrades
    /// </summary>
    /// <param name="upgradeablePlayer">Player reference</param>
    public static void Init(IUpgradeablePlayer upgradeablePlayer)
    {
        foreach (Upgrade upgrade in Upgrades)
        {
            upgrade.Init(upgradeablePlayer);
        }
    }

    /// <summary>
    /// Executes the bullet initialization functions of all assigned upgrades
    /// </summary>
    /// <param name="upgradeableBullet">Bullet reference</param>
    public static void Init(IUpgradeableBullet upgradeableBullet)
    {
        foreach (Upgrade upgrade in Upgrades)
        {
            upgrade.Init(upgradeableBullet);
        }
    }

    /// <summary>
    /// Executes the functionalities of all assigned upgrades when the player fires
    /// </summary>
    /// <param name="upgradeablePlayer">Player reference</param>
    public static void OnFire(IUpgradeablePlayer upgradeablePlayer)
    {
        foreach (Upgrade upgrade in Upgrades)
        {
            upgrade.OnFire(upgradeablePlayer);
        }
    }

    /// <summary>
    /// Executes the functionalities of all assigned upgrades when the player blocks
    /// </summary>
    /// <param name="upgradeablePlayer">Player reference</param>
    public static void OnBlock(IUpgradeablePlayer upgradeablePlayer)
    {
        foreach (Upgrade upgrade in Upgrades)
        {
            upgrade.OnBlock(upgradeablePlayer);
        }
    }

    /// <summary>
    /// Executes the functionalities of all assigned upgrades every frame while the bullet is flying
    /// </summary>
    /// <param name="upgradeableBullet">Bullet reference</param>
    public static void BulletUpdate(IUpgradeableBullet upgradeableBullet)
    {
        foreach (Upgrade upgrade in Upgrades)
        {
            upgrade.BulletUpdate(upgradeableBullet);
        }
    }

    /// <summary>
    /// Executes the functionalities of all assigned upgrades for the player every frame 
    /// </summary>
    /// <param name="upgradeablePlayer">Player reference</param>
    public static void PlayerUpdate(IUpgradeablePlayer upgradeablePlayer)
    {
        foreach (Upgrade upgrade in Upgrades)
        {
            upgrade.PlayerUpdate(upgradeablePlayer);
        }
    }

    /// <summary>
    /// Executes the functionalities of all assigned upgrades when the bullet hits something
    /// </summary>
    /// <param name="upgradeableBullet">Bullet reference</param>
    /// <param name="collision">Collision information</param>
    /// <returns>Bool, whether the bullet should survive afterwards</returns>
    public static bool OnBulletImpact(IUpgradeableBullet upgradeableBullet, Collision2D collision)
    {
        // binary unconditional logical OR ('|' not '||') needed to evaluate every operand (no short-circuiting)
        bool bulletSurvives = false;

        foreach (Upgrade upgrade in Upgrades)
        {
            bulletSurvives |= upgrade.OnBulletImpact(upgradeableBullet, collision);
        }

        return bulletSurvives;
    }

    /// <summary>
    /// Executes the functionalities of all assigned upgrades when the player dies
    /// </summary>
    /// <param name="upgradeablePlayer">Player reference</param>
    public static void OnPlayerDeath(IUpgradeablePlayer upgradeablePlayer)
    {
        foreach (Upgrade upgrade in Upgrades)
        {
            upgrade.OnPlayerDeath(upgradeablePlayer);
        }
    }
}