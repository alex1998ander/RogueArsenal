using System;
using System.Collections;
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
        if (SetModificationLevel((1 + upgrade.BulletDamage) * (upgrade.BulletCount + 1) - 1, ref modifierIndex, ModifierNameBulletDamage)) return;
        if (SetModificationLevel(-upgrade.FireCooldown, ref modifierIndex, ModifierNameFireCooldown)) return;
        if (SetModificationLevel(upgrade.Health, ref modifierIndex, ModifierNameHealth)) return;
        if (SetModificationLevel(upgrade.MagazineSize, ref modifierIndex, ModifierNameMagazineSize)) return;
        if (SetModificationLevel(upgrade.ReloadTime, ref modifierIndex, ModifierNameReloadTime)) return;
        if (SetModificationLevel(upgrade.WeaponSpray, ref modifierIndex, ModifierNameWeaponSpray)) return;
        if (SetModificationLevel(upgrade.BulletSpeed, ref modifierIndex, ModifierNameBulletSpeed)) return;
        SetModificationLevel(upgrade.BulletRange, ref modifierIndex, ModifierNameBulletRange);
    }

    private bool SetModificationLevel(float upgradeModificationValue, ref int modifierIndex, string modifierName)
    {
        if (upgradeModificationValue != 0f)
        {
            upgradeModifier[modifierIndex].SetModificationLevel(DetermineModificationLevel(upgradeModificationValue));
            upgradeModifier[modifierIndex].SetModifierName(modifierName);

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
        StartCoroutine(FadeScale(1.02f, 0.1f));
    }

    public void OnUpgradeHoverExit()
    {
        StartCoroutine(FadeScale(1f, 0.1f));
    }
    
    private IEnumerator FadeScale(float targetScale, float duration)
    {
        var time = 0f;
        var initialScale = transform.localScale.x;

        while (time < duration)
        {
            var scale = Mathf.Lerp(initialScale, targetScale, time / duration);

            transform.localScale = new Vector3(scale, scale, scale);

            time += Time.unscaledDeltaTime;
            yield return null;
        }
    }

    public void OnUpgradeClick()
    {
        UpgradeManager.BindUpgrade(id);
        ProgressionManager.BuyUpgrade();
        LevelManager.Continue();
    }
}

[System.Serializable]
public struct UpgradeBanner
{
    public Sprite banner;
    public UpgradeIdentification upgradeIdentification;
}