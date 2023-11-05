public class UpgradeHitman : Upgrade
{
    public override string Name => "Hitman";
    public override string Description => "Break the sound barrier with bullets that leave enemies in awe and questioning their life choices.";
    public override string HelpfulDescription => "Bullet Range +250%\nBullet Speed +250%\nFire Delay +100%";

    public override float BulletRange => 2.5f;
    public override float BulletSpeed => 2.5f;
    public override float FireCooldown => 1.0f;
}