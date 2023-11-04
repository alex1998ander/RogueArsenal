using System.Collections;
using System.Globalization;
using TMPro;
using UnityEngine;

public class AmmoBarView : MonoBehaviour
{
    private const string ReloadText = "RELOADING";

    private const float HealthBarAnglePositionOffset = 100f;

    private const float RelativeMinClampValue = -0.01f;
    private const float RelativeMaxClampValue = 1f;

    [SerializeField] private RectTransform ammoBarFill;
    [SerializeField] private TextMeshProUGUI ammoBarText;

    private float ammoBarMaxValue;
    private float ammoBarValue;
    private float ammoBarReloadTime;

    private bool reloading;

    private void UpdateBarValue()
    {
        var relativeValueCurrencyBar = Mathf.Clamp(ammoBarValue / ammoBarMaxValue, RelativeMinClampValue, RelativeMaxClampValue);
        var absoluteReductionValueMainBar = (ammoBarFill.rect.width - HealthBarAnglePositionOffset) * (1f - relativeValueCurrencyBar);
        ammoBarFill.localPosition = new Vector3(-absoluteReductionValueMainBar, 0f, 0f);
    }

    private void UpdateBarText()
    {
        ammoBarText.text = reloading ? ReloadText : ammoBarValue.ToString(CultureInfo.CurrentCulture);
    }

    private IEnumerator ReloadBar()
    {
        while (ammoBarValue < ammoBarMaxValue)
        {
            ammoBarValue += ammoBarMaxValue / ammoBarReloadTime * Time.deltaTime;
            UpdateBarValue();
            yield return null;
        }

        ammoBarValue = ammoBarMaxValue;
        reloading = false;
        UpdateBarText();
    }

    public void SetReloadTime(float seconds)
    {
        ammoBarReloadTime = seconds;
    }

    public void SetMaxValue(float value)
    {
        ammoBarMaxValue = value;
    }

    public void SetValue(float value)
    {
        reloading = false;

        ammoBarValue = value;
        UpdateBarValue();
        UpdateBarText();
    }

    public void StartReloadBar()
    {
        reloading = true;
        UpdateBarText();
        StartCoroutine(ReloadBar());
    }
}