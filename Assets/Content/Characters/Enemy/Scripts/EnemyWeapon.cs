using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] private GameObject enemyBulletPrefab;
    [SerializeField] private Transform firePoint;

    [SerializeField] protected Animator muzzleFlashAnimator;
    [SerializeField] protected Light2D muzzleFlashLight;
    [SerializeField] protected float muzzleFlashLightIntensity;

    [SerializeField] private float defaultBulletSpeed = 6f;
    [SerializeField] private float defaultDistance = 20f;
    [SerializeField] private float defaultDamage = 35f;

    [SerializeField] private int bulletCount = 1;
    [SerializeField] private float weaponSpray = 0f;

    private static readonly int Shoot = Animator.StringToHash("Shoot");

    public void Fire()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            GameObject bullet = Instantiate(enemyBulletPrefab, firePoint.position, firePoint.rotation);
            bullet.transform.Rotate(Vector3.forward, Random.Range(-weaponSpray, weaponSpray));
            bullet.GetComponent<EnemyBullet>().Init(defaultDamage, defaultDistance, defaultBulletSpeed, transform.parent.gameObject);
        }

        muzzleFlashAnimator.SetTrigger(Shoot);
        muzzleFlashLight.intensity = muzzleFlashLightIntensity;
    }
}