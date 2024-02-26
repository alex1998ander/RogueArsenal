public static class PlayerData
{
    public static float health;
    public static float maxHealth;

    public static int ammo;
    public static int maxAmmo;
    public static float reloadTime;

    public static float fireCooldown;
    public static float abilityCooldown;

    public static bool canMove = true;
    public static bool canDash = true;
    public static bool canFire = true;
    public static bool canReload = true;
    public static bool canUseAbility = true;

    public static bool invulnerable;
    public static bool IsDashing;

    public static bool god;

    // Upgrade: Phoenix
    public static bool phoenixed;

    // Upgrade: Healing Field
    public static bool healingFieldUsed;

    // Upgrade: Shield
    public static bool ShieldActive;

    // Upgrade: Sticky Fingers
    public static bool stickyFingers;

    public static void ResetData()
    {
        canMove = true;
        canDash = true;
        canFire = true;
        canReload = true;
        canUseAbility = true;
        invulnerable = false;
        IsDashing = false;
        phoenixed = false;
        ShieldActive = false;
        stickyFingers = false;
    }
}