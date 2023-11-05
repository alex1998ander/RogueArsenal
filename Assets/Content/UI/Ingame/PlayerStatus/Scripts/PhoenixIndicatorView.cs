using UnityEngine;
using UnityEngine.UI;

public class PhoenixIndicatorView : MonoBehaviour
{

    [SerializeField] private Sprite indicatorEnabledSprite;
    [SerializeField] private Sprite indicatorDisabledSprite;
    
    private Image image;
    
    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void EnableIndicator()
    {
        image.sprite = indicatorEnabledSprite;
    }
    
    public void DisableIndicator()
    {
        image.sprite = indicatorDisabledSprite;
    }

    public void ShowIndicator()
    {
        enabled = true;
    }

    public void HideIndicator()
    {
        enabled = false;
    }
}
