using UnityEngine;

public class UpgradePiercing : Upgrade
{
    public override string Name => "Piercing";
    public override string Description => "";
    public override string HelpfulDescription => "";

    public override void Init(PlayerBullet playerBullet)
    {
        playerBullet.PiercesLeft += Configuration.Piercing_PiercesCount;
    }

    public override bool OnBulletTrigger(PlayerBullet playerBullet, Collider2D other)
    {
        if (playerBullet.PiercesLeft <= 0 || (!other.CompareTag("Enemy") && !other.CompareTag("Player")))
            return false;

        playerBullet.PiercesLeft--;
        return true;
    }
}