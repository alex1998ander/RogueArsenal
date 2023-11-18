using UnityEngine;

public class CurrencyBarView : MonoBehaviour
{
    private const float HealthBarAnglePositionOffset = 12f;

    private const float RelativeMinClampValue = -0.01f;
    private const float RelativeMaxClampValue = 1f;

    [SerializeField] private RectTransform currencyBarFill;

    private float currencyBarMaxValue;
    private float currencyBarValue;
    
    private void UpdateBar()
    {
        var relativeValueCurrencyBar = Mathf.Clamp(currencyBarValue / currencyBarMaxValue, RelativeMinClampValue, RelativeMaxClampValue);
        var absoluteReductionValueMainBar = (currencyBarFill.rect.width - HealthBarAnglePositionOffset) * (1f - relativeValueCurrencyBar);
        currencyBarFill.localPosition = new Vector3(-absoluteReductionValueMainBar, 0f, 0f);
    }
    
    public void SetMaxValue(float value)
    {
        currencyBarMaxValue = value;
    }

    public void SetValue(float value)
    {
        currencyBarValue = value;
        UpdateBar();
    }
    
    public void SetViewToDefault()
    {
        currencyBarValue = currencyBarMaxValue;
        UpdateBar();
    }
    
}
