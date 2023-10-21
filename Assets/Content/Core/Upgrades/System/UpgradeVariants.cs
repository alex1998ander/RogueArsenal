using UnityEngine;

/*

public class Upgrade : Upgrade
{
    public override string Name => "";
    public override string Description => "";
    public override string HelpfulDescription => "";

}

*/

public class UpgradeBigBullet : Upgrade
{
    public override string Name => "Big Bullet";
    public override string Description => "Because size matters, watch as your bullets look big and intimidating. Who needs modesty when you can have an ego as big as a cannonball?";
    public override string HelpfulDescription => "Bigger bullets\n\nBullet Size +100%";

    public override float BulletSize => 1f;
}

public class UpgradeBounce : Upgrade
{
    public override string Name => "Bounce";
    public override string Description => "Inject your bullets with enthusiasm, turning your attacks into a lively pinball game.";
    public override string HelpfulDescription => "Bullets bounce off of walls\n\nBullet Damage +25%";

    public override float BulletDamage => 0.25f;

    public override void Init(IUpgradeableBullet upgradeableBullet)
    {
        upgradeableBullet.InitBounce();
    }

    public override bool OnBulletImpact(IUpgradeableBullet upgradeableBullet, Collision2D collision)
    {
        return upgradeableBullet.ExecuteBounce_OnBulletImpact(collision);
    }
}

public class UpgradeBuckshot : Upgrade
{
    public override string Name => "Buckshot";
    public override string Description => "Unleash a shotgun-inspired impact that scatters enemies like confetti.";
    public override string HelpfulDescription => "Adds a shotgun vibe\n\nBullet Range -50%\nBullet Count +4\nBullet Damage -60%\nFire Delay +200%";

    public override int BulletCount => 4;
    public override float BulletDamage => -0.6f;
    public override float BulletRange => -0.5f;
    public override float FireDelay => 2.0f;
    public override float WeaponSpray => 4f;
}

public class UpgradeBurst : Upgrade
{
    public override string Name => "Burst";
    public override string Description => "Trade the single-shot snooze for a burst of pew-pew-pew and turn your enemies into a walking target.";
    public override string HelpfulDescription => "Multiple bullets are fired in a sequence\n\nBullet Damage -60%\nFire Delay +100%";

    public override float BulletDamage => -0.6f;
    public override float FireDelay => 1f;

    public override void OnFire(IUpgradeablePlayer upgradeablePlayer)
    {
        upgradeablePlayer.ExecuteBurst_OnFire();
    }
}

public class UpgradeCarefulPlanning : Upgrade
{
    public override string Name => "Careful Planning";
    public override string Description => "Embrace the spirit of meticulous plotting, trading rapid-fire chaos for jaw-dropping destruction.";
    public override string HelpfulDescription => "Bullet Damage +150%\nFire Delay +200%";

    public override float BulletDamage => 1.5f;
    public override float FireDelay => 2f;
}

public class UpgradeDemonicPact : Upgrade
{
    public override string Name => "Demonic Pact";
    public override string Description => "Embrace the dark arts of bullet wizardry and trade a bit of your life essence for instant trigger happiness.";
    public override string HelpfulDescription => "Shooting costs 10HP\nRemoves shooting cooldown";

    public override void OnFire(IUpgradeablePlayer upgradeablePlayer)
    {
        upgradeablePlayer.ExecuteDemonicPact_OnFire();
    }
}

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

public class UpgradeExplosiveBullet : Upgrade
{
    public override string Name => "Explosive Bullet";
    public override string Description => "Arm yourself with these explosive delights, turning your bullets into cheeky troublemakers that go 'boom' upon impact.";
    public override string HelpfulDescription => "Bullet explodes on impact\n\nFire Delay +100%";

    public override float FireDelay => 1f;

    public override bool OnBulletImpact(IUpgradeableBullet upgradeableBullet, Collision2D collision)
    {
        return upgradeableBullet.ExecuteExplosiveBullet_OnBulletImpact(collision);
    }
}

public class UpgradeGlassCannon : Upgrade
{
    public override string Name => "Glass Cannon";
    public override string Description => "Deal devastating damage to your enemies, but be warned: A mere sneeze could knock you out.";
    public override string HelpfulDescription => "Bullet Damage +300%\nHealth -95%";

    public override float BulletDamage => 3f;
    public override float Health => -0.95f;
}

public class UpgradeHealingField : Upgrade
{
    public override string Name => "Healing Field";
    public override string Description => "Transform the battlefield into a spa-like oasis of mending with a field of rejuvenation that magically patches up your injuries.";
    public override string HelpfulDescription => "Right click creates a healing field\n\nHealth +30%";

    public override float Health => 0.3f;

    public override void OnAbility(IUpgradeablePlayer upgradeablePlayer)
    {
        upgradeablePlayer.ExecuteHealingField_OnAbility();
    }
}

public class UpgradeHitman : Upgrade
{
    public override string Name => "Hitman";
    public override string Description => "Break the sound barrier with bullets that leave enemies in awe and questioning their life choices.";
    public override string HelpfulDescription => "Bullet Range +250%\nBullet Speed +250%\nFire Delay +100%";

    public override float BulletRange => 2.5f;
    public override float BulletSpeed => 2.5f;
    public override float FireDelay => 1.0f;
}

public class UpgradeHoming : Upgrade
{
    public override string Name => "Homing";
    public override string Description => "Give your bullets a crash course in stalking 101, turning them into slightly creepy projectiles that relentlessly pursue visible targets.";
    public override string HelpfulDescription => "Bullets home towards visible targets\n\nBullet Damage -25%\nFire Delay +50%";

    public override float BulletDamage => -0.25f;
    public override float FireDelay => 0.5f;

    public override void BulletUpdate(IUpgradeableBullet upgradeableBullet)
    {
        upgradeableBullet.ExecuteHoming_BulletUpdate();
    }
}

public class UpgradeMentalMeltdown : Upgrade
{
    public override string Name => "Mental Meltdown";
    public override string Description => "Your bullets possess the power to crash your enemies' brains, leaving them searching for a Ctrl+Alt+Delete button to reboot their shattered thoughts.";
    public override string HelpfulDescription => "Bullets stun the opponent";

    public override bool OnBulletImpact(IUpgradeableBullet upgradeableBullet, Collision2D collision)
    {
        return upgradeableBullet.ExecuteMentalMeltdown_OnBulletImpact(collision);
    }
}

public class UpgradeMinigun : Upgrade
{
    public override string Name => "Minigun";
    public override string Description => "";
    public override string HelpfulDescription => "";

    public override float BulletDamage => -0.8f;
    public override float FireDelay => -0.9f;
    public override float MagazineSize => 4f;
    public override float WeaponSpray => 3f;
}

public class UpgradePhoenix : Upgrade
{
    public override string Name => "Phoenix";
    public override string Description => "Rise from the ashes with the power of a phoenix and turn your defeat into a glorious opportunity that ignite your comeback.";
    public override string HelpfulDescription => "Respawn once on death\n\nHealth -35%";

    public override float Health => -0.35f;

    public override void OnPlayerDeath(IUpgradeablePlayer upgradeablePlayer)
    {
        upgradeablePlayer.ExecutePhoenix_OnPlayerDeath();
    }
}

public class UpgradePoison : Upgrade
{
    public override string Name => "Poison";
    public override string Description => "Experience the sadistic pleasure of watching your enemies writhe in a long-lasting death throes that slowly fade their health.";
    public override string HelpfulDescription => "";
}

public class UpgradeTank : Upgrade
{
    public override string Name => "Tank";
    public override string Description => "Roar into battle as the ferocious Tankasaurus, impervious to damage and ready to stomp through enemy lines.";
    public override string HelpfulDescription => "Health +100%\nFire Delay +100%";

    public override float Health => 1f;
    public override float FireDelay => 1f;
}