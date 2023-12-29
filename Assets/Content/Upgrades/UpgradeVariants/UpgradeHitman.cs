public class UpgradeHitman : Upgrade
{
    public override string Name => "Hitman";
    public override UpgradeIdentification UpgradeIdentification => UpgradeIdentification.Hitman;
    public override UpgradeType UpgradeType => UpgradeType.Weapon;
    public override string FlavorText => "Break the sound barrier with bullets that leave enemies in awe and questioning their life choices.";
    public override string Description => "Increased range and bullet speed but slower reload";

    public override float BulletRange => 2.5f;
    public override float BulletSpeed => 2.5f;
    public override float FireCooldown => 1.0f;
}