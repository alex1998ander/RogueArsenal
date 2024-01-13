using UnityEngine;

namespace Content.Characters.Enemy.Scripts
{
    public class EnemyBounceBullet: MonoBehaviour
    {
        [SerializeField] private float defaultBulletSpeed = 10f;

        private Rigidbody2D _rb;

        private float _assignedDamage;
        private float _assignedDistance;

        private int _bouncesLeft;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }
        
        private void Start()
        {
            _rb.velocity = transform.up * defaultBulletSpeed;
            Destroy(gameObject, _assignedDistance / defaultBulletSpeed);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            ICharacterHealth characterHealth = other.GetComponentInParent<ICharacterHealth>();
            if (!(characterHealth is PlayerHealth))
            {
                characterHealth?.InflictDamage(_assignedDamage, true);
            }
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            Destroy(gameObject);
            EventManager.OnEnemyBulletDestroyed.Trigger();
        }
        
        /// <summary>
        /// Initializes this bullet.
        /// </summary>
        /// <param name="assignedDamage">Amount of damage caused by this bullet.</param>
        /// <param name="sourceCharacter">Reference of the character who shot this bullet.</param>
        public void Init(float assignedDamage, float assignedDistance, GameObject sourceCharacter)
        {
            _assignedDamage = assignedDamage;
            _assignedDistance = assignedDistance;
        }
    }
}