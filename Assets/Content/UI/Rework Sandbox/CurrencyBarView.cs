using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CurrencyBarView : MonoBehaviour
{
    private const float HealthBarAnglePositionOffset = 12f;

    private const float RelativeMinClampValue = -0.01f;
    private const float RelativeMaxClampValue = 1f;

    [SerializeField] private RectTransform currencyBarFill;

    [SerializeField] private float currencyBarMaxValue;
    [SerializeField] private float currencyBarValue;
    
    private void Update()
    {
        var relativeValueCurrencyBar = Mathf.Clamp(currencyBarValue / currencyBarMaxValue, RelativeMinClampValue, RelativeMaxClampValue);
        var absoluteReductionValueMainBar = (currencyBarFill.rect.width - HealthBarAnglePositionOffset) * (1f - relativeValueCurrencyBar);
        currencyBarFill.localPosition = new Vector3(-absoluteReductionValueMainBar, 0f, 0f);
    }
}
