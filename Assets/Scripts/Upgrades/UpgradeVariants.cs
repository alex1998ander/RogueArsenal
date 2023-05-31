using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeFastBall : Upgrade {
    public override string Name => "FastBall";
    public override string Description => "Break the sound barrier with bullets that leave enemies in awe and questioning their life choices.";

    public override float BulletSpeedMultiplier => 3.5f;
    public override float AttackSpeedMultiplier => 0.5f;
}

public class UpgradeBuckshot : Upgrade {
    public override string Name => "Buckshot";
    public override string Description => "Unleash a shotgun-inspired impact that scatters enemies like confetti.";

    public override float BulletCountMultiplier => 5.0f;
    public override float BulletDamageMultiplier => 0.4f;
}

public class UpgradeBurst : Upgrade {
    public override string Name => "Burst";
    public override string Description => "Trade the single-shot snooze for a burst of pew-pew-pew and turn your enemies into a walking target.";
    
    public override float BulletDamageMultiplier => 0.4f;

    public override void OnFire(IUpgradeable upgradeable) {
        upgradeable.ExecuteBurst_OnFire();
    }
}

public class UpgradeBounce : Upgrade {
    public override string Name => "Bounce";
    public override string Description => "Inject your bullets with enthusiasm, turning your attacks into a lively pinball game.";
    
    public override float BulletDamageMultiplier => 1.25f;

    public override void BulletUpdate(IUpgradeable upgradeable) {
        upgradeable.ExecuteBounce_BulletUpdate();
    }
}

public class UpgradeCarefulPlanning : Upgrade {
    public override string Name => "Careful Planning";
    public override string Description => "Embrace the spirit of meticulous plotting, trading rapid-fire chaos for jaw-dropping destruction.";
    
    public override float BulletDamageMultiplier => 2.0f;
    public override float AttackSpeedMultiplier => 0.5f;
}

public class UpgradeTank : Upgrade {
    public override string Name => "Tank";
    public override string Description => "Roar into battle as the ferocious Tankasaurus, impervious to damage and ready to stomp through enemy lines.";
    
    public override float HealthMultiplier => 2.0f;
    public override float AttackSpeedMultiplier => 0.75f;
}

public class UpgradeExplosiveBullet : Upgrade {
    public override string Name => "Explosive Bullet";
    public override string Description => "Arm yourself with these explosive delights, turning your bullets into cheeky troublemakers that go 'boom' upon impact.";
    
    public override float AttackSpeedMultiplier => 0.3f;
    
    public override void OnBulletImpact(IUpgradeable upgradeable) {
        upgradeable.ExecuteExplosiveBullet_OnBulletImpact();
    }
}

public class UpgradeHealingField : Upgrade {
    public override string Name => "Healing Field";
    public override string Description => "Transform the battlefield into a spa-like oasis of mending with a field of rejuvenation that magically patches up your injuries.";
    
    public override float HealthMultiplier => 1.3f;
    
    public override void OnBlock(IUpgradeable upgradeable) {
        upgradeable.ExecuteHealingField_OnBlock();
    }
}

public class UpgradeTargetTracer : Upgrade {
    public override string Name => "Target Tracer";
    public override string Description => "Give your bullets a crash course in stalking 101, turning them into slightly creepy projectiles that relentlessly pursue visible targets.";
    
    public override float BulletDamageMultiplier => 0.75f;
    public override float AttackSpeedMultiplier => 0.75f;
    
    public override void BulletUpdate(IUpgradeable upgradeable) {
        upgradeable.ExecuteTargetTracer_BulletUpdate();
    }
}

public class UpgradePhoenix : Upgrade {
    public override string Name => "Phoenix";
    public override string Description => "Rise from the ashes with the power of a phoenix and turn your defeat into a glorious opportunity that ignite your comeback.";
    
    public override float HealthMultiplier => 0.60f;
    
    public override void OnPlayerDeath(IUpgradeable upgradeable) {
        upgradeable.ExecutePhoenix_OnPlayerDeath();
    }
}
