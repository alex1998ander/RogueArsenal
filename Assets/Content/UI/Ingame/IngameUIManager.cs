using System;
using UnityEngine;

public class IngameUIManager : MonoBehaviour
{
    [SerializeField] private AbilityBarView abilityBarView;
    [SerializeField] private AmmoBarView ammoBarView;
    [SerializeField] private CurrencyBarView currencyBarView;
    [SerializeField] private HealthBarView healthBarView;
    [SerializeField] private PhoenixIndicatorView phoenixIndicatorView;

    private void Start()
    {
        EventManager.OnPlayerHealthUpdate.Subscribe(HealthUpdate);
        EventManager.OnPlayerAmmoUpdate.Subscribe(AmmoUpdate);
        EventManager.OnWeaponReload.Subscribe(Reload);
        EventManager.OnPlayerAbilityUsed.Subscribe(AbilityUsed);
        EventManager.OnPlayerPhoenixed.Subscribe(Phoenixed);
        EventManager.OnUpgradeChange.Subscribe(UpgradeChange);

        UpdateBarConfigValues(PlayerData.abilityCooldown, PlayerData.maxAmmo, PlayerData.reloadTime, default, PlayerData.maxHealth);
    }

    private void UpgradeChange()
    {
        UpdateBarConfigValues(PlayerData.abilityCooldown, PlayerData.maxAmmo, PlayerData.reloadTime, default, PlayerData.maxHealth);
    }

    private void Phoenixed()
    {
        phoenixIndicatorView.DisableIndicator();
    }

    private void AbilityUsed()
    {
        abilityBarView.EmptyAndReloadBar();
    }

    private void Reload()
    {
        ammoBarView.StartReloadBar();
    }

    private void AmmoUpdate()
    {
        ammoBarView.SetValue(PlayerData.ammo);
    }

    private void HealthUpdate()
    {
        healthBarView.SetValue(PlayerData.health);
    }

    private void UpdateBarConfigValues(float abilityReloadTime, float ammoBarMaxValue, float ammoBarReloadTime, float currencyBarMaxValue, float healthBarMaxValue)
    {
        abilityBarView.SetReloadTime(abilityReloadTime);
        ammoBarView.SetMaxValue(ammoBarMaxValue);
        ammoBarView.SetReloadTime(ammoBarReloadTime);
        currencyBarView.SetMaxValue(currencyBarMaxValue);
        healthBarView.SetMaxValue(healthBarMaxValue);
    }

    private void SetIngameUIBarsToDefault()
    {
        abilityBarView.SetViewToDefault();
        ammoBarView.SetViewToDefault();
        ammoBarView.SetViewToDefault();
        currencyBarView.SetViewToDefault();
        healthBarView.SetViewToDefault();
    }

    private void OnDestroy()
    {
        EventManager.OnPlayerHealthUpdate.Unsubscribe(HealthUpdate);
        EventManager.OnPlayerAmmoUpdate.Unsubscribe(AmmoUpdate);
        EventManager.OnWeaponReload.Unsubscribe(Reload);
        EventManager.OnPlayerAbilityUsed.Unsubscribe(AbilityUsed);
        EventManager.OnPlayerPhoenixed.Unsubscribe(Phoenixed);
        EventManager.OnUpgradeChange.Unsubscribe(UpgradeChange);
    }
}