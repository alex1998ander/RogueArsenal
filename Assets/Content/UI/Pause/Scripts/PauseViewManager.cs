using UnityEngine;

public class PauseViewManager : MonoBehaviour
{
    [SerializeField] private UpgradePanelView upgradePanelDetailsView;
    [SerializeField] private DetailPanelIconGridView upgradeIconGridView;
    [SerializeField] private StringButtonView resumeButton;
    [SerializeField] private StringButtonView mainMenuButton;
    [SerializeField] private StringButtonView settingsButton;

    private void Start()
    {
        upgradeIconGridView.InitializeUpgradeView(UpgradeManager.CurrentUpgrades);
        upgradePanelDetailsView.InitializeUpgradePanelView(UpgradeManager.GetUpgradeFromIdentifier(UpgradeIdentification.BigBullet));
        
        resumeButton.Initialize(GameManager.TogglePause);
        mainMenuButton.Initialize(LevelManager.LoadMainMenu);
        settingsButton.Initialize(() => LevelManager.ShowSettingsMenu(true));
    }
}
