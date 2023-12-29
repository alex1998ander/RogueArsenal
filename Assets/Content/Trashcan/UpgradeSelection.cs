using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class UpgradeSelection : MonoBehaviour
{
    //Array of the text fields
    [SerializeField] private TextMeshProUGUI[] upgradeDescription;

    [SerializeField] private int upgradeLevelCounter = 3;

    List<Upgrade> _listOfAbilityUpgrades = new List<Upgrade>();
    
    void Start()
    {
        SetupUpgradeInfo();
    }

    /// <summary>
    /// Sets up the information shown on the cards used for upgrade selection.
    /// </summary>
    private void SetupUpgradeInfo()
    {
        if (ProgressionManager.UpgradeReady)
        {
            Upgrade[] listOfWeaponUpgrades = UpgradeManager.GenerateNewRandomUpgradeSelection(3);

            // Adds the description to the cards
            for (int counter = 0; counter < upgradeDescription.Length; counter++)
            {
                upgradeDescription[counter].text =
                    "<size=24>" + listOfWeaponUpgrades[counter].Name + "</size>" + "<br>" +
                    "<br>" + "<size=12>" + listOfWeaponUpgrades[counter].Description + "</size>";
            }
        }
        else
        {
            for (int counter = 0; counter < upgradeDescription.Length; counter++)
            {
                upgradeDescription[counter].text = "<size=24>SHOULD NOT SEE THIS</size>" + "<br>";
            }
        }
    }


    /// <summary>
    /// This function is called when a button is pressed.
    /// It then adds the corresponding upgrade to the player.
    /// </summary>
    public void OnCardButtonPressed(int cardIdx)
    {
        if (ProgressionManager.UpgradeReady)
        {
            UpgradeManager.BindUpgrade(cardIdx);
            ProgressionManager.BuyUpgrade();
            Debug.Log("Weapon Upgrade");
        }
        else
        {
            Debug.Log("SHOULD NOT SEE THIS");
        }

        LevelManager.Continue();
    }
}