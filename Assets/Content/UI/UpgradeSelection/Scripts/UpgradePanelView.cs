using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanelView : MonoBehaviour
{
    private const string AbilityUpgradeTypeText = "Ability Upgrade";
    private const string PassiveUpgradeTypeText = "Passive Upgrade";
    private const string WeaponUpgradeTypeText = "Weapon Upgrade";

    private const string ModifierNameBulletDamage = "Damage";
    private const string ModifierNameFireCooldown = "Fire Rate";
    private const string ModifierNameHealth = "Health";
    private const string ModifierNameMagazineSize = "Magazine";
    private const string ModifierNameReloadTime = "Reload Time";
    private const string ModifierNameWeaponSpray = "Spray";
    private const string ModifierNameBulletSpeed = "Bullet Speed";
    private const string ModifierNameBulletRange = "Range";

    private readonly Color _modifierColorBulletDamage = new Color(1f, 0.33f, 0.33f);
    private readonly Color _modifierColorFireCooldown = new Color(0.83f,0.67f,0);
    private readonly Color _modifierColorHealth = new Color(0.44f,0.78f,0.22f);
    private readonly Color _modifierColorMagazineSize = new Color(0.8f,0.66f,1f);
    private readonly Color _modifierColorReloadTime = new Color(0.8f,0.87f,0.53f);
    private readonly Color _modifierColorWeaponSpray = new Color(0.83f,0.55f,0.37f);
    private readonly Color _modifierColorBulletSpeed = new Color(0.33f,0.87f,1f);
    private readonly Color _modifierColorBulletRange = new Color(1f,0.5f,0.9f);

    private const float ModificationLevelUpperThresholdPositiveLow = 0.2f;
    private const float ModificationLevelUpperThresholdPositiveMedium = 0.5f;
    private const float ModificationLevelUpperThresholdPositiveHigh = 1.0f;
    private const float ModificationLevelUpperThresholdNegativeLow = -0.2f;
    private const float ModificationLevelUpperThresholdNegativeMedium = -0.5f;
    private const float ModificationLevelUpperThresholdNegativeHigh =-0.75f;

    [SerializeField] private int id;
    
    [SerializeField] private UpgradeBanner[] upgradeBanners;
    [SerializeField] private UpgradeModifierView[] upgradeModifier;

    [SerializeField] private TextMeshProUGUI upgradeNameText;
    [SerializeField] private TextMeshProUGUI upgradeTypeText;
    [SerializeField] private TextMeshProUGUI upgradeDescriptionText;

    [SerializeField] private Image upgradeBanner;
    private Button _button;

    private void Awake()
    {
        GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
    }

    public void InitializeUpgradePanelView(Upgrade upgrade)
    {
        foreach (var banner in upgradeBanners)
        {
            if (banner.upgradeIdentification == upgrade.UpgradeIdentification)
            {
                upgradeBanner.sprite = banner.banner;
                break;
            }
        }

        upgradeNameText.text = upgrade.Name;
        upgradeDescriptionText.text = upgrade.Description;

        switch (upgrade.UpgradeType)
        {
            case UpgradeType.Ability:
                upgradeTypeText.text = AbilityUpgradeTypeText;
                break;
            case UpgradeType.Passive:
                upgradeTypeText.text = PassiveUpgradeTypeText;
                break;
            case UpgradeType.Weapon:
                upgradeTypeText.text = WeaponUpgradeTypeText;
                break;
        }

        var modifierIndex = 0;

        InitializeModifiers(upgrade, ref modifierIndex);

        switch (modifierIndex)
        {
            case 0:
                upgradeModifier[0].ShowModifier(false);
                upgradeModifier[1].ShowModifier(false);
                upgradeModifier[2].ShowModifier(false);
                break;
            case 1:
                upgradeModifier[0].ShowModifier(true);
                upgradeModifier[1].ShowModifier(false);
                upgradeModifier[2].ShowModifier(false);
                break;
            case 2:
                upgradeModifier[0].ShowModifier(true);
                upgradeModifier[1].ShowModifier(true);
                upgradeModifier[2].ShowModifier(false);
                break;
            case 3:
                upgradeModifier[0].ShowModifier(true);
                upgradeModifier[1].ShowModifier(true);
                upgradeModifier[2].ShowModifier(true);
                break;
            default:
                throw new InvalidOperationException();
        }
    }

    private void InitializeModifiers(Upgrade upgrade, ref int modifierIndex)
    {
        if (SetModificationLevel((1 + upgrade.BulletDamage) * (upgrade.BulletCount + 1) - 1, ref modifierIndex, ModifierNameBulletDamage, _modifierColorBulletDamage)) return;
        if (SetModificationLevel(-upgrade.FireCooldown, ref modifierIndex, ModifierNameFireCooldown, _modifierColorFireCooldown)) return;
        if (SetModificationLevel(upgrade.Health, ref modifierIndex, ModifierNameHealth, _modifierColorHealth)) return;
        if (SetModificationLevel(upgrade.MagazineSize, ref modifierIndex, ModifierNameMagazineSize, _modifierColorMagazineSize)) return;
        if (SetModificationLevel(upgrade.ReloadTime, ref modifierIndex, ModifierNameReloadTime, _modifierColorReloadTime)) return;
        if (SetModificationLevel(upgrade.WeaponSpray, ref modifierIndex, ModifierNameWeaponSpray, _modifierColorWeaponSpray)) return;
        if (SetModificationLevel(upgrade.BulletSpeed, ref modifierIndex, ModifierNameBulletSpeed, _modifierColorBulletSpeed)) return;
        SetModificationLevel(upgrade.BulletRange, ref modifierIndex, ModifierNameBulletRange, _modifierColorBulletRange);
    }

    private bool SetModificationLevel(float upgradeModificationValue, ref int modifierIndex, string modifierName, Color modifierColor)
    {
        if (upgradeModificationValue != 0f)
        {
            upgradeModifier[modifierIndex].SetModificationLevel(DetermineModificationLevel(upgradeModificationValue));
            upgradeModifier[modifierIndex].SetModifierName(modifierName);
            upgradeModifier[modifierIndex].SetModifierColor(modifierColor);

            modifierIndex++;
        }

        return modifierIndex >= 3;
    }

    private ModificationLevel DetermineModificationLevel(float upgradeModificationValue)
    {
        return upgradeModificationValue switch
        {
            < ModificationLevelUpperThresholdNegativeHigh => ModificationLevel.NegativeExtreme,
            < ModificationLevelUpperThresholdNegativeMedium => ModificationLevel.NegativeHigh,
            < ModificationLevelUpperThresholdNegativeLow => ModificationLevel.NegativeMedium,
            < 0f => ModificationLevel.NegativeLow,
            > ModificationLevelUpperThresholdPositiveHigh => ModificationLevel.PositiveExtreme,
            > ModificationLevelUpperThresholdPositiveMedium => ModificationLevel.PositiveHigh,
            > ModificationLevelUpperThresholdPositiveLow => ModificationLevel.PositiveMedium,
            > 0f => ModificationLevel.PositiveLow,
            _ => throw new InvalidOperationException()
        };
    }

    public void OnUpgradeHoverEnter()
    {
    }

    public void OnUpgradeHoverExit()
    {
    }

    public void OnUpgradeClick()
    {
        UpgradeManager.BindUpgrade(id);
    }
}

[System.Serializable]
public struct UpgradeBanner
{
    public Sprite banner;
    public UpgradeIdentification upgradeIdentification;
}