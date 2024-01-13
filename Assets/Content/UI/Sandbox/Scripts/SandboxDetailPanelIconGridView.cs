using System.Collections.Generic;
using UnityEngine;

public class SandboxDetailPanelIconGridView : DetailPanelIconGridView
{
    [SerializeField] private ButtonView enableAllButton;
    [SerializeField] private ButtonView disableAllButton;

    private readonly List<SandboxDetailPanelIconView> _iconViews = new();

    private void Start()
    {
        enableAllButton.Initialize(ActivateAllUpgrades);
        disableAllButton.Initialize(DeactivateAllUpgrades);
    }

    protected override void InitializeUpgradeIconView(IconView iconView, UpgradeIcon upgradeIcon, Upgrade upgrade)
    {
        var sandboxIconView = iconView as SandboxDetailPanelIconView;
        
        if (sandboxIconView == null) return;
        
        sandboxIconView.Initialize(upgradeIcon.icon, upgrade.Name, detailPanelView, upgradeIcon.upgradeIdentification, UpgradeManager.IsUpgradeBinded(upgradeIcon.upgradeIdentification), this);
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