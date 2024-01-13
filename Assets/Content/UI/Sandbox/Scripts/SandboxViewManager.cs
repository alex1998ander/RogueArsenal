using UnityEngine;

public class SandboxViewManager : MonoBehaviour
{
    [SerializeField] private UpgradePanelView upgradePanelDetailsView;
    [SerializeField] private SandboxDetailPanelIconGridView sandboxDetailPanelIconGridView;
    [SerializeField] private StringButtonView applyButton;
    [SerializeField] private StringButtonView settingsButton;
    [SerializeField] private StringButtonView mainMenuButton;

    private void Start()
    {
        sandboxDetailPanelIconGridView.InitializeUpgradeView(UpgradeManager.DefaultUpgradePool);
        upgradePanelDetailsView.InitializeUpgradePanelView(UpgradeManager.GetUpgradeFromIdentifier(UpgradeIdentification.BigBullet));
        
        applyButton.Initialize(() => GameManager.PauseGame(false));
        settingsButton.Initialize(() => LevelManager.ShowSettingsMenu(true));
        mainMenuButton.Initialize(LevelManager.LoadMainMenu);
    }
}
