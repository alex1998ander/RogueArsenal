public class UpgradeBuckshot : Upgrade
{
    public override string Name => "Buckshot";
    public override string Description => "Unleash a shotgun-inspired impact that scatters enemies like confetti.";
    public override string HelpfulDescription => "Adds a shotgun vibe\n\nBullet Range -50%\nBullet Count +4\nBullet Damage -60%\nFire Delay +200%";

    public override int BulletCount => 4;
    public override float BulletDamage => -0.6f;
    public override float BulletRange => -0.5f;
    public override float FireCooldown => 2.0f;
    public override float WeaponSpray => 4f;
}