using UnityEngine;

public class UpgradeHoming : Upgrade
{
    public override string Name => "Homing";
    public override UpgradeIdentification UpgradeIdentification => UpgradeIdentification.Homing;
    public override UpgradeType UpgradeType => UpgradeType.Weapon;
    public override string FlavorText => "Give your bullets a crash course in stalking 101, turning them into slightly creepy projectiles that relentlessly pursue visible targets.";
    public override string Description => "Bullets home towards visible targets but decreased damage and slower relead";

    public override float BulletDamage => -0.25f;
    public override float FireCooldown => 0.5f;

    private readonly LayerMask _targetLayer = LayerMask.GetMask("Player_Trigger", "Enemy_Trigger");

    private float _bulletSpeed;
    private float _bulletRotationSpeed;

    public override void Init(PlayerBullet playerBullet)
    {
        _bulletSpeed = Configuration.Bullet_MovementSpeed * UpgradeManager.GetBulletSpeedMultiplier();
        _bulletRotationSpeed = Configuration.Homing_RotationSpeed * UpgradeManager.GetBulletSpeedMultiplier();
    }

    public override void BulletUpdate(PlayerBullet playerBullet)
    {
        if (playerBullet.BouncesLeft == Configuration.Bounce_BounceCount)
            return;

        Vector2 targetPos = Vector2.zero;
        Vector2 directionToTargetNormalized = Vector2.zero;

        if (CheckCharacterInFieldOfView(playerBullet.transform, ref targetPos, ref directionToTargetNormalized))
        {
            Vector2 forwardDirection = playerBullet.transform.up;
            float rotationAmount = Vector3.Cross(directionToTargetNormalized, forwardDirection).z;

            playerBullet.Rigidbody.angularVelocity = -rotationAmount * _bulletRotationSpeed;
            playerBullet.Rigidbody.velocity = forwardDirection * _bulletSpeed;
        }
        else
        {
            playerBullet.Rigidbody.angularVelocity = 0f;
        }
    }

    /// <summary>
    /// Checks if a target is in the field of view of this bullet
    /// </summary>
    /// <param name="transform">Bullet transform</param>
    /// <param name="closestTarget">Position of the closest target</param>
    /// <param name="directionToTarget">Direction in which the target is located</param>
    /// <returns>Bool, whether a target is in the field of view of this bullet</returns>
    private bool CheckCharacterInFieldOfView(Transform transform, ref Vector2 closestTarget, ref Vector2 directionToTarget)
    {
        Vector2 position = transform.position;
        Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(position, Configuration.Homing_Radius, _targetLayer);

        if (rangeCheck.Length <= 0)
        {
            return false;
        }

        Vector2 closestTargetPos = rangeCheck[0].transform.position;
        float closestDistance = Mathf.Infinity;

        foreach (Collider2D targetCollider in rangeCheck)
        {
            float curDistance = Vector2.Distance(targetCollider.transform.position, position);

            if (curDistance < closestDistance)
            {
                closestTargetPos = targetCollider.transform.position;
                closestDistance = curDistance;
            }
        }

        closestTarget = closestTargetPos;
        directionToTarget = (closestTargetPos - position).normalized;

        return Vector2.Angle(transform.up, directionToTarget) < Configuration.Homing_HalfAngle;
    }

    /*
    [SerializeField] [Range(0f, 1f)] private float visualViewBoundFocusSpeed = 0.3f;
    [SerializeField] private float visualViewBoundLength = 4f;
    private Vector2 _visualLastAngleLeft, _visualAngleRight;


    /// <summary>
    /// Homing upgrade visualization
    /// </summary>
    /// <param name="canSeeTargetCharacter">Bool, whether this bullet can "see" a target character</param>
    /// <param name="directionToTarget">Vector pointing to the target character</param>
    private void VisualizeHoming(bool canSeeTargetCharacter, Vector3 directionToTarget)
    {
        float currentRotation = -transform.eulerAngles.z;
        Vector3 currentPosition = transform.position;

        Vector2 angleLeft;
        Vector2 angleRight;

        if (canSeeTargetCharacter)
        {
            angleLeft = Quaternion.Euler(0f, 0f, -5f) * directionToTarget;
            angleRight = Quaternion.Euler(0f, 0f, 5f) * directionToTarget;
            _lineRenderer.startColor = Color.red;
            _lineRenderer.endColor = Color.red;
        }
        else
        {
            angleLeft = Util.DirectionFromAngle(currentRotation - homingAngleHalf);
            angleRight = Util.DirectionFromAngle(currentRotation + homingAngleHalf);
            _lineRenderer.startColor = Color.grey;
            _lineRenderer.endColor = Color.grey;
        }

        angleLeft = Vector2.Lerp(_visualLastAngleLeft, angleLeft, visualViewBoundFocusSpeed).normalized;
        angleRight = Vector2.Lerp(_visualAngleRight, angleRight, visualViewBoundFocusSpeed).normalized;

        _visualLastAngleLeft = angleLeft;
        _visualAngleRight = angleRight;

        _lineRenderer.SetPositions(new[] { currentPosition + (Vector3)angleLeft * visualViewBoundLength, currentPosition, currentPosition + (Vector3)angleRight * visualViewBoundLength });
    }*/
}