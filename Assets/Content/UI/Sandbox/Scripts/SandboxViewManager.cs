using UnityEngine;

public class SandboxViewManager : MonoBehaviour
{
    [SerializeField] private UpgradePanelView upgradePanelDetailsView;
    [SerializeField] private SandboxDetailPanelIconGridView sandboxDetailPanelIconGridView;
    [SerializeField] private StringButtonView applyButton;
    [SerializeField] private StringButtonView optionsButton;
    [SerializeField] private StringButtonView mainMenuButton;

    private void Start()
    {
        sandboxDetailPanelIconGridView.InitializeUpgradeView(UpgradeManager.DefaultUpgradePool);
        upgradePanelDetailsView.InitializeUpgradePanelView(UpgradeManager.GetUpgradeFromIdentifier(UpgradeIdentification.BigBullet));
        
        applyButton.Initialize(null);
        optionsButton.Initialize(null);
        mainMenuButton.Initialize(null);
    }
}
