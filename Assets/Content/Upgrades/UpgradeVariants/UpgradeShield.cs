public class UpgradeShield : Upgrade
{
    public override string Name => "Shield";
    public override UpgradeIdentification UpgradeIdentification => UpgradeIdentification.Shield;
    public override UpgradeType UpgradeType => UpgradeType.Ability;

    public override string FlavorText => "";
    public override string Description => "Elegantly turn enemy bullets back to their dismayed shooters.";

    public override void OnAbility(PlayerController playerController, PlayerWeapon playerWeapon)
    {
        UpgradeSpawnablePrefabHolder.SpawnPrefab(UpgradeSpawnablePrefabHolder.instance.shieldPrefab, playerController.transform.position, Configuration.Shield_Duration, playerController.gameObject);
        playerController.StartCoroutine(Util.OnOffCoroutine(
            ActivateShield,
            DeactivateShield,
            Configuration.Shield_Duration)
        );
        EventManager.OnShieldStart.Trigger();
    }

    public void ActivateShield()
    {
        PlayerData.canDash = false;
        PlayerData.invulnerable = true;
        PlayerData.ShieldActive = true;
    }

    public void DeactivateShield()
    {
        PlayerData.canDash = true;
        PlayerData.invulnerable = false;
        PlayerData.ShieldActive = false;
    }
}