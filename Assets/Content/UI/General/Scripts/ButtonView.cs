using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonView : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Color hoverColor = new(0.9f,0.9f,0.9f);
    private Action _onClickAction;
    private Color _initialColor;

    private void Awake()
    {
        image.alphaHitTestMinimumThreshold = 0.1f;
        _initialColor = image.color;
    }

    public void Initialize(Action onClickAction)
    {
        _onClickAction = onClickAction;
    }

    public void OnHoverEnter()
    {
        image.color = hoverColor;
    }

    public void OnHoverExit()
    {
        image.color = _initialColor;
    }

    public void OnClick()
    {
        _onClickAction?.Invoke();
    }
}