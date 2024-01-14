using System;
using UnityEngine;
using UnityEngine.UI;

public class ToggleView : MonoBehaviour
{
    [SerializeField] private Image toggleIcon;
    [SerializeField] private Sprite toggleOn;
    [SerializeField] private Sprite toggleOff;
    
    public bool Enabled { get; private set; }
    private Action<bool> _onToggleAction;

    private void Awake()
    {
        SetToggleIconSprite();
        toggleIcon.alphaHitTestMinimumThreshold = 0.1f;
    }

    public void Initialize(Action<bool> onToggleAction, bool initialBool = false)
    {
        _onToggleAction = onToggleAction;
        Enabled = initialBool;
        SetToggleIconSprite();
    }

    public void OnHoverEnter()
    {
        toggleIcon.color = new Color(0.9f, 0.9f, 0.9f);
    }

    public void OnHoverExit()
    {
        toggleIcon.color = new Color(1f, 1f, 1f);
    }

    public void OnClick()
    {
        Enabled = !Enabled;
        _onToggleAction?.Invoke(Enabled);
        
        SetToggleIconSprite();
    }

    private void SetToggleIconSprite()
    {
        toggleIcon.sprite = Enabled ? toggleOn : toggleOff;
    }
}