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
        EventManager.OnWeaponReloadStart.Subscribe(Reload);
        EventManager.OnPlayerAbilityUsed.Subscribe(AbilityUsed);
        EventManager.OnPhoenixRevive.Subscribe(Phoenixed);
        EventManager.OnPlayerCollectCurrency.Subscribe(CollectedCurrency);
        EventManager.OnUpgradeChange.Subscribe(UpdateBarConfigValues);

        UpdateBarConfigValues();
        SetIngameUIValues();
    }

    public void Init_Sandbox()
    {
        UpdateBarConfigValues();
        SetIngameUIValues();
    }

    private void Phoenixed()
    {
        phoenixIndicatorView.DisableIndicator();
    }

    private void CollectedCurrency()
    {
        currencyBarView.SetValue(ProgressionManager.CollectedCurrency);
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

    private void HealthUpdate(float healthChange)
    {
        healthBarView.SetValue(PlayerData.health);
    }

    private void UpdateBarConfigValues()
    {
        UpdateBarConfigValues(PlayerData.abilityCooldown, PlayerData.maxAmmo, PlayerData.reloadTime, ProgressionManager.CurrentUpgradePrice, PlayerData.maxHealth);
    }
    
    private void UpdateBarConfigValues(float abilityReloadTime, float ammoBarMaxValue, float ammoBarReloadTime, float currencyBarMaxValue, float healthBarMaxValue)
    {
        abilityBarView.SetReloadTime(abilityReloadTime);
        ammoBarView.SetMaxValue(ammoBarMaxValue);
        ammoBarView.SetReloadTime(ammoBarReloadTime);
        currencyBarView.SetMaxValue(currencyBarMaxValue);
        healthBarView.SetMaxValue(healthBarMaxValue);
    }

    private void SetIngameUIValues()
    {
        abilityBarView.SetViewToDefault();
        ammoBarView.SetValue(PlayerData.ammo);
        currencyBarView.SetValue(ProgressionManager.CollectedCurrency);
        healthBarView.SetValue(PlayerData.health);
        phoenixIndicatorView.ShowIndicator(UpgradeManager.IsPhoenixActive);
        
    }

    private void OnDestroy()
    {
        EventManager.OnPlayerHealthUpdate.Unsubscribe(HealthUpdate);
        EventManager.OnPlayerAmmoUpdate.Unsubscribe(AmmoUpdate);
        EventManager.OnWeaponReloadStart.Unsubscribe(Reload);
        EventManager.OnPlayerAbilityUsed.Unsubscribe(AbilityUsed);
        EventManager.OnPhoenixRevive.Unsubscribe(Phoenixed);
        EventManager.OnPlayerCollectCurrency.Unsubscribe(CollectedCurrency);
        EventManager.OnUpgradeChange.Unsubscribe(UpdateBarConfigValues);
    }
}