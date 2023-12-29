using UnityEngine;

public abstract class Upgrade
{
    public abstract string Name { get; }
    public abstract UpgradeIdentification UpgradeIdentification { get; }
    public abstract UpgradeType UpgradeType { get; }
    public virtual string FlavorText { get; protected set; }
    public virtual string Description { get; protected set; }

    public virtual float AbilityDelay { get; protected set; } = 0f;
    public virtual int BulletCount { get; protected set; } = 0;
    public virtual float BulletDamage { get; protected set; } = 0f;
    public virtual float BulletRange { get; protected set; } = 0f;
    public virtual float BulletSize { get; protected set; } = 0f;
    public virtual float BulletSpeed { get; protected set; } = 0f;
    public virtual float FireCooldown { get; protected set; } = 0f;
    public virtual float Health { get; protected set; } = 0f;
    public virtual float MagazineSize { get; protected set; } = 0f;
    public virtual float PlayerMovementSpeed { get; protected set; } = 0f;
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
    public virtual void OnFire(PlayerController playerController, PlayerWeapon playerWeapon, Vector2 fireDirectionOverwrite = default) { }

    /// <summary>
    /// Optional functionality that is performed when the player reloads
    /// </summary>
    /// <param name="playerController">Player reference</param>
    /// <param name="playerWeapon">Player weapon reference</param>
    public virtual void OnReload(PlayerController playerController, PlayerWeapon playerWeapon) { }

    /// <summary>
    /// Optional functionality that is performed when the players magazine has been emptied
    /// </summary>
    /// <param name="playerController">Player reference</param>
    /// <param name="playerWeapon">Player weapon reference</param>
    public virtual void OnMagazineEmptied(PlayerController playerController, PlayerWeapon playerWeapon) { }

    /// <summary>
    /// Optional functionality that is performed when the bullet is fired.
    /// This is only executed when the bullet is instantiated by the weapon.
    /// </summary>
    /// <param name="playerBullet">Player reference</param>
    public virtual void OnFire(PlayerBullet playerBullet) { }

    /// <summary>
    /// Optional functionality that is performed when the player uses their ability
    /// </summary>
    /// <param name="playerController">Player reference</param>
    /// <param name="playerWeapon">Player weapon reference</param>
    public virtual void OnAbility(PlayerController playerController, PlayerWeapon playerWeapon) { }

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
    /// Optional functionality that is executed when the bullet trigger zone overlaps something
    /// </summary>
    /// <param name="playerBullet">Bullet reference</param>
    /// <param name="other">Collider information</param>
    /// <returns>Bool, whether the bullet should survive afterwards</returns>
    public virtual bool OnBulletTrigger(PlayerBullet playerBullet, Collider2D other)
    {
        return false;
    }

    /// <summary>
    /// Optional functionality that is executed when the bullet collides with something
    /// </summary>
    /// <param name="playerBullet">Bullet reference</param>
    /// <param name="collision">Collision information</param>
    /// <returns>Bool, whether the bullet should survive afterwards</returns>
    public virtual bool OnBulletCollision(PlayerBullet playerBullet, Collision2D collision)
    {
        return false;
    }

    /// <summary>
    /// Optional functionality that is executed when the bullet is destroyed
    /// </summary>
    /// <param name="playerBullet">The destroyed bullet</param>
    public virtual void OnBulletDestroy(PlayerBullet playerBullet) { }

    /// <summary>
    /// Optional functionality that is executed when the player dies
    /// </summary>
    /// <param name="playerController">Player reference</param>
    public virtual void OnPlayerDeath(PlayerController playerController) { }
}