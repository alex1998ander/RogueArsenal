using TMPro;
using UnityEngine;

public class UpgradeSelectionManager : MonoBehaviour
{
    [SerializeField] private UpgradePanelView[] upgradeViews;
    [SerializeField] private IconGridView upgradeIconView;
    [SerializeField] private TextMeshProUGUI upgradeInventoryHeaderText;

    private void Start()
    {
        var upgradeSelection = UpgradeManager.GenerateNewRandomUpgradeSelection(3);
        
        upgradeViews[0].InitializeUpgradePanelView(upgradeSelection[0]);
        upgradeViews[1].InitializeUpgradePanelView(upgradeSelection[1]);
        upgradeViews[2].InitializeUpgradePanelView(upgradeSelection[2]);

        var currentUpgrades = UpgradeManager.CurrentUpgrades;
        
        if (currentUpgrades.Count == 0)
        {
            upgradeInventoryHeaderText.gameObject.SetActive(false);
            
        }
        else
        {
            upgradeInventoryHeaderText.gameObject.SetActive(true);
            upgradeIconView.InitializeUpgradeView(currentUpgrades);
        }
    }
}


