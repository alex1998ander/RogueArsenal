using System;
using TMPro;
using UnityEngine;

public class StringButtonView : ButtonView
{
    
    [SerializeField] private TextMeshProUGUI text;
    
    public void Initialize(Action onClickAction, string text)
    {
        base.Initialize(onClickAction);
        this.text.text = text;
    }
    
}