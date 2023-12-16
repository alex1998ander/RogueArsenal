using UnityEngine;

public static class ProgressionManager
{
    public static int DifficultyLevel { private set; get; }
    public static int CollectedCurrency { private set; get; }
    public static bool UpgradeReady { private set; get; }

    public static int CurrentUpgradePrice { get; private set; }= 100;
    private const float NextUpgradePrinceInPercent = 1.05f;

    public static void CollectCurrency()
    {
        CollectedCurrency++;
        if (CollectedCurrency >= CurrentUpgradePrice)
        {
            UpgradeReady = true;
        }
    }

    public static void SetupNextUpgrade()
    {
        CollectedCurrency -= CurrentUpgradePrice;
        CurrentUpgradePrice = Mathf.RoundToInt(CurrentUpgradePrice * NextUpgradePrinceInPercent);
        Debug.Log("Next Upgrade Price: " + CurrentUpgradePrice);
        UpgradeReady = false;
    }

    public static void IncreaseDifficultyLevel()
    {
        DifficultyLevel++;
    }
}