using System;
using UnityEngine;

// Event naming convention: On + [contributor] + [action / event]
// Examples for contributors: Player, Enemy, Weapon

public static class EventManager {
    #region Player Events

    /// <summary>
    /// The player fires his weapon (Fire button was pressed). This event is triggered regardless of the number of bullets or shots.
    /// </summary>
    public static readonly Event OnPlayerFire = new();

    /// <summary>
    /// The player takes damage. The amount of damage is passed as a parameter.
    /// </summary>
    public static readonly Event<float> OnPlayerDamage = new();

    /// <summary>
    /// The player dies.
    /// </summary>
    public static readonly Event OnPlayerDeath = new();

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

    #endregion

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    #region Game Events

    #endregion
}