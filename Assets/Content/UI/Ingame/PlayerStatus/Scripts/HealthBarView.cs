using UnityEngine;

public class HealthBarView : MonoBehaviour
{
    private const float ExtensionBarEnableOffsetValue = Configuration.Player_MaxHealth * 0.1f;
    private const float HealthBarAnglePositionOffset = 48f;

    private const float RelativeMinClampValue = -0.01f;
    private const float RelativeMaxClampValue = 1f;

    [SerializeField] private RectTransform mainBarMaxValueFill;
    [SerializeField] private RectTransform mainBarFill;

    [SerializeField] private GameObject extensionBar;
    [SerializeField] private RectTransform extensionBarMaxValueFill;
    [SerializeField] private RectTransform extensionBarFill;

    [SerializeField] private RectTransform border;

    [SerializeField] private float healthBarMaxValue;
    [SerializeField] private float healthBarValue;

    private void UpdateBarMaxValue()
    {
        if (healthBarMaxValue > Configuration.Player_MaxHealth + ExtensionBarEnableOffsetValue)
        {
            extensionBar.SetActive(true);
            var relativeValueExtensionBar = Mathf.Clamp(healthBarMaxValue / Configuration.Player_MaxHealth - 1, RelativeMinClampValue, RelativeMaxClampValue);

            var rect = extensionBarMaxValueFill.rect;
            var absoluteReductionValueExtensionBar = (rect.width - HealthBarAnglePositionOffset) * (1f - relativeValueExtensionBar);
            extensionBarMaxValueFill.localPosition = new Vector3(-absoluteReductionValueExtensionBar, 0f, 0f);

            border.localPosition = new Vector3(extensionBar.transform.localPosition.x + rect.width - absoluteReductionValueExtensionBar, 0f, 0f);
        }
        else
        {
            extensionBar.SetActive(false);
            var relativeValueMainBar = Mathf.Clamp(healthBarMaxValue / Configuration.Player_MaxHealth, RelativeMinClampValue, RelativeMaxClampValue);

            var rect = mainBarMaxValueFill.rect;
            var absoluteReductionValueMainBar = (rect.width - HealthBarAnglePositionOffset) * (1f - relativeValueMainBar);
            mainBarMaxValueFill.localPosition = new Vector3(-absoluteReductionValueMainBar, 0f, 0f);

            border.localPosition = new Vector3(rect.width - absoluteReductionValueMainBar, 0f, 0f);
        }
    }

    private void UpdateBar()
    {
        var relativeValueExtensionBar = Mathf.Clamp(healthBarValue / Configuration.Player_MaxHealth - 1f, RelativeMinClampValue, RelativeMaxClampValue);
        var absoluteReductionValueExtensionBar = (extensionBarFill.rect.width - HealthBarAnglePositionOffset) * (1f - relativeValueExtensionBar) + extensionBarMaxValueFill.localPosition.x;
        extensionBarFill.localPosition = new Vector3(-absoluteReductionValueExtensionBar, 0f, 0f);

        var relativeValueMainBar = Mathf.Clamp(healthBarValue / Configuration.Player_MaxHealth, RelativeMinClampValue, RelativeMaxClampValue);
        var absoluteReductionValueMainBar = (mainBarFill.rect.width - HealthBarAnglePositionOffset) * (1f - relativeValueMainBar) + mainBarMaxValueFill.localPosition.x;
        mainBarFill.localPosition = new Vector3(-absoluteReductionValueMainBar, 0f, 0f);
    }
    
    public void SetMaxValue(float value)
    {
        healthBarMaxValue = value;
        UpdateBarMaxValue();
    }

    public void SetValue(float value)
    {
        healthBarValue = value;
        UpdateBar();
    }
}