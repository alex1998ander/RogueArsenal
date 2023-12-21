using System;
using UnityEngine;

// Event naming convention: On + [contributor] + [action / event]
// Examples for contributors: Player, Enemy, Weapon

public static class EventManager
{
    #region Player Events

    public static readonly Event OnPlayerAmmoUpdate = new();
    public static readonly Event<float> OnPlayerHealthUpdate = new();

    public static readonly Event OnPlayerAbilityUsed = new();
    public static readonly Event OnPlayerCollectCurrency = new();
    public static readonly Event OnPlayerDeath = new();
    public static readonly Event OnPlayerPhoenixed = new();
    public static readonly Event OnPlayerShotFired = new();
    public static readonly Event OnPlayerDash = new();

    public static readonly Event OnWeaponReload = new();

    #endregion

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    #region Enemy Events

    public static readonly Event<float> OnEnemyDamage = new();
    public static readonly Event<Vector3> OnEnemyDeath = new();
    public static readonly Event OnEnemyShotFired = new();

    #endregion

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    #region Level Events

    public static readonly Event OnLevelExit = new();
    public static readonly Event OnLevelEnter = new();

    #endregion

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    #region Game Events

    public static readonly Event<bool> OnPauseGame = new();
    public static readonly Event OnMainMenuEnter = new();
    public static readonly Event OnUpgradeChange = new();

    #endregion
}