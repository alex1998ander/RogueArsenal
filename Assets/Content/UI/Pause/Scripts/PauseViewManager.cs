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
        resumeButton.Initialize(() => GameManager.PauseGame(false));
        mainMenuButton.Initialize(LevelManager.LoadMainMenu);
        settingsButton.Initialize(() => LevelManager.ShowSettingsMenu(true));
    }

    private void OnEnable()
    {
        if (upgradeIconGridView == null || upgradePanelDetailsView == null)
        {
            return;
        }

        var currentUpgrades = UpgradeManager.CurrentUpgrades;

        if (currentUpgrades.Count == 0)
        {
            upgradePanelDetailsView.gameObject.SetActive(false);
            upgradeIconGridView.gameObject.SetActive(false);
        }
        else
        {
            upgradePanelDetailsView.gameObject.SetActive(true);
            upgradeIconGridView.gameObject.SetActive(true);
            
            upgradeIconGridView.InitializeUpgradeView(currentUpgrades);
            upgradePanelDetailsView.InitializeUpgradePanelView(UpgradeManager.GetUpgradeFromIdentifier(currentUpgrades[0].UpgradeIdentification));
        }
    }
}