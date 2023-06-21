using UnityEngine;

public class EmptyUpgradeSlot : Upgrade
{
}

public class UpgradeHitman : Upgrade
{
    public override string Name => "Hitman";
    public override string Description => "Break the sound barrier with bullets that leave enemies in awe and questioning their life choices.";

    public override float BulletRange => 2.5f;
    public override float FireDelay => -0.5f;
}

public class UpgradeBuckshot : Upgrade
{
    public override string Name => "Buckshot";
    public override string Description => "Unleash a shotgun-inspired impact that scatters enemies like confetti.";

    public override float BulletRange => -0.5f;
    public override int BulletCount => 4;
    public override float BulletDamage => -0.6f;
}

public class UpgradeBurst : Upgrade
{
    public override string Name => "Burst";
    public override string Description => "Trade the single-shot snooze for a burst of pew-pew-pew and turn your enemies into a walking target.";

    public override float BulletDamage => -0.6f;

    public override void OnFire(IUpgradeablePlayer upgradeablePlayer)
    {
        upgradeablePlayer.ExecuteBurst_OnFire();
    }
}

public class UpgradeBounce : Upgrade
{
    public override string Name => "Bounce";
    public override string Description => "Inject your bullets with enthusiasm, turning your attacks into a lively pinball game.";

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

public class UpgradeCarefulPlanning : Upgrade
{
    public override string Name => "Careful Planning";
    public override string Description => "Embrace the spirit of meticulous plotting, trading rapid-fire chaos for jaw-dropping destruction.";

    public override float BulletDamage => 1.5f;
    public override float FireDelay => 2f;
}

public class UpgradeTank : Upgrade
{
    public override string Name => "Tank";
    public override string Description => "Roar into battle as the ferocious Tankasaurus, impervious to damage and ready to stomp through enemy lines.";

    public override float Health => 1f;
    public override float FireDelay => -1f;
}

public class UpgradeExplosiveBullet : Upgrade
{
    public override string Name => "Explosive Bullet";
    public override string Description => "Arm yourself with these explosive delights, turning your bullets into cheeky troublemakers that go 'boom' upon impact.";

    public override float FireDelay => 1f;

    public override bool OnBulletImpact(IUpgradeableBullet upgradeableBullet, Collision2D collision)
    {
        return upgradeableBullet.ExecuteExplosiveBullet_OnBulletImpact(collision);
    }
}

public class UpgradeHealingField : Upgrade
{
    public override string Name => "Healing Field";
    public override string Description => "Transform the battlefield into a spa-like oasis of mending with a field of rejuvenation that magically patches up your injuries.";

    public override float Health => 0.3f;

    public override void OnBlock(IUpgradeablePlayer upgradeablePlayer)
    {
        upgradeablePlayer.ExecuteHealingField_OnBlock();
    }
}

public class UpgradeHoming : Upgrade
{
    public override string Name => "Homing";
    public override string Description => "Give your bullets a crash course in stalking 101, turning them into slightly creepy projectiles that relentlessly pursue visible targets.";

    public override float BulletDamage => -0.25f;
    public override float FireDelay => 0.5f;

    public override void BulletUpdate(IUpgradeableBullet upgradeableBullet)
    {
        upgradeableBullet.ExecuteHoming_BulletUpdate();
    }
}

public class UpgradePhoenix : Upgrade
{
    public override string Name => "Phoenix";
    public override string Description => "Rise from the ashes with the power of a phoenix and turn your defeat into a glorious opportunity that ignite your comeback.";

    public override float Health => -0.35f;

    public override void OnPlayerDeath(IUpgradeablePlayer upgradeablePlayer)
    {
        upgradeablePlayer.ExecutePhoenix_OnPlayerDeath();
    }
}

public class UpgradeBigBullet : Upgrade
{
    public override string Name => "Big Bullet";
    public override string Description => "Because size matters, watch as your bullets look big and intimidating. Who needs modesty when you can have an ego as big as a cannonball?";

    public override float BulletSize => 1f;
}

public class UpgradeMentalMeltdown : Upgrade
{
    public override string Name => "Mental Meltdown";
    public override string Description => "Your bullets possess the power to crash your enemies' brains, leaving them searching for a Ctrl+Alt+Delete button to reboot their shattered thoughts.";

    public override bool OnBulletImpact(IUpgradeableBullet upgradeableBullet, Collision2D collision)
    {
        return upgradeableBullet.ExecuteMentalMeltdown_OnBulletImpact(collision);
    }
}

public class UpgradeDemonicPact : Upgrade
{
    public override string Name => "Demonic Pact";
    public override string Description => "Embrace the dark arts of bullet wizardry and trade a bit of your life essence for instant trigger happiness.";

    public override void OnFire(IUpgradeablePlayer upgradeablePlayer)
    {
        upgradeablePlayer.ExecuteDemonicPact_OnFire();
    }
}

public class UpgradeDrill : Upgrade
{
    public override string Name => "Drill";
    public override string Description => "Break the laws of physics with bullets that defy solid matter, turning your enemies' hiding spots into mere illusions of safety.";

    public override bool OnBulletImpact(IUpgradeableBullet upgradeableBullet, Collision2D collision)
    {
        return upgradeableBullet.ExecuteDrill_OnBulletImpact(collision);
    }
}

public class UpgradeGlassCannon : Upgrade
{
    public override string Name => "Glass Cannon";
    public override string Description => "Deal devastating damage to your enemies, but be warned: A mere sneeze could knock you out.";

    public override float BulletDamage => 2f;
    public override float Health => -0.95f;
}