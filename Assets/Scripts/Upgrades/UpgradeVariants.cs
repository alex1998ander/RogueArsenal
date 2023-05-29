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
    public override string Description => "";
    
    public override float BulletDamageMultiplier => 0.4f;

    public override void Fire(IUpgradeable upgradeable) {
        upgradeable.ExecuteBurstFire();
    }
}