using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class UpgradeDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI UpgradeDisplayText;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            for (int i = 0; i < 5; i++)
            {
                UpgradeDisplayText.text += UpgradeManager.GetUpgradeAtIndex(i).Name + "    ";
            }
        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            UpgradeDisplayText.text = "";
        }
    }

    
}
