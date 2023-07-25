using System;
using UnityEditor;
using UnityEngine;

public class PlayerBullet : MonoBehaviour, IUpgradeableBullet
{
    [SerializeField] private float defaultDistance = 10f;
    [SerializeField] private float defaultBulletSpeed = 20f;
    private float _damage;
    private GameObject _sourceCharacter;

    private Rigidbody2D _rb;
    private LineRenderer _lineRenderer;

    // Upgrade: Bounce
    [Header("Upgrade: Bounce")] [SerializeField] private PhysicsMaterial2D bulletBouncePhysicsMaterial;
    [SerializeField] private int maxBounces = 2;
    private int _bouncesLeft;

    // Upgrade: Homing
    [Header("Upgrade: Homing")] [SerializeField] private float homingRadius = 8f;
    [SerializeField] [Range(1, 360)] private float homingAngleHalf = 50f;
    [SerializeField] private float homingRotationSpeed = 250f;
    [SerializeField] private LayerMask homingTargetLayer;
    [SerializeField] [Range(0f, 1f)] private float visualViewBoundFocusSpeed = 0.3f;
    [SerializeField] private float visualViewBoundLength = 4f;
    private Vector2 _visualLastAngleLeft, _visualAngleRight;

    // Upgrade: Explosive Bullet
    [Header("Upgrade: Explosive Bullet")] [SerializeField] private float explosiveBulletRadius = 1f;
    [SerializeField] private LayerMask explosiveBulletTargetLayer;

    // Upgrade: Drill
    [Header("Upgrade: Drill")] [SerializeField] private int drillPlayerBulletLayerId;
    [SerializeField] private int drillWallLayerId;

#if UNITY_EDITOR
    private bool _canSeeTargetCharacterGizmos;
    private Vector2 _targetPositionGizmos;
#endif

    private void Awake()
    {
        _bouncesLeft = maxBounces;
        _rb = GetComponent<Rigidbody2D>();
        _lineRenderer = GetComponent<LineRenderer>();
        UpgradeManager.Init(this);
    }

    private void Start()
    {
        float bulletSpeed = defaultBulletSpeed * UpgradeManager.GetBulletSpeedMultiplier();
        
        _rb.velocity = bulletSpeed * transform.up;
        Destroy(gameObject, defaultDistance * UpgradeManager.GetBulletRangeMultiplier() / bulletSpeed);
    }

    private void FixedUpdate()
    {
        UpgradeManager.BulletUpdate(this);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!UpgradeManager.OnBulletImpact(this, other))
        {
            Destroy(gameObject);
        }

        other.gameObject.GetComponent<ICharacterHealth>()?.InflictDamage(_damage, true);
    }

    /// <summary>
    /// Initializes this bullet.
    /// </summary>
    /// <param name="assignedDamage">Amount of damage caused by this bullet.</param>
    /// <param name="sourceCharacter">Reference of the character who shot this bullet.</param>
    public void Init(float assignedDamage, GameObject sourceCharacter)
    {
        _damage = assignedDamage;
        _sourceCharacter = sourceCharacter;
    }


    // Upgrade: Bounce
    public void InitBounce()
    {
        GetComponent<Rigidbody2D>().sharedMaterial = bulletBouncePhysicsMaterial;
    }

    public bool ExecuteBounce_OnBulletImpact(Collision2D collision)
    {
        if (_bouncesLeft > 0 && !collision.gameObject.CompareTag("Enemy") && !collision.gameObject.CompareTag("Player"))
        {
            _bouncesLeft--;
            
            float angle = Mathf.Atan2(_rb.velocity.y, _rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
            
            return true;
        }

        return false;
    }


    // Upgrade: Homing
    public void ExecuteHoming_BulletUpdate()
    {
        Vector2 targetPos = Vector2.zero;
        Vector2 directionToTargetNormalized = Vector2.zero;

        if (CheckCharacterInFieldOfView(ref targetPos, ref directionToTargetNormalized))
        {
            Vector2 forwardDirection = transform.up;
            float rotationAmount = Vector3.Cross(directionToTargetNormalized, forwardDirection).z;
            _rb.angularVelocity = -rotationAmount * homingRotationSpeed;

            _rb.velocity = forwardDirection * defaultBulletSpeed;

#if UNITY_EDITOR //////////////////////////////////////////////////////////////////////////////////
            _canSeeTargetCharacterGizmos = true;
            _targetPositionGizmos = targetPos;
#endif ////////////////////////////////////////////////////////////////////////////////////////////
        }
        else
        {
            _rb.angularVelocity = 0;

#if UNITY_EDITOR //////////////////////////////////////////////////////////////////////////////////
            _canSeeTargetCharacterGizmos = false;
#endif ////////////////////////////////////////////////////////////////////////////////////////////
        }

        VisualizeHoming(_canSeeTargetCharacterGizmos, directionToTargetNormalized);
    }

    /// <summary>
    /// Checks if a target is in the field of view of this bullet
    /// </summary>
    /// <param name="closestTarget">Position of the closest target</param>
    /// <param name="directionToTarget">Direction in which the target is located</param>
    /// <returns>Bool, whether a target is in the field of view of this bullet</returns>
    private bool CheckCharacterInFieldOfView(ref Vector2 closestTarget, ref Vector2 directionToTarget)
    {
        Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, homingRadius, homingTargetLayer);

        if (rangeCheck.Length <= 0)
        {
            return false;
        }

        Vector2 position = transform.position;
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

        return Vector2.Angle(transform.up, directionToTarget) < homingAngleHalf /*&& !Physics2D.Raycast(position, directionToTarget, closestDistance)*/;
    }

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
    }

    // Upgrade: Explosive Bullet
    public bool ExecuteExplosiveBullet_OnBulletImpact(Collision2D collision)
    {
        Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, explosiveBulletRadius, explosiveBulletTargetLayer);

        foreach (Collider2D targetCollider in rangeCheck)
        {
            targetCollider.GetComponent<ICharacterHealth>().InflictDamage(0);
        }

        return false;
    }


    // Upgrade: Mental Meltdown
    public bool ExecuteMentalMeltdown_OnBulletImpact(Collision2D collision)
    {
        collision.gameObject.GetComponent<ICharacterController>()?.StunCharacter();
        return false;
    }


    // Upgrade: Drill
    public void InitDrill()
    {
        throw new NotImplementedException();
        // Enables that the projectile can shoot through a wall (is disabled by the bullet's child OnTriggerEnter2D method)
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Walls"), LayerMask.NameToLayer("PlayerBullets"), true);
    }


#if UNITY_EDITOR //////////////////////////////////////////////////////////////////////////////////
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Handles.DrawWireDisc(transform.position, Vector3.forward, homingRadius);

        Vector3 angle01 = Util.DirectionFromAngle(-transform.eulerAngles.z - homingAngleHalf);
        Vector3 angle02 = Util.DirectionFromAngle(-transform.eulerAngles.z + homingAngleHalf);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + angle01 * homingRadius);
        Gizmos.DrawLine(transform.position, transform.position + angle02 * homingRadius);

        if (_canSeeTargetCharacterGizmos)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, _targetPositionGizmos);
        }
    }
#endif ////////////////////////////////////////////////////////////////////////////////////////////
}