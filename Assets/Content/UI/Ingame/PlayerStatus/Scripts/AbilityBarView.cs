using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AbilityBarView : MonoBehaviour
{
    [SerializeField] private Image abilityBarFill;

    private float abilityBarMaxValue;
    private float abilityBarValue;
    
    private IEnumerator ReloadBar()
    {
        while (abilityBarValue < abilityBarMaxValue)
        {
            abilityBarValue += Time.deltaTime;
            abilityBarFill.fillAmount = abilityBarValue / abilityBarMaxValue;
            yield return null;
        }
    }

    public void SetReloadTime(float seconds)
    {
        abilityBarMaxValue = seconds;
    }
    
    public void EmptyAndReloadBar()
    {
        abilityBarValue = 0f;
        abilityBarFill.fillAmount = 0f;
        StartCoroutine(ReloadBar());
    }
    
    public void SetViewToDefault()
    {
        abilityBarValue = abilityBarMaxValue;
        abilityBarFill.fillAmount = 1f;
    }
}
