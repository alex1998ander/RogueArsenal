public interface IUpgradeablePlayer
{
    #region OnFire

    /// <summary>
    /// Executes the Burst upgrade when the player fires
    /// </summary>
    public void ExecuteBurst_OnFire();

    #endregion


    #region OnBlock

    /// <summary>
    /// Executes the Healing Field upgrade when the player blocks
    /// </summary>
    public void ExecuteHealingField_OnBlock();

    #endregion


    #region OnPlayerDeath

    /// <summary>
    /// Executes the Phoenix upgrade when the player dies
    /// </summary>
    public void ExecutePhoenix_OnPlayerDeath();

    #endregion
}