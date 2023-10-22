using UnityEngine;

public abstract class Upgrade
{
    public virtual string Name { get; } = "";
    public virtual string Description { get; protected set; } = "";
    public virtual string HelpfulDescription { get; protected set; } = "";

    public virtual float AbilityDelay { get; protected set; } = 0f;
    public virtual int BulletCount { get; protected set; } = 0;
    public virtual float BulletDamage { get; protected set; } = 0f;
    public virtual float BulletRange { get; protected set; } = 0f;
    public virtual float BulletSize { get; protected set; } = 0f;
    public virtual float BulletSpeed { get; protected set; } = 0f;
    public virtual float FireDelay { get; protected set; } = 0f;
    public virtual float Health { get; protected set; } = 0f;
    public virtual float MagazineSize { get; protected set; } = 0f;
    public virtual float PlayerMovementSpeed { get; protected set; } = 0f;
    public virtual bool PreventDash { get; protected set; } = false;
    public virtual float ReloadTime { get; protected set; } = 0f;
    public virtual float WeaponSpray { get; protected set; } = 0f;

    /// <summary>
    /// Optional functionality for initialization
    /// </summary>
    /// <param name="playerController">Player reference</param>
    public virtual void Init(PlayerController playerController) { }

    /// <summary>
    /// Optional functionality for initialization
    /// </summary>
    /// <param name="playerBullet">Bullet reference</param>
    public virtual void Init(PlayerBullet playerBullet) { }

    /// <summary>
    /// Optional functionality that is performed when the player fires
    /// </summary>
    /// <param name="playerController">Player reference</param>
    /// <param name="playerWeapon">Player weapon reference</param>
    public virtual void OnFire(PlayerController playerController, PlayerWeapon playerWeapon) { }

    /// <summary>
    ///  Optional functionality that is performed when the player reloads
    /// </summary>
    /// <param name="playerController">Player reference</param>
    /// <param name="playerWeapon">Player weapon reference</param>
    public virtual void OnReload(PlayerController playerController, PlayerWeapon playerWeapon) { }

    /// <summary>
    /// Optional functionality that is performed when the player uses their ability
    /// </summary>
    /// <param name="playerController">Player reference</param>
    public virtual void OnAbility(PlayerController playerController) { }

    /// <summary>
    /// Optional functionality that is executed every frame while the bullet is flying
    /// </summary>
    /// <param name="playerBullet">Bullet reference</param>
    public virtual void BulletUpdate(PlayerBullet playerBullet) { }

    /// <summary>
    /// Optional functionality that is executed for the player every frame 
    /// </summary>
    /// <param name="playerController">Player reference</param>
    public virtual void PlayerUpdate(PlayerController playerController) { }

    /// <summary>
    /// Optional functionality that is executed when the bullet hits something
    /// </summary>
    /// <param name="playerBullet">Bullet reference</param>
    /// <param name="other">Collider information</param>
    /// <returns>Bool, whether the bullet should survive afterwards</returns>
    public virtual bool OnBulletImpact(PlayerBullet playerBullet, Collider2D other)
    {
        return false;
    }

    /// <summary>
    /// Optional functionality that is executed when the player dies
    /// </summary>
    /// <param name="playerController">Player reference</param>
    public virtual void OnPlayerDeath(PlayerController playerController) { }
}