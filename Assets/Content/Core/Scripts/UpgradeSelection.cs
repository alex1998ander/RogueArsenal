using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;
using Cinemachine;

public class UpgradeSelection : MonoBehaviour
{
    //Array of the text fields
    [SerializeField] private TextMeshProUGUI[] upgradeDescription;

    [SerializeField] private int upgradeLevelCounter = 3;

    List<Upgrade> _listOfAbilityUpgrades = new List<Upgrade>();

    private StatUpgrade[] _listOfStatUpgrades = new StatUpgrade[4]
    {
        UpgradeManager.MaxHealthIncrease, UpgradeManager.BulletDamageIncrease,
        UpgradeManager.PlayerMovementSpeedIncrease, UpgradeManager.BulletKnockbackIncrease
    };

    void Start()
    {
        SetupUpgradeInfo();
    }

    /// <summary>
    /// Sets up the information shown on the cards used for upgrade selection.
    /// </summary>
    private void SetupUpgradeInfo()
    {
        if (LevelManager.levelCounter % upgradeLevelCounter == 0)
        {
            WeaponUpgrade[] listOfWeaponUpgrades = UpgradeManager.GenerateNewRandomWeaponUpgradeSelection(3);

            // Adds the description to the cards
            for (int counter = 0; counter < upgradeDescription.Length; counter++)
            {
                upgradeDescription[counter].text =
                    "<size=24>" + listOfWeaponUpgrades[counter].Name + "</size>"  + "<br>" +
                    "<br>" + "<size=12>" + listOfWeaponUpgrades[counter].HelpfulDescription + "</size>";
            }
        }
        else
        {
            for (int counter = 0; counter < upgradeDescription.Length; counter++)
            {
                upgradeDescription[counter].text = "<size=24>" + _listOfStatUpgrades[counter].Name + "</size>" + "<br>";
            }
        }
    }


    /// <summary>
    /// This function is called when a button is pressed.
    /// It then adds the corresponding upgrade to the player.
    /// </summary>
    public void OnCardButtonPressed(int cardIdx)
    {
        if (LevelManager.levelCounter % upgradeLevelCounter == 0)
        {
            UpgradeManager.BindWeaponUpgrade(cardIdx);
            Debug.Log("Weapon Upgrade");
        }
        else
        {
            _listOfStatUpgrades[cardIdx].Upgrade();
            Debug.Log("Upgraded Stat");
        }

        LevelManager.LoadNextLevel();
    }
}