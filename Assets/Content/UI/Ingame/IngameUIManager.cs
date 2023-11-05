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
        EventManager.OnPlayerHealthUpdate.Subscribe(() => healthBarView.SetValue(PlayerData.health));
        EventManager.OnPlayerAmmoUpdate.Subscribe(() => ammoBarView.SetValue(PlayerData.ammo));
        EventManager.OnWeaponReload.Subscribe(() => ammoBarView.StartReloadBar());
        EventManager.OnPlayerAbilityUsed.Subscribe(() => abilityBarView.EmptyAndReloadBar());
        EventManager.OnPlayerPhoenixed.Subscribe(() => phoenixIndicatorView.DisableIndicator());

        UpdateBarConfigValues(PlayerData.abilityCooldown, PlayerData.maxAmmo, PlayerData.reloadTime, default, PlayerData.maxHealth);
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
}