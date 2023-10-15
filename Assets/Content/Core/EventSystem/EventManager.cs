using System;
using UnityEngine;

// Event naming convention: On + [contributor] + [action / event]
// Examples for contributors: Player, Enemy, Weapon

public static class EventManager
{
    #region Player Events

    /// <summary>
    /// The player takes damage. The amount of damage is passed as a parameter.
    /// </summary>
    public static readonly Event<float> OnPlayerDamage = new();

    /// <summary>
    /// The player dies.
    /// </summary>
    public static readonly Event OnPlayerDeath = new();

    /// <summary>
    /// The player just fired a shot.
    /// </summary>
    public static readonly Event OnPlayerShotFired = new();

    /// <summary>
    /// The player collected currency.
    /// </summary>
    public static readonly Event OnPlayerCollectCurrency = new();

    #endregion

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    #region Enemy Events

    /// <summary>
    /// A Enemy takes damage. The amount of damage is passed as a parameter.
    /// </summary>
    public static readonly Event<float> OnEnemyDamage = new();

    /// <summary>
    /// The player dies.
    /// </summary>
    public static readonly Event<GameObject> OnEnemyDeath = new();
    
    /// <summary>
    /// The enemy just fired a shot.
    /// </summary>
    public static readonly Event OnEnemyShotFired = new();

    #endregion

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    #region Level Events

    /// <summary>
    /// The player exits a level and enters the upgrade selection screen.
    /// </summary>
    public static readonly Event OnLevelExit = new();

    /// <summary>
    /// The player exits the upgrade selection screen and enters a level.
    /// </summary>
    public static readonly Event OnLevelEnter = new();

    #endregion
    
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    #region Game Events

    public static readonly Event<bool> OnPauseGame = new();
    
    public static readonly Event OnMainMenuEnter = new();

    #endregion
}