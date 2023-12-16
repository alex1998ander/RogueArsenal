using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Content.Core.Scripts
{
    public class TutorialTurret: MonoBehaviour
    {
        [SerializeField] EnemyWeapon weapon;
        private readonly float _weaponCooldown = 0.15f;
        private void Start()
        {
            StartCoroutine(Shoot());
        }

        IEnumerator Shoot()
        {
            yield return new WaitForSeconds(_weaponCooldown);
            weapon.Fire();
            EventManager.OnEnemyShotFired.Trigger();
            StartCoroutine(Shoot());
        }
    }
}