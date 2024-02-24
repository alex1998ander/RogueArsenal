using UnityEngine;
using UnityEngine.UI;

public class IndicatorView : MonoBehaviour
{
    [SerializeField] private Image indicatorImage;
    [SerializeField] private Sprite indicatorEnabledSprite;
    [SerializeField] private Sprite indicatorDisabledSprite;
    
    private Image image;
    
    public void EnableIndicator()
    {
        indicatorImage.sprite = indicatorEnabledSprite;
    }
    
    public void DisableIndicator()
    {
        indicatorImage.sprite = indicatorDisabledSprite;
    }

    public void ShowIndicator(bool indicatorEnabled)
    {
        indicatorImage.gameObject.SetActive(indicatorEnabled);
    }
}
