using UnityEngine;

public class Configuration
{
    // Naming convention: <module/upgrade name>UNDERSCORE<value type>

    // Player constants
    public const float Player_MaxHealth = 100f;
    public const float Player_MovementSpeed = 5f;
    public const float Player_DashSpeed = 15f;
    public const float Player_DashTime = 0.3f;
    public const float Player_DashCoolDown = 0.1f;
    public const float Player_FireCoolDown = 0.2f;
    public const float Player_AbilityCoolDown = 5.0f;
    public const float Player_SelfDamageMultiplier = 0.2f; //0.4f;

    // Weapon constants
    public const float Weapon_Damage = 30f;
    public const float Weapon_SprayAngle = 5f;
    public const float Weapon_ReloadTime = 1f;
    public const float Weapon_BulletSpawnDistance = 1.4f;
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
    public const float Enemy_DashTime = 0.2f;
    public const float Enemy_DashForce = 30f;

    // Boss constants
    public const float Boss_AttackSpeed = 0.25f;
    public const float Boss_AbilityCooldown = 2f;
    public const float Boss_StompRadius = 1f;
    public const float Boss_StompDamage = 40f;
    public const float Boss_LaserDamage = 30f;
    public const int Boss_LaserRepetitions = 3;
    public const float Boss_MineCountdown = 3f;
    public const float Boss_MineDamage = 50f;
    public const float Boss_360ShotBulletDamage = 20f;
    public const float Boss_360ShotBulletDistance = 20f;
    public const float Boss_360ShotBulletSpeed = 6f;
    public const int Boss_360ShotWaveCount = 3;
    public const float Boss_ShieldMaxHealth = 200f;
    public const float Boss_ShieldRotationSpeed = 65f;

    // Upgrade: Bounce
    public const int Bounce_BounceCount = 2;

    // Upgrade: Burst
    public const int Burst_AdditionalBulletCount = 2;
    public const float Burst_FireDelayFraction = 0.15f;

    // Upgrade: Demonic Pact
    public const int DemonicPact_DamageBurstsPerSecond = 1;
    public const float DemonicPact_IgnoredDamageBurstsAfterHeal = 2f;
    public const float DemonicPact_DamagePerSecond = 5f;
    public const float DemonicPact_MinHealAmount = 0.5f;
    public const float DemonicPact_MaxHealAmount = 20f;
    public const float DemonicPact_MinBulletDamageBase = 5.0f;
    public const float DemonicPact_MaxBulletDamageBase = 300.0f;
    public const float DemonicPact_MinHealthPercentage = 0.2f;

    // Upgrade: Explosive Bullet
    public const float ExplosiveBullet_MinDamage = 2f;
    public const float ExplosiveBullet_MaxDamage = 60f;
    public const float ExplosiveBullet_MinSize = 0.5f;
    public const float ExplosiveBullet_MaxSize = 1.5f;
    public const float ExplosiveBullet_BulletDamageBaseMin = 5f;
    public const float ExplosiveBullet_BulletDamageBaseMax = 50f;
    public const float ExplosiveBullet_Radius = 1.5f;

    // Upgrade: Healing Field
    public const int HealingField_Bursts = 5;
    public const float HealingField_Amount = 30f;
    public const float HealingField_Duration = 1.5f;
    public const float HealingField_Radius = 1.5f;

    // Upgrade: Homing
    public const float Homing_HalfAngle = 50f;
    public const float Homing_Radius = 8f;
    public const float Homing_RotationSpeed = 800f;

    // Upgrade: Phoenix
    public const float Phoenix_WarmUpTime = 4f;
    public const float Phoenix_InvincibilityTime = 3f;

    // Upgrade: Piercing
    public const int Piercing_PiercesCount = 1;

    // Upgrade: Shockwave
    public const float Shockwave_Duration = 1f;
    public const float Shockwave_Range = 5f;
    public const float Shockwave_MinStrength = 2000f;
    public const float Shockwave_MaxStrength = 4000f;

    // Upgrade: Sinusoidal Shots
    public const float SinusoidalShots_Frequency = 2f * Mathf.PI * 2f; // Two times Pi to normalize the frequency to 1 full wavelength a second
    public const float SinusoidalShots_Amplitude = 0.15f;
    public const float SinusoidalShots_SplitShotHalfAngleAdjustment = -40f;

    // Upgrade: Smart Pistol
    public const float SmartPistol_Range = 5f;

    // Upgrade: Split Shot
    public const float SplitShot_DamageMultiplierAfterwards = 0.5f;
    public const float SplitShot_Delay = 0.1f;
    public const float SplitShot_HalfAngle = 15f;

    // Upgrade: Stimpack
    public const float Stimpack_DamageMultiplier = 1.5f;
    public const float Stimpack_Duration = 4f;

    // Upgrade: Timefreeze
    public const float Timefreeze_Duration = 1f;
    public const float Timefreeze_TimeScale = 0.5f;

    // Upgrade: Shield
    public const float Shield_Duration = 2f;
}