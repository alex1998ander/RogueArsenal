using UnityEngine;

public class UpgradeHitman : Upgrade {
    public override string Name => "Hitman";
    public override string Description => "Break the sound barrier with bullets that leave enemies in awe and questioning their life choices.";
    
    public override float BulletRange => 2.5f;
    public override float AttackDelay => -0.5f;
}

public class UpgradeBuckshot : Upgrade {
    public override string Name => "Buckshot";
    public override string Description => "Unleash a shotgun-inspired impact that scatters enemies like confetti.";

    public override float BulletRange => -0.5f;
    public override int BulletCount => 4;
    public override float BulletDamage => -0.6f;
}

public class UpgradeBurst : Upgrade {
    public override string Name => "Burst";
    public override string Description => "Trade the single-shot snooze for a burst of pew-pew-pew and turn your enemies into a walking target.";
    
    public override float BulletDamage => -0.6f;

    public override void OnFire(IUpgradeablePlayer upgradeablePlayer) {
        upgradeablePlayer.ExecuteBurst_OnFire();
    }
}

public class UpgradeBounce : Upgrade {
    public override string Name => "Bounce";
    public override string Description => "Inject your bullets with enthusiasm, turning your attacks into a lively pinball game.";
    
    public override float BulletDamage => 0.25f;

    public override void Init(IUpgradeableBullet upgradeableBullet) {
        upgradeableBullet.InitBounce();
    }

    public override bool OnBulletImpact(IUpgradeableBullet upgradeableBullet, Collision2D collision) {
        return upgradeableBullet.ExecuteBounce_OnBulletImpact(collision);
    }
}

public class UpgradeCarefulPlanning : Upgrade {
    public override string Name => "Careful Planning";
    public override string Description => "Embrace the spirit of meticulous plotting, trading rapid-fire chaos for jaw-dropping destruction.";
    
    public override float BulletDamage => 1.0f;
    public override float AttackDelay => 1.0f;
}

public class UpgradeTank : Upgrade {
    public override string Name => "Tank";
    public override string Description => "Roar into battle as the ferocious Tankasaurus, impervious to damage and ready to stomp through enemy lines.";
    
    public override float Health => 1.0f;
    public override float AttackDelay => 0.5f;
}

public class UpgradeExplosiveBullet : Upgrade {
    public override string Name => "Explosive Bullet";
    public override string Description => "Arm yourself with these explosive delights, turning your bullets into cheeky troublemakers that go 'boom' upon impact.";
    
    public override float AttackDelay => 1.0f;
    
    public override bool OnBulletImpact(IUpgradeableBullet upgradeableBullet, Collision2D collision) {
        return upgradeableBullet.ExecuteExplosiveBullet_OnBulletImpact(collision);
    }
}

public class UpgradeHealingField : Upgrade {
    public override string Name => "Healing Field";
    public override string Description => "Transform the battlefield into a spa-like oasis of mending with a field of rejuvenation that magically patches up your injuries.";
    
    public override float Health => 0.3f;
    
    public override void OnBlock(IUpgradeablePlayer upgradeablePlayer) {
        upgradeablePlayer.ExecuteHealingField_OnBlock();
    }
}

public class UpgradeHoming : Upgrade {
    public override string Name => "Homing";
    public override string Description => "Give your bullets a crash course in stalking 101, turning them into slightly creepy projectiles that relentlessly pursue visible targets.";
    
    public override float BulletDamage => -0.25f;
    public override float AttackDelay => 0.5f;
    
    public override void BulletUpdate(IUpgradeableBullet upgradeableBullet) {
        upgradeableBullet.ExecuteHoming_BulletUpdate();
    }
}

public class UpgradePhoenix : Upgrade {
    public override string Name => "Phoenix";
    public override string Description => "Rise from the ashes with the power of a phoenix and turn your defeat into a glorious opportunity that ignite your comeback.";
    
    public override float Health => -0.35f;
    
    public override void OnPlayerDeath(IUpgradeablePlayer upgradeablePlayer) {
        upgradeablePlayer.ExecutePhoenix_OnPlayerDeath();
    }
}
