using System;
using UnityEditor;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float Damage { get; set; }
    public float Lifetime { get; private set; }

    public Rigidbody2D Rigidbody { get; private set; }
    private LineRenderer _lineRenderer;


    // Upgrade: Bounce
    [Header("Upgrade: Bounce")] [SerializeField]
    private PhysicsMaterial2D bulletBouncePhysicsMaterial;

    public int BouncesLeft { get; set; }

    // Upgrade: Piercing
    public int PiercesLeft { get; set; }

#if UNITY_EDITOR
    private bool _canSeeTargetCharacterGizmos;
    private Vector2 _targetPositionGizmos;
#endif

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        _lineRenderer = GetComponent<LineRenderer>();
        UpgradeManager.Init(this);
    }

    private void FixedUpdate()
    {
        UpgradeManager.BulletUpdate(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!UpgradeManager.OnBulletTrigger(this, other))
        {
            Destroy(gameObject);
        }

        other.gameObject.GetComponent<ICharacterHealth>()?.InflictDamage(Damage, true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!UpgradeManager.OnBulletCollision(this, collision))
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Initializes this bullet and set its velocity
    /// </summary>
    /// <param name="assignedDamage">Amount of damage caused by this bullet</param>
    /// <param name="assignedLifetime">Lifetime in seconds</param>
    public void Init(float assignedDamage, float assignedLifetime = 0)
    {
        float bulletSpeed = Configuration.Bullet_MovementSpeed * UpgradeManager.GetBulletSpeedMultiplier();

        if (assignedLifetime == 0)
        {
            assignedLifetime = Configuration.Bullet_ShotDistance * UpgradeManager.GetBulletRangeMultiplier() / bulletSpeed;
        }

        Damage = assignedDamage;
        Lifetime = assignedLifetime;
        Rigidbody.velocity = bulletSpeed * transform.up;

        Destroy(gameObject, Lifetime);
    }

    public void AdjustFacingMovementDirection()
    {
        float angle = Mathf.Atan2(Rigidbody.velocity.y, Rigidbody.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
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