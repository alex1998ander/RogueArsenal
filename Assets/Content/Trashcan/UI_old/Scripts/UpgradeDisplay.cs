using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class UpgradeDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI UpgradeDisplayText;

    private int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            while (UpgradeManager.GetWeaponUpgradeAtIndex(i) != null)
            {
                UpgradeDisplayText.text += UpgradeManager.GetWeaponUpgradeAtIndex(i).Name + "    ";
                i++;
            }
                
            
        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            UpgradeDisplayText.text = "";
            i = 0;
        }
    }

    
}
