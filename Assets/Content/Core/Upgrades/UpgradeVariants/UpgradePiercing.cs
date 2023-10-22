using UnityEngine;

public class UpgradePiercing : Upgrade
{
    public override string Name => "Piercing";
    public override string Description => "";
    public override string HelpfulDescription => "";

    public override bool OnBulletImpact(PlayerBullet playerBullet, Collider2D other)
    {
        if (playerBullet.PiercesLeft <= 0 || (!other.CompareTag("Enemy") && !other.CompareTag("Player")))
            return false;

        playerBullet.PiercesLeft--;
        return true;
    }
}