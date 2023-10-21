public class UpgradeBigBullet : Upgrade
{
    public override string Name => "Big Bullet";
    public override string Description => "Because size matters, watch as your bullets look big and intimidating. Who needs modesty when you can have an ego as big as a cannonball?";
    public override string HelpfulDescription => "Bigger bullets\n\nBullet Size +100%";

    public override float BulletSize => 1f;
}