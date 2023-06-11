using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class UpgradeChoosing : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] upgradeDescription;


    Upgrade[] _listOfUpgrades = new Upgrade[3];

    // Start is called before the first frame update
    void Start()
    {
        UpgradeBurst burst = new UpgradeBurst();
        UpgradeBounce bounce = new UpgradeBounce();
        UpgradeBuckshot buckshot = new UpgradeBuckshot();
        _listOfUpgrades[0] = buckshot;
        _listOfUpgrades[1] = burst;
        _listOfUpgrades[2] = bounce;
        for (int counter = 0; counter < upgradeDescription.Length; counter++)
        {
            upgradeDescription[counter].text = "<size=24>" + _listOfUpgrades[counter].Name + "</size>" + "<br>" +
                                                "<br>" + _listOfUpgrades[counter].Description;
        }
    }

    public void AddUpgrade()
    {
        string clickedButtonName = EventSystem.current.currentSelectedGameObject.name;
        int i = (int)clickedButtonName[5];
        UpgradeManager.BindUpgrade(_listOfUpgrades[i]);
    }
}