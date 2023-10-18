using UnityEngine;

public static class ProgressionManager
{
    public static int DifficultyLevel { private set; get; }
    public static int CollectedCurrency { private set; get; }
    public static bool UpgradeReady { private set; get; }

    private static int _upgradePrice = 100;
    private const float NextUpgradePrinceInPercent = 1.05f;

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
        CollectedCurrency -= _upgradePrice;
        _upgradePrice = Mathf.RoundToInt(_upgradePrice * NextUpgradePrinceInPercent);
        Debug.Log("Next Upgrade Price: " + _upgradePrice);
        UpgradeReady = false;
    }

    public static void IncreaseDifficultyLevel()
    {
        DifficultyLevel++;
    }
}