using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeModifierView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI modifierTypeText;
    [SerializeField] private Image[] arrowsBottomToTop;
    [SerializeField] private Image modifierBackground;

    public void SetModificationLevel(ModificationLevel modificationLevel)
    {
        switch (modificationLevel)
        {
            case ModificationLevel.NegativeLow:
                arrowsBottomToTop[0].transform.localRotation = Quaternion.Euler(0f, 0f, -15f);

                arrowsBottomToTop[0].gameObject.SetActive(true);
                arrowsBottomToTop[1].gameObject.SetActive(false);
                arrowsBottomToTop[2].gameObject.SetActive(false);
                arrowsBottomToTop[3].gameObject.SetActive(false);
                break;
            case ModificationLevel.NegativeMedium:
                arrowsBottomToTop[0].transform.localRotation = Quaternion.Euler(0f, 0f, -15f);
                arrowsBottomToTop[1].transform.localRotation = Quaternion.Euler(0f, 0f, -15f);

                arrowsBottomToTop[0].gameObject.SetActive(true);
                arrowsBottomToTop[1].gameObject.SetActive(true);
                arrowsBottomToTop[2].gameObject.SetActive(false);
                arrowsBottomToTop[3].gameObject.SetActive(false);
                break;
            case ModificationLevel.NegativeHigh:
                arrowsBottomToTop[0].transform.localRotation = Quaternion.Euler(0f, 0f, -15f);
                arrowsBottomToTop[1].transform.localRotation = Quaternion.Euler(0f, 0f, -15f);
                arrowsBottomToTop[2].transform.localRotation = Quaternion.Euler(0f, 0f, -15f);

                arrowsBottomToTop[0].gameObject.SetActive(true);
                arrowsBottomToTop[1].gameObject.SetActive(true);
                arrowsBottomToTop[2].gameObject.SetActive(true);
                arrowsBottomToTop[3].gameObject.SetActive(false);
                break;
            case ModificationLevel.NegativeExtreme:
                arrowsBottomToTop[0].transform.localRotation = Quaternion.Euler(0f, 0f, -15f);
                arrowsBottomToTop[1].transform.localRotation = Quaternion.Euler(0f, 0f, -15f);
                arrowsBottomToTop[2].transform.localRotation = Quaternion.Euler(0f, 0f, -15f);
                arrowsBottomToTop[3].transform.localRotation = Quaternion.Euler(0f, 0f, -15f);

                arrowsBottomToTop[0].gameObject.SetActive(true);
                arrowsBottomToTop[1].gameObject.SetActive(true);
                arrowsBottomToTop[2].gameObject.SetActive(true);
                arrowsBottomToTop[3].gameObject.SetActive(true);
                break;
            case ModificationLevel.PositiveLow:
                arrowsBottomToTop[0].transform.localRotation = Quaternion.Euler(0f, 0f, 165f);

                arrowsBottomToTop[0].gameObject.SetActive(true);
                arrowsBottomToTop[1].gameObject.SetActive(false);
                arrowsBottomToTop[2].gameObject.SetActive(false);
                arrowsBottomToTop[3].gameObject.SetActive(false);
                break;
            case ModificationLevel.PositiveMedium:
                arrowsBottomToTop[0].transform.localRotation = Quaternion.Euler(0f, 0f, 165f);
                arrowsBottomToTop[1].transform.localRotation = Quaternion.Euler(0f, 0f, 165f);

                arrowsBottomToTop[0].gameObject.SetActive(true);
                arrowsBottomToTop[1].gameObject.SetActive(true);
                arrowsBottomToTop[2].gameObject.SetActive(false);
                arrowsBottomToTop[3].gameObject.SetActive(false);
                break;
            case ModificationLevel.PositiveHigh:
                arrowsBottomToTop[0].transform.localRotation = Quaternion.Euler(0f, 0f, 165f);
                arrowsBottomToTop[1].transform.localRotation = Quaternion.Euler(0f, 0f, 165f);
                arrowsBottomToTop[2].transform.localRotation = Quaternion.Euler(0f, 0f, 165f);

                arrowsBottomToTop[0].gameObject.SetActive(true);
                arrowsBottomToTop[1].gameObject.SetActive(true);
                arrowsBottomToTop[2].gameObject.SetActive(true);
                arrowsBottomToTop[3].gameObject.SetActive(false);
                break;
            case ModificationLevel.PositiveExtreme:
                arrowsBottomToTop[0].transform.localRotation = Quaternion.Euler(0f, 0f, 165f);
                arrowsBottomToTop[1].transform.localRotation = Quaternion.Euler(0f, 0f, 165f);
                arrowsBottomToTop[2].transform.localRotation = Quaternion.Euler(0f, 0f, 165f);
                arrowsBottomToTop[3].transform.localRotation = Quaternion.Euler(0f, 0f, 165f);

                arrowsBottomToTop[0].gameObject.SetActive(true);
                arrowsBottomToTop[1].gameObject.SetActive(true);
                arrowsBottomToTop[2].gameObject.SetActive(true);
                arrowsBottomToTop[3].gameObject.SetActive(true);
                break;
        }
    }

    public void ShowModifier(bool enabled)
    {
        gameObject.SetActive(enabled);
    }

    public void SetModifierName(string modifierName)
    {
        modifierTypeText.text = modifierName;
    }

    public void SetModifierColor(Color modifierColor)
    {
        modifierBackground.color = modifierColor;
        arrowsBottomToTop[0].color = modifierColor;
        arrowsBottomToTop[1].color = modifierColor;
        arrowsBottomToTop[2].color = modifierColor;
        arrowsBottomToTop[3].color = modifierColor;
    }
}