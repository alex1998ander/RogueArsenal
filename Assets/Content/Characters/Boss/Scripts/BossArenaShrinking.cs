using System;
using UnityEngine;

namespace Content.Characters.Boss.Scripts
{
    public class BossArenaShrinking: MonoBehaviour
    {
        [SerializeField] private Transform[] walls;
        private GameObject _bossEnemy;
        private EnemyHealth _bossHealth;
        private void Start()
        {
            _bossEnemy = GameObject.Find("BossEnemy");
            _bossHealth = FindObjectOfType<EnemyHealth>();
        }

        private void Update()
        {
            _bossEnemy.GetComponent<EnemyHealth>();
            if (_bossHealth.GetHealth().x * 2 / 3 < _bossHealth.GetHealth().y)
            {
                transform.position = Vector3.Lerp(walls[0].transform.position, transform.position, 1f );
            }
        }
    }
}