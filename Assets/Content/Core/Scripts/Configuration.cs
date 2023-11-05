using UnityEngine;

public class Configuration
{
    // Naming convention: <module/upgrade name>UNDERSCORE<value type>

    // Player constants
    public const float Player_MaxHealth = 100f;
    public const float Player_MovementSpeed = 5f;
    public const float Player_DashSpeed = 15f;
    public const float Player_DashTime = 0.2f;
    public const float Player_DashCoolDown = 0.1f;
    public const float Player_FireCoolDown = 0.2f;
    public const float Player_AbilityCoolDown = 5.0f;
    public const float Player_SelfDamageMultiplier = 0.4f;

    // Weapon constants
    public const float Weapon_Damage = 30f;
    public const float Weapon_SprayAngle = 5f;
    public const float Weapon_ReloadTime = 1f;
    public const float Weapon_BulletSpawnDistance = 0.8f;
    public const int Weapon_BulletCount = 1;
    public const int Weapon_MagazineSize = 10;

    // Weapon max constants
    public const float WeaponSprayMax = 9f;
    public const float WeaponFireCooldownMax = 10f;

    // Bullet constants
    public const float Bullet_ShotDistance = 10f;
    public const float Bullet_MovementSpeed = 15f;
    public const float Bullet_SpawnTime = 0.5f;

    // Enemy constants
    public const float Enemy_ViewDistance = 6f;
    public const float Enemy_HearDistance = 10f;
    public const float Enemy_StunTime = 1f;
    public const float Enemy_StunImmunityTime = 2f;
    public const float Enemy_ThrownTime = 1f;
    public const float Enemy_ThrownImmunityTime = 2f;

    // Upgrade: Big Bullet
    public const int BigBullet_PiercesCount = 3;

    // Upgrade: Bounce
    public const int Bounce_BounceCount = 2;

    // Upgrade: Burst
    public const int Burst_AdditionalBulletCount = 2;
    public const float Burst_FireDelayFraction = 0.15f;

    // Upgrade: Demonic Pact
    public const float DemonicPact_HealthLossPerSecond = 5f;
    public const float DemonicPact_BaseHealAmount = 0.2f;

    // Upgrade: Explosive Bullet
    public const float ExplosiveBullet_Radius = 1f;

    // Upgrade: Healing Field
    public const float HealingField_Duration = 1.5f;

    // Upgrade: Homing
    public const float Homing_HalfAngle = 50f;
    public const float Homing_Radius = 8f;
    public const float Homing_RotationSpeed = 400f;

    // Upgrade: Piercing
    public const int Piercing_PiercesCount = 1;

    // Upgrade: Shockwave
    public const float Shockwave_Range = 5f;
    public const float Shockwave_MinStrength = 2000f;
    public const float Shockwave_MaxStrength = 4000f;

    // Upgrade: Sinusoidal Shots
    public const float SinusoidalShots_Frequency = 2f * Mathf.PI * 2f; // Two times Pi to normalize the frequency to 1 full wavelength a second
    public const float SinusoidalShots_Amplitude = 15f;
    public const float SinusoidalShots_SplitShotHalfAngleAdjustment = -40f;

    // Upgrade: Smart Pistol
    public const float SmartPistol_Range = 5f;

    // Upgrade: Split Shot
    public const float SplitShot_DamageMultiplierAfterwards = 1.1f;
    public const float SplitShot_Delay = 0.1f;
    public const float SplitShot_HalfAngle = 15f;

    // Upgrade: Stimpack
    public const float Stimpack_DamageMultiplier = 2f;
    public const float Stimpack_Duration = 5f;

    // Upgrade: Timefreeze
    public const float Timefreeze_Duration = 3f;
    public const float Timefreeze_TimeScale = 0.5f;
}