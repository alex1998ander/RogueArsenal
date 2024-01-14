using UnityEngine;

public static class ProgressionManager
{
    public static int DifficultyLevel { private set; get; }
    public static int CollectedCurrency { private set; get; }
    public static bool UpgradeReady { private set; get; }

    public static int CurrentUpgradePrice { get; private set; } = InitialUpgradePrice;
    private const int InitialUpgradePrice = 100;
    private const float NextUpgradePrinceInPercent = 1.15f;

    static ProgressionManager()
    {
        EventManager.OnMainMenuEnter.Subscribe(ResetProgression);
    }

    public static void CollectCurrency()
    {
        CollectedCurrency++;
        if (CollectedCurrency >= CurrentUpgradePrice)
        {
            UpgradeReady = true;
        }
    }

    public static void BuyUpgrade()
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

    public static void ResetProgression()
    {
        DifficultyLevel = 0;
        CollectedCurrency = 0;
        UpgradeReady = false;
        CurrentUpgradePrice = InitialUpgradePrice;
    }
}