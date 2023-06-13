using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerBullet : MonoBehaviour, IUpgradeableBullet
{
    [SerializeField] private float defaultLifetime = 0.5f;
    [SerializeField] private float defaultBulletSpeed = 10f;
    private float _assignedDamage;
    private GameObject _sourceCharacter;
    private bool _currentlyColliding;
    private Rigidbody2D _rb;

    // Upgrade: Bounce
    [Header("Upgrade: Bounce")] [SerializeField] private PhysicsMaterial2D bulletBouncePhysicsMaterial;
    [SerializeField] private int maxBounces = 2;
    private int _bouncesLeft;

    // Upgrade: Homing
    [Header("Upgrade: Homing")] [SerializeField] private float radius = 5f;
    [SerializeField] [Range(1, 360)] private float angleHalf = 22.5f;
    [SerializeField] private float rotationSpeed = 200f;
    [SerializeField] private LayerMask targetLayer;

#if UNITY_EDITOR
    private bool _canSeeTargetCharacterGizmos;
    private Vector2 _targetPositionGizmos;
#endif

    private void Awake()
    {
        _bouncesLeft = maxBounces;
        _rb = GetComponent<Rigidbody2D>();
        UpgradeManager.Init(this);
        Destroy(gameObject, defaultLifetime * UpgradeManager.GetBulletRangeMultiplier());
    }

    private void Start()
    {
        _rb.velocity = transform.up * defaultBulletSpeed;
    }

    private void FixedUpdate()
    {
        UpgradeManager.BulletUpdate(this);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!_currentlyColliding && !UpgradeManager.OnBulletImpact(this, other))
        {
            Destroy(gameObject);
        }

        // Enemy hit
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyHealth>().InflictDamage(_assignedDamage);
        }
        // Player hits themself
        else if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealth>().InflictDamage(_assignedDamage, true, _sourceCharacter.GetComponent<PlayerController>());
        }

        _currentlyColliding = true;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        _currentlyColliding = false;
    }

    /// <summary>
    /// Initializes this bullet.
    /// </summary>
    /// <param name="assignedDamage">Amount of damage caused by this bullet.</param>
    /// <param name="sourceCharacter">Reference of the character who shot this bullet.</param>
    public void Init(float assignedDamage, GameObject sourceCharacter)
    {
        _assignedDamage = assignedDamage;
        _sourceCharacter = sourceCharacter;
    }


    // Upgrade: Bounce
    public void InitBounce()
    {
        GetComponent<Rigidbody2D>().sharedMaterial = bulletBouncePhysicsMaterial;
    }
    
    public bool ExecuteBounce_OnBulletImpact(Collision2D collision)
    {
        if (_bouncesLeft > 0 && !collision.gameObject.CompareTag("Enemy"))
        {
            _bouncesLeft--;
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
            _rb.angularVelocity = -rotationAmount * rotationSpeed;

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
    }

    /// <summary>
    /// Checks if a target is in the field of view of this bullet
    /// </summary>
    /// <param name="closestTarget">Position of the closest target</param>
    /// <param name="directionToTarget">Direction in which the target is located</param>
    /// <returns>Bool, whether a target is in the field of view of this bullet</returns>
    private bool CheckCharacterInFieldOfView(ref Vector2 closestTarget, ref Vector2 directionToTarget)
    {
        Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);

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

        return Vector2.Angle(transform.up, directionToTarget) < angleHalf /*&& !Physics2D.Raycast(position, directionToTarget, closestDistance)*/;
    }


    // Upgrade: Explosive Bullet
    public bool ExecuteExplosiveBullet_OnBulletImpact(Collision2D collision)
    {
        throw new NotImplementedException();
    }


    // Upgrade: Mental Meltdown
    public bool ExecuteMentalMeltdown_OnBulletImpact(Collision2D collision)
    {
        throw new NotImplementedException();
    }


    // Upgrade: Drill
    public bool ExecuteDrill_OnBulletImpact(Collision2D collision)
    {
        throw new NotImplementedException();
    }

    
#if UNITY_EDITOR //////////////////////////////////////////////////////////////////////////////////
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, radius);

        Vector3 angle01 = DirectionFromAngle(-transform.eulerAngles.z, -angleHalf);
        Vector3 angle02 = DirectionFromAngle(-transform.eulerAngles.z, angleHalf);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + angle01 * radius);
        Gizmos.DrawLine(transform.position, transform.position + angle02 * radius);

        if (_canSeeTargetCharacterGizmos)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, _targetPositionGizmos);
        }
    }

    private Vector2 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;
        return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
#endif ////////////////////////////////////////////////////////////////////////////////////////////
}