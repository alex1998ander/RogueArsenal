using System.Collections.Generic;
using UnityEngine;

public class SandboxIconGridView : IconGridView
{
    [SerializeField] private ButtonView enableAllButton;
    [SerializeField] private ButtonView disableAllButton;

    [SerializeField] private SandboxViewManager manager;

    private readonly List<SandboxIconView> _iconViews = new();

    private void Start()
    {
        enableAllButton.Initialize(ActivateAllUpgrades);
        disableAllButton.Initialize(DeactivateAllUpgrades);
    }

    protected override void InitializeUpgradeIconView(IconView iconView, UpgradeIcon upgradeIcon, Upgrade upgrade)
    {
        var sandboxIconView = iconView as SandboxIconView;
        
        if (sandboxIconView == null) return;
        
        sandboxIconView.Initialize(upgradeIcon.icon, upgrade.Name, UpgradeManager.IsUpgradeBinded(upgradeIcon.upgradeIdentification), upgradeIcon.upgradeIdentification, this);
        _iconViews.Add(sandboxIconView);
    }

    public void ActivateUpgrade(UpgradeIdentification upgradeIdentification)
    {
        UpgradeManager.BindUpgrade_Sandbox(upgradeIdentification);
    }

    public void DeactivateUpgrade(UpgradeIdentification upgradeIdentification)
    {
        UpgradeManager.UnbindUpgrade_Sandbox(upgradeIdentification);
    }

    public void SetUpgradeDetailView(UpgradeIdentification upgradeIdentification)
    {
        manager.SetUpgradeDetailView(upgradeIdentification);
    }

    public void ActivateAllUpgrades()
    {
        foreach (var iconView in _iconViews)
        {
            iconView.SetIconEnabled();
            iconView.active = true;
        }

        UpgradeManager.BindAllUpgrades_Sandbox();
    }

    public void DeactivateAllUpgrades()
    {
        foreach (var iconView in _iconViews)
        {
            iconView.SetIconDisabled();
            iconView.active = false;
        }

        UpgradeManager.UnbindAllUpgrades_Sandbox();
    }
}