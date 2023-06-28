using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using System;

public class UpgradeChoosing : MonoBehaviour
{   
    //Array of the text fields
    [SerializeField] private TextMeshProUGUI[] upgradeDescription;
    //Array of the possible upgrades
    Upgrade[] _listOfUpgrades = new Upgrade[5];

    // Start is called before the first frame update
    void Start()
    {
        _listOfUpgrades = UpgradeManager.GenerateNewRandomUpgradeSelection();
        
        //Adds the description to the cards
        for (int counter = 0; counter < upgradeDescription.Length; counter++)
        {
            upgradeDescription[counter].text = "<size=24>" + _listOfUpgrades[counter].Name + "</size>" + "<br>" +
                                               "<br>" + _listOfUpgrades[counter].Description;
        }
    }

    /// <summary>
    /// This function is called when a button is pressed.
    /// It then adds the corresponding upgrade to the player.
    /// </summary>
    public void AddUpgrade()
    {
        string clickedButtonName = EventSystem.current.currentSelectedGameObject.name;
        int i = (int) Char.GetNumericValue(clickedButtonName[5]) - 1;
        Debug.Log(i);
        UpgradeManager.BindUpgrade(i);
    }
}