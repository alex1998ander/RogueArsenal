public class UpgradeStimpack : Upgrade
{
    public override string Name => "Stimpack";
    public override UpgradeIdentification UpgradeIdentification => UpgradeIdentification.Stimpack;
    public override UpgradeType UpgradeType => UpgradeType.Ability;
    public override string Description => "Damage multiplier for a short duration";

    public override float AbilityDelay => 1f;

    public override void OnAbility(PlayerController playerController, PlayerWeapon playerWeapon)
    {
        UpgradeSpawnablePrefabHolder.SpawnPrefab(UpgradeSpawnablePrefabHolder.instance.stimpackPrefab, playerController.transform.position, Configuration.Stimpack_Duration, playerController.gameObject);

        playerController.StartCoroutine(Util.OnOffCoroutine(
            () => BulletDamage = Configuration.Stimpack_DamageMultiplier,
            () => BulletDamage = 0f,
            Configuration.Stimpack_Duration)
        );

        EventManager.OnStimpack.Trigger();
    }
}