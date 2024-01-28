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
    public static readonly Event OnPlayerHit = new();
    public static readonly Event OnPlayerDeath = new();

    public static readonly Event OnPlayerShot = new();
    public static readonly Event OnPlayerShotEmpty = new();
    public static readonly Event OnPlayerBulletDestroyed = new();
    public static readonly Event OnPlayerDash = new();

    public static readonly Event OnWeaponReloadStart = new();
    public static readonly Event OnWeaponReloadEnd = new();

    #endregion

    #region Upgrade Events

    public static readonly Event OnBulletBounce = new();
    public static readonly Event OnExplosiveBulletExplosion = new();
    public static readonly Event OnHealingFieldStart = new();
    public static readonly Event OnShieldStart = new();
    public static readonly Event OnPhoenixRevive = new();
    public static readonly Event OnShockwave = new();
    public static readonly Event OnStimpack = new();
    public static readonly Event OnTimefreeze = new();

    #endregion

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    #region Enemy Events

    public static readonly Event<float> OnEnemyDamage = new();
    public static readonly Event<Vector3> OnEnemyDeath = new();
    public static readonly Event OnEnemyShotFired = new();
    public static readonly Event OnEnemyBulletDestroyed = new();
    public static readonly Event<Vector3, int> OnEnemyCurrencyDropped = new();

    #endregion

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    #region Level Events

    public static readonly Event OnLevelExit = new();
    public static readonly Event OnLevelEnter = new();

    #endregion

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    #region Game Events

    public static readonly Event OnStartGame = new();
    public static readonly Event<bool> OnPauseGame = new();
    public static readonly Event<bool> OnFreezeGamePlay = new();
    public static readonly Event OnMainMenuEnter = new();
    public static readonly Event OnUpgradeChange = new();

    #endregion
}