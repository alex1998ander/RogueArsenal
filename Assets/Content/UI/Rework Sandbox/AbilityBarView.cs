using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class AbilityBarView : MonoBehaviour
{
    [SerializeField] private Image abilityBarFill;

    [SerializeField] private float currencyBarMaxValue;
    [SerializeField] private float currencyBarValue;
    
    private void Update()
    {
        abilityBarFill.fillAmount = currencyBarValue / currencyBarMaxValue;
    }
}
