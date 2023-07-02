using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    // Animator of the player
    [SerializeField] private Animator playerAnimator;

    // Animator of the weapon
    [SerializeField] private Animator weaponAnimator;

    // Sprite of the player
    [SerializeField] private SpriteRenderer playerSprite;

    // Sprite of the players weapon
    [SerializeField] private SpriteRenderer weaponSprite;

    private static readonly int Running = Animator.StringToHash("Running");
    private static readonly int Dead = Animator.StringToHash("Dead");

    private void Start()
    {
        EventManager.OnPlayerMove.Subscribe(PlayerMovementAnimation);
        EventManager.OnPlayerAim.Subscribe(PlayerAimingAnimation);
        EventManager.OnPlayerFire.Subscribe(FireWeaponAnimation);
        EventManager.OnPlayerDeath.Subscribe(PlayerDeathAnimation);
    }

    /// <summary>
    /// Start the running animations when the player is moving.
    /// Flips the sprite depending whether the player is moving left or right.
    /// </summary>
    /// <param name="movementInput">The movement input of the player</param>
    private void PlayerMovementAnimation(Vector2 movementInput)
    {
        if (movementInput.x > 0)
        {
            playerSprite.flipX = false;
        }

        if (movementInput.x < 0)
        {
            playerSprite.flipX = true;
        }

        playerAnimator.SetBool(Running, !movementInput.Equals(Vector2.zero));
    }

    /// <summary>
    /// Flips the players weapon in the y axis when he is aiming to the left so the weapon isn't upside down.
    /// </summary>
    /// <param name="angle">Calculated angle </param>
    private void PlayerAimingAnimation(float angle)
    {
        weaponSprite.flipY = angle is >= 90f and <= 180f or <= -90f and >= -180f;
    }

    /// <summary>
    /// Plays the death animation of the player
    /// </summary>
    private void PlayerDeathAnimation()
    {
        playerAnimator.Play("Player_Death");
        weaponSprite.enabled = false;
    }

    private void FireWeaponAnimation()
    {
        weaponAnimator.Play("Weapon_Fire");
    }

    private void OnDestroy()
    {
        EventManager.OnPlayerMove.Unsubscribe(PlayerMovementAnimation);
        EventManager.OnPlayerAim.Unsubscribe(PlayerAimingAnimation);
        EventManager.OnPlayerFire.Unsubscribe(FireWeaponAnimation);
        EventManager.OnPlayerDeath.Unsubscribe(PlayerDeathAnimation);
    }
}