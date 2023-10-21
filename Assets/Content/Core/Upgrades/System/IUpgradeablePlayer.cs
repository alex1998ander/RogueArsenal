public interface IUpgradeablePlayer
{
    #region OnFire

    /// <summary>
    /// Executes the Burst upgrade when the player fires
    /// </summary>
    public void ExecuteBurst_OnFire();

    /// <summary>
    /// Executes the Demonic Pact upgrade when the player fires
    /// </summary>
    public void ExecuteDemonicPact_OnFire();

    #endregion


    #region OnAbility

    /// <summary>
    /// Executes the Healing Field upgrade when the player uses their ability
    /// </summary>
    public void ExecuteHealingField_OnAbility();

    #endregion


    #region OnPlayerDeath

    /// <summary>
    /// Executes the Phoenix upgrade when the player dies
    /// </summary>
    public void ExecutePhoenix_OnPlayerDeath();

    #endregion
}