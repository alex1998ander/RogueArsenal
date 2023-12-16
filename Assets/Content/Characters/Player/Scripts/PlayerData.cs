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
    
    // Upgrade: Phoenix
    public static bool phoenixed;
    
    // Upgrade: Sticky Fingers
    public static bool stickyFingers;
}