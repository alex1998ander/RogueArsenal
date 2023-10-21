public class UpgradeDrill : Upgrade
{
    public override string Name => "Drill";
    public override string Description => "Break the laws of physics with bullets that defy solid matter, turning your enemies' hiding spots into mere illusions of safety.";
    public override string HelpfulDescription => "";

    public override void Init(IUpgradeableBullet upgradeableBullet)
    {
        upgradeableBullet.InitDrill();
    }
}