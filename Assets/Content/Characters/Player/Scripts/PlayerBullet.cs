using System;
using UnityEditor;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private float _damage;
    private GameObject _sourceCharacter;

    private Rigidbody2D _rb;
    private LineRenderer _lineRenderer;

    // Upgrade: Bounce
    [Header("Upgrade: Bounce")] [SerializeField] private PhysicsMaterial2D bulletBouncePhysicsMaterial;
    public int BouncesLeft { get; set; } = Configuration.Bounce_BounceCount;

#if UNITY_EDITOR
    private bool _canSeeTargetCharacterGizmos;
    private Vector2 _targetPositionGizmos;
#endif

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _lineRenderer = GetComponent<LineRenderer>();
        UpgradeManager.Init(this);
    }

    private void Start()
    {
        float bulletSpeed = Configuration.Bullet_MovementSpeed * UpgradeManager.GetBulletSpeedMultiplier();
        
        _rb.velocity = bulletSpeed * transform.up;
        Destroy(gameObject, Configuration.Bullet_ShotDistance * UpgradeManager.GetBulletRangeMultiplier() / bulletSpeed);
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
    public void SetBouncyPhysicsMaterial()
    {
        GetComponent<Rigidbody2D>().sharedMaterial = bulletBouncePhysicsMaterial;
    }

    public void AdjustFacingMovementDirection()
    {
            float angle = Mathf.Atan2(_rb.velocity.y, _rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
    }


    // Upgrade: Homing
    public void SetRigidbodyAngularVelocity(float angularVelocity)
    {
        _rb.angularVelocity = angularVelocity;
    }


#if UNITY_EDITOR //////////////////////////////////////////////////////////////////////////////////
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Handles.DrawWireDisc(transform.position, Vector3.forward, Configuration.Homing_Radius);

        Vector3 angle01 = Util.DirectionFromAngle(-transform.eulerAngles.z - Configuration.Homing_HalfAngle);
        Vector3 angle02 = Util.DirectionFromAngle(-transform.eulerAngles.z + Configuration.Homing_HalfAngle);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + angle01 * Configuration.Homing_Radius);
        Gizmos.DrawLine(transform.position, transform.position + angle02 * Configuration.Homing_Radius);

        if (_canSeeTargetCharacterGizmos)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, _targetPositionGizmos);
        }
    }
#endif ////////////////////////////////////////////////////////////////////////////////////////////
}