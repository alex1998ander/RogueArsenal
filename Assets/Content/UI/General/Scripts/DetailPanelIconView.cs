using UnityEngine;

public class DetailPanelIconView : IconView
{
    protected UpgradeIdentification _upgradeIdentification;
    protected UpgradePanelView _upgradePanelDetailsView;

    private void Awake()
    {
        upgradeIcon.alphaHitTestMinimumThreshold = 0.1f;
    }

    public void Initialize(Sprite sprite, string name, UpgradePanelView upgradePanelDetailsView, UpgradeIdentification upgradeIdentification)
    {
        upgradeIcon.sprite = sprite;
        upgradeName.text = name;
        _upgradeIdentification = upgradeIdentification;
        _upgradePanelDetailsView = upgradePanelDetailsView;
    }

    public void OnUpgradeHoverEnter()
    {
        _upgradePanelDetailsView.InitializeUpgradePanelView(UpgradeManager.GetUpgradeFromIdentifier(_upgradeIdentification));
        upgradeIcon.color = new Color(1f, 1f, 1f, 0.7f);
    }
    
    public void OnUpgradeHoverExit()
    {
        upgradeIcon.color = new Color(1f, 1f, 1f, 1f);
    }
}