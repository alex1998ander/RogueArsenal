using System.Collections.Generic;
using UnityEngine;

public class IconGridView : MonoBehaviour
{
    [SerializeField] private RectTransform contentParent;
    [SerializeField] private IconView iconPrefab;
    

    [SerializeField] private UpgradeIcon[] upgradeIcons;

    public void InitializeUpgradeView(List<Upgrade> upgrades)
    {
        foreach (var upgrade in upgrades)
        {
            var upgradeIcon = Instantiate(iconPrefab, contentParent);

            foreach (var icon in upgradeIcons)
            {
                if (icon.upgradeIdentification == upgrade.UpgradeIdentification)
                {
                    InitializeUpgradeIconView(upgradeIcon, icon, upgrade);
                    break;
                }
            }
        }
    }

    protected virtual void InitializeUpgradeIconView(IconView iconView, UpgradeIcon upgradeIcon, Upgrade upgrade)
    {
        iconView.Initialize(upgradeIcon.icon, upgrade.Name);
    }
}