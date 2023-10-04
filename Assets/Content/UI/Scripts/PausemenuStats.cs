using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PausemenuStats : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI statText;
    [SerializeField] private TextMeshProUGUI abilitiesText;
    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        statText.text = "Max Health: " + PlayerController.GetMaxHealth();
        statText.text += "\nDamage: " + PlayerController.GetBulletDamage();
        statText.text += "\nMovement speed: " + PlayerController.GetPlayerMovementSpeed();
        abilitiesText.text = "";
        while (UpgradeManager.GetWeaponUpgradeAtIndex(i) != null)
        {
            abilitiesText.text += UpgradeManager.GetWeaponUpgradeAtIndex(i).Name + "\n";
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
