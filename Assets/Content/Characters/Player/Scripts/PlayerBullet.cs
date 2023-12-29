using System;
using UnityEditor;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float Damage { get; set; }
    public float TotalLifetime { get; private set; }
    public float Lifetime { get; private set; }

    public Rigidbody2D Rigidbody { get; private set; }
    private LineRenderer _lineRenderer;

    // Upgrade: Bounce
    [Header("Upgrade: Bounce")] [SerializeField]
    private PhysicsMaterial2D bulletBouncePhysicsMaterial;

    public int BouncesLeft { get; set; }

    // Upgrade: Piercing
    public int PiercesLeft { get; set; }

    // Upgrade: SinusoidalShots
    public int RotationMultiplier { get; set; }

#if UNITY_EDITOR
    private bool _canSeeTargetCharacterGizmos;
    private Vector2 _targetPositionGizmos;
#endif

    private float _spawnEndTimestamp;

    private void Awake()
    {
        _spawnEndTimestamp = Time.time + Configuration.Bullet_SpawnTime;
        Rigidbody = GetComponent<Rigidbody2D>();
        _lineRenderer = GetComponent<LineRenderer>();
        UpgradeManager.Init(this);
    }

    private void FixedUpdate()
    {
        Lifetime += Time.fixedDeltaTime;
        UpgradeManager.BulletUpdate(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Time.time < _spawnEndTimestamp)
            return;

        if (!UpgradeManager.OnBulletTrigger(this, other))
        {
            Destroy(gameObject);
        }

        ICharacterHealth characterHealth = other.gameObject.GetComponent<ICharacterHealth>();
        if (characterHealth is PlayerHealth)
        {
            if (Time.time >= _spawnEndTimestamp)
            {
                characterHealth?.InflictDamage(Damage * Configuration.Player_SelfDamageMultiplier, true);
            }
        }
        else
        {
            characterHealth?.InflictDamage(Damage, true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!UpgradeManager.OnBulletCollision(this, collision))
        {
            EventManager.OnPlayerBulletDestroyed.Trigger();
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        UpgradeManager.OnBulletDestroy(this);
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
        TotalLifetime = assignedLifetime;
        Rigidbody.velocity = bulletSpeed * transform.up;

        Destroy(gameObject, TotalLifetime);
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