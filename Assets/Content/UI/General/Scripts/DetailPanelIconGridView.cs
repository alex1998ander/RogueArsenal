using UnityEngine;

public class DetailPanelIconGridView : IconGridView
{
    [SerializeField] protected UpgradePanelView detailPanelView;
    
    protected override void InitializeUpgradeIconView(IconView iconView, UpgradeIcon upgradeIcon, Upgrade upgrade)
    {
        var detailPanelIconView = iconView as DetailPanelIconView;
        
        if (detailPanelIconView == null) return;
        
        detailPanelIconView.Initialize(upgradeIcon.icon, upgrade.Name, detailPanelView, upgradeIcon.upgradeIdentification);
    }
}