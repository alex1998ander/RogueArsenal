using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IconView : MonoBehaviour
{

    [SerializeField] protected Image upgradeIcon;
    [SerializeField] protected TextMeshProUGUI upgradeName;

    public void Initialize(Sprite sprite, string name)
    {
        upgradeIcon.sprite = sprite;
        upgradeName.text = name;
    }

}
