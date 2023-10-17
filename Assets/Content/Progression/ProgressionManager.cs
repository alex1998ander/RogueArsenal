using UnityEngine;

public static class ProgressionManager
{
    public static int DifficultyLevel { private set; get; }
    public static int CollectedCurrency { private set; get; }
    public static bool UpgradeReady { private set; get; }

    private static int _upgradePrice = 100;
    private const float UpgradePrinceIncreaseInPercent = 0.2f;

    public static void CollectCurrency()
    {
        CollectedCurrency++;
        if (CollectedCurrency >= _upgradePrice)
        {
            UpgradeReady = true;
        }
    }

    public static void SetupNextUpgrade()
    {
        _upgradePrice += Mathf.RoundToInt(_upgradePrice * UpgradePrinceIncreaseInPercent);
        UpgradeReady = false;
    }
}