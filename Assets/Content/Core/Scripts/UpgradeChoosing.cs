using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
//using Random = System.Random;
using Random = UnityEngine.Random;

public class UpgradeChoosing : MonoBehaviour
{   
    //Array of the text fields
    [SerializeField] private TextMeshProUGUI[] upgradeDescription;
    //Array of the possible upgrades
    Upgrade[] _listOfWeaponUpgrades = new Upgrade[3];
    List <Upgrade> _listOfAbilityUpgrades = new List<Upgrade>();
    private String[] _listOfStatUpgradeNames = new string[4] {"Health", "Bullet Knockback", "Player Speed", "Projectile Damage"};
    private StatUpgrade[] _listOfStatUpgrades = new StatUpgrade[4] {UpgradeManager.MaxHealthIncrease, UpgradeManager.BulletKnockbackIncrease, UpgradeManager.PlayerMovementSpeedIncrease, UpgradeManager.BulletDamageIncrease };

    // Start is called before the first frame update
    void Start()
    {
        if (SceneChange.stageCount % 5 == 0)
        {
            int weaponUpgradesAmount = Random.Range(1, 4);
            //_listOfWeaponUpgrades.add
            _listOfWeaponUpgrades = UpgradeManager.GenerateNewRandomWeaponUpgradeSelection(3);
            //Adds the description to the cards
            for (int counter = 0; counter < upgradeDescription.Length; counter++)
            {
                upgradeDescription[counter].text = "<size=24>" + _listOfWeaponUpgrades[counter].Name + "</size>" + "<br>" +
                                                   "<br>" + _listOfWeaponUpgrades[counter].Description;
            }
        }
        else
        {
            for (int counter = 0; counter < upgradeDescription.Length; counter++)
            {
                upgradeDescription[counter].text = "<size=24>" + _listOfStatUpgradeNames[counter] + "</size>" + "<br>";
            }
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
        if (SceneChange.stageCount % 5 == 0)
        {
            UpgradeManager.BindWeaponUpgrade(i);
            Debug.Log("Weapon Upgrade");
        }
        else
        {
            _listOfStatUpgrades[i].Upgrade();
            Debug.Log("Upgraded Stat");
        }
        
    }
}