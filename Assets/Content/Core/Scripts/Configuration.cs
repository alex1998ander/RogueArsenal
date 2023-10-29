public class Configuration
{
    // Naming convention: <module/upgrade name>UNDERSCORE<value type>

    public const float Player_MaxHealth = 100f;
    public const float Player_MovementSpeed = 5f;
    public const float Player_DashSpeed = 15f;
    public const float Player_DashTime = 0.2f;
    public const float Player_DashCoolDown = 0.1f;
    public const float Player_FireCoolDown = 0.2f;
    public const float Player_AbilityCoolDown = 5.0f;
    public const float Player_SelfDamageMultiplier = 0.4f;

    public const float Weapon_Damage = 30f;
    public const float Weapon_SprayAngle = 5f;
    public const float Weapon_ReloadTime = 1f;
    public const float Weapon_BulletSpawnDistance = 0.8f;
    public const int Weapon_BulletCount = 1;
    public const int Weapon_MagazineSize = 10;

    public const float WeaponSprayMax = 9f;
    public const float WeaponFireCooldownMax = 10f;

    public const float Bullet_ShotDistance = 10f;
    public const float Bullet_MovementSpeed = 15f;
    public const float Bullet_SpawnTime = 0.5f;

    public const int BigBullet_PiercesCount = 3;

    public const int Bounce_BounceCount = 2;

    public const int Burst_AdditionalBulletCount = 2;
    public const float Burst_FireDelayFraction = 0.25f;

    public const float ExplosiveBullet_Radius = 1f;

    public const float HealingField_Duration = 1.5f;

    public const float Homing_HalfAngle = 50f;
    public const float Homing_Radius = 8f;
    public const float Homing_RotationSpeed = 400f;

    public const int Piercing_PiercesCount = 1;

    public const float SmartPistol_Range = 5f;

    public const float SplitShot_DamageMultiplierAfterwards = 1.1f;
    public const float SplitShot_Delay = 0.1f;
    public const float SplitShot_HalfAngle = 15f;

    public const float Stimpack_DamageMultiplier = 2f;
    public const float Stimpack_Duration = 5f;

    public const float Timefreeze_Duration = 3f;
    public const float Timefreeze_TimeScale = 0.5f;
}