using UnityEngine;
using UnityEngine.Serialization;

public class SandboxViewManager : MonoBehaviour
{
    [SerializeField] private UpgradePanelView upgradePanelDetailsView;
    [SerializeField] private SandboxIconGridView sandboxIconGridView;
    [SerializeField] private StringButtonView applyButton;
    [SerializeField] private StringButtonView optionsButton;
    [SerializeField] private StringButtonView mainMenuButton;

    private void Start()
    {
        sandboxIconGridView.InitializeUpgradeView(UpgradeManager.DefaultUpgradePool);
        upgradePanelDetailsView.InitializeUpgradePanelView(UpgradeManager.GetUpgradeFromIdentifier(UpgradeIdentification.BigBullet));
        
        applyButton.Initialize(null);
        optionsButton.Initialize(null);
        mainMenuButton.Initialize(null);
    }

    public void SetUpgradeDetailView(UpgradeIdentification upgradeIdentification)
    {
        upgradePanelDetailsView.InitializeUpgradePanelView(UpgradeManager.GetUpgradeFromIdentifier(upgradeIdentification));
    }
}
