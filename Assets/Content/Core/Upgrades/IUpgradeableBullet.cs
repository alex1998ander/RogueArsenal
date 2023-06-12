using UnityEngine;

public interface IUpgradeableBullet
{
    #region Init

    /// <summary>
    /// Initializes the Bounce upgrade 
    /// </summary>
    public void InitBounce();

    #endregion


    #region BulletUpdate

    /// <summary>
    /// Executes the Homing upgrade every frame
    /// </summary>
    public void ExecuteHoming_BulletUpdate();

    #endregion


    #region OnBulletImpact

    /// <summary>
    /// Executes the Bounce upgrade when the bullet impacts
    /// </summary>
    /// <param name="collision">Collision data</param>
    /// <returns>Bool, whether the bullet should NOT be destroyed afterwards</returns>
    public bool ExecuteBounce_OnBulletImpact(Collision2D collision);

    /// <summary>
    /// Executes the Explosive Bullet upgrade when the bullet impacts
    /// </summary>
    /// <param name="collision">Collision data</param>
    /// <returns>Bool, whether the bullet should NOT be destroyed afterwards</returns>
    public bool ExecuteExplosiveBullet_OnBulletImpact(Collision2D collision);

    /// <summary>
    /// Executes the Mental Meltdown upgrade when the bullet impacts
    /// </summary>
    /// <param name="collision">Collision data</param>
    /// <returns>Bool, whether the bullet should NOT be destroyed afterwards</returns>
    public bool ExecuteMentalMeltdown_OnBulletImpact(Collision2D collision);

    /// <summary>
    /// Executes the Wall Piercer upgrade when the bullet impacts
    /// </summary>
    /// <param name="collision">Collision data</param>
    /// <returns>Bool, whether the bullet should NOT be destroyed afterwards</returns>
    public bool ExecuteWallPiercer_OnBulletImpact(Collision2D collision);

    #endregion
}