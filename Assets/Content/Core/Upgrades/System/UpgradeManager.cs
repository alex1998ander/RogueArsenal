using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class UpgradeManager
{
<<<<<<< HEAD
    private static readonly Upgrade[] Upgrades = { new UpgradeHealingField(), new UpgradeBigBullet(), new UpgradeHoming(), new UpgradeMentalMeltdown(), new EmptyUpgradeSlot() };
=======
    private static readonly Upgrade[] Upgrades = { new UpgradeHoming(), new EmptyUpgradeSlot(), new EmptyUpgradeSlot(), new EmptyUpgradeSlot(), new EmptyUpgradeSlot() };
>>>>>>> upgrades
    private static int _nextReplacementIndex;

    private static Upgrade[] _currentUpgradeSelection = { new EmptyUpgradeSlot(), new EmptyUpgradeSlot(), new EmptyUpgradeSlot(), new EmptyUpgradeSlot(), new EmptyUpgradeSlot() };

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
        return Upgrades[index];
    }

    /// <summary>
    /// Binds an upgrade from the current upgrade selection to the upgrade inventory to the upgrade slot of the current oldest upgrade.
    /// </summary>
    /// <param name="selectionIdx">Index of the new upgrade in the upgrade selection</param>
    public static void BindUpgrade(int selectionIdx)
    {
        Upgrade oldUpgrade = Upgrades[_nextReplacementIndex];
        Upgrade newUpgrade = _currentUpgradeSelection[selectionIdx];

        // Replace upgrade
        Upgrades[_nextReplacementIndex] = newUpgrade;

        // Remove new upgrade from upgrade pool
        UpgradePool.Remove(newUpgrade);

        // Add old update back to the upgrade pool
        if (oldUpgrade is not EmptyUpgradeSlot)
        {
            UpgradePool.Add(oldUpgrade);
        }

        _nextReplacementIndex = (_nextReplacementIndex + 1) % 5;
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
    /// Calculates the multiplier from the passed values. It is ensured that no negative multipliers occur.
    /// </summary>
    /// <param name="upgrade1">1. Upgrade</param>
    /// <param name="upgrade2">2. Upgrade</param>
    /// <param name="upgrade3">3. Upgrade</param>
    /// <param name="upgrade4">4. Upgrade</param>
    /// <param name="upgrade5">5. Upgrade</param>
    /// <returns>Calculated Multiplier</returns>
    private static float CalculateMultiplier(float upgrade1, float upgrade2, float upgrade3, float upgrade4, float upgrade5)
    {
        return Mathf.Max(0, 1 + upgrade1 + upgrade2 + upgrade3 + upgrade4 + upgrade5);
    }


    /// <summary>
    /// Calculates the bullet range multiplier of all upgrades.
    /// </summary>
    /// <returns>Common bullet range multiplier</returns>
    public static float GetBulletRangeMultiplier()
    {
        return CalculateMultiplier(Upgrades[0].BulletRange, Upgrades[1].BulletRange, Upgrades[2].BulletRange, Upgrades[3].BulletRange, Upgrades[4].BulletRange);
    }

    /// <summary>
    /// Calculates the bullet count adjustment of all upgrades.
    /// </summary>
    /// <returns>Common bullet count adjustment</returns>
    public static int GetBulletCountAdjustment()
    {
        return Upgrades[0].BulletCount + Upgrades[1].BulletCount + Upgrades[2].BulletCount + Upgrades[3].BulletCount + Upgrades[4].BulletCount;
    }

    /// <summary>
    /// Calculates the bullet damage multiplier of all upgrades.
    /// </summary>
    /// <returns>Common bullet damage multiplier</returns>
    public static float GetBulletDamageMultiplier()
    {
        return CalculateMultiplier(Upgrades[0].BulletDamage, Upgrades[1].BulletDamage, Upgrades[2].BulletDamage, Upgrades[3].BulletDamage, Upgrades[4].BulletDamage);
    }

    /// <summary>
    /// Calculates the bullet size multiplier of all upgrades.
    /// </summary>
    /// <returns>Common bullet size multiplier</returns>
    public static float GetBulletSizeMultiplier()
    {
        return CalculateMultiplier(Upgrades[0].BulletSize, Upgrades[1].BulletSize, Upgrades[2].BulletSize, Upgrades[3].BulletSize, Upgrades[4].BulletSize);
    }

    /// <summary>
    /// Calculates the fire delay multiplier of all upgrades.
    /// </summary>
    /// <returns>Common fire delay multiplier</returns>
    public static float GetFireDelayMultiplier()
    {
        return CalculateMultiplier(Upgrades[0].FireDelay, Upgrades[1].FireDelay, Upgrades[2].FireDelay, Upgrades[3].FireDelay, Upgrades[4].FireDelay);
    }

    /// <summary>
    /// Calculates the block delay multiplier of all upgrades.
    /// </summary>
    /// <returns>Common block delay multiplier</returns>
    public static float GetBlockDelayMultiplier()
    {
        return CalculateMultiplier(Upgrades[0].BlockDelay, Upgrades[1].BlockDelay, Upgrades[2].BlockDelay, Upgrades[3].BlockDelay, Upgrades[4].BlockDelay);
    }

    /// <summary>
    /// Calculates the health multiplier of all upgrades.
    /// </summary>
    /// <returns>Common health multiplier</returns>
    public static float GetHealthMultiplier()
    {
        return CalculateMultiplier(Upgrades[0].Health, Upgrades[1].Health, Upgrades[2].Health, Upgrades[3].Health, Upgrades[4].Health);
    }

    /// <summary>
    /// Calculates the movement speed multiplier of all upgrades.
    /// </summary>
    /// <returns>Common movement speed multiplier</returns>
    public static float GetMovementSpeedMultiplier()
    {
        return CalculateMultiplier(Upgrades[0].MovementSpeed, Upgrades[1].MovementSpeed, Upgrades[2].MovementSpeed, Upgrades[3].MovementSpeed, Upgrades[4].MovementSpeed);
    }

    /// <summary>
    /// Executes the player initialization functions of all assigned upgrades
    /// </summary>
    /// <param name="upgradeablePlayer">Player reference</param>
    public static void Init(IUpgradeablePlayer upgradeablePlayer)
    {
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
    public static void Init(IUpgradeableBullet upgradeableBullet)
    {
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
    public static void OnFire(IUpgradeablePlayer upgradeablePlayer)
    {
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
    public static void OnBlock(IUpgradeablePlayer upgradeablePlayer)
    {
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
    public static void BulletUpdate(IUpgradeableBullet upgradeableBullet)
    {
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
    public static void PlayerUpdate(IUpgradeablePlayer upgradeablePlayer)
    {
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
    /// <returns>Bool, whether the bullet should survive afterwards</returns>
    public static bool OnBulletImpact(IUpgradeableBullet upgradeableBullet, Collision2D collision)
    {
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
    public static void OnPlayerDeath(IUpgradeablePlayer upgradeablePlayer)
    {
        Upgrades[0].OnPlayerDeath(upgradeablePlayer);
        Upgrades[1].OnPlayerDeath(upgradeablePlayer);
        Upgrades[2].OnPlayerDeath(upgradeablePlayer);
        Upgrades[3].OnPlayerDeath(upgradeablePlayer);
        Upgrades[4].OnPlayerDeath(upgradeablePlayer);
    }
}