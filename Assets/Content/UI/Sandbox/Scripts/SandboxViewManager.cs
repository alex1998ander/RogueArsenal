using UnityEngine;
using UnityEngine.Serialization;

public class SandboxViewManager : MonoBehaviour
{
    [SerializeField] private UpgradePanelView upgradePanelDetailsView;
    [SerializeField] private SandboxDetailPanelIconGridView sandboxDetailPanelIconGridView;
    [SerializeField] private StringButtonView applyButton;
    [FormerlySerializedAs("optionsButton")] [SerializeField] private StringButtonView settingsButton;
    [SerializeField] private StringButtonView mainMenuButton;

    private void Start()
    {
        sandboxDetailPanelIconGridView.InitializeUpgradeView(UpgradeManager.DefaultUpgradePool);
        upgradePanelDetailsView.InitializeUpgradePanelView(UpgradeManager.GetUpgradeFromIdentifier(UpgradeIdentification.BigBullet));
        
        applyButton.Initialize(GameManager.Pause);
        settingsButton.Initialize(() => LevelManager.ShowSettingsMenu(true));
        mainMenuButton.Initialize(null);
    }
}
