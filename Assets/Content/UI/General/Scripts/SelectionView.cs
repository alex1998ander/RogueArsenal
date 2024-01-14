using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectionView : MonoBehaviour
{
    [SerializeField] private Image leftArrowIcon;
    [SerializeField] private Image rightArrowIcon;
    [SerializeField] private TextMeshProUGUI optionText;

    public int SelectedOptionIndex { get; private set; }
    private List<string> _options;
    private Action<int> _onChangeOption;

    private void Awake()
    {
        leftArrowIcon.alphaHitTestMinimumThreshold = 0.1f;
        rightArrowIcon.alphaHitTestMinimumThreshold = 0.1f;
    }

    public void Initialize(List<string> options, int selectedOptionIndex, Action<int> onChangeOption)
    {
        _options = options;
        SelectedOptionIndex = selectedOptionIndex;
        _onChangeOption = onChangeOption;
        
        UpdateLabel();
    }

    public void OnLeftArrowHoverEnter()
    {
        leftArrowIcon.color = new Color(0.9f, 0.9f, 0.9f);
    }

    public void OnLeftArrowHoverExit()
    {
        leftArrowIcon.color = new Color(1f, 1f, 1f);
    }

    public void OnRightArrowHoverEnter()
    {
        rightArrowIcon.color = new Color(0.9f, 0.9f, 0.9f);
    }

    public void OnRightArrowHoverExit()
    {
        rightArrowIcon.color = new Color(1f, 1f, 1f);
    }

    public void OnLeftArrowClick()
    {
        SelectedOptionIndex--;
        
        if (SelectedOptionIndex < 0)
        {
            SelectedOptionIndex = 0;
        }

        _onChangeOption?.Invoke(SelectedOptionIndex);
        
        UpdateLabel();
    }

    public void OnRightArrowClick()
    {
        SelectedOptionIndex++;
        
        if (SelectedOptionIndex > _options.Count - 1)
        {
            SelectedOptionIndex = _options.Count - 1;
        }

        _onChangeOption?.Invoke(SelectedOptionIndex);
        
        UpdateLabel();
    }

    private void UpdateLabel()
    {
        optionText.text = _options[SelectedOptionIndex];
    }
}