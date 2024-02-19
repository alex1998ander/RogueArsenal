using System.Collections.Generic;
using UnityEngine;

public class BossArenaShrinking : MonoBehaviour
{
    [SerializeField] private Transform[] walls;
    private EnemyHealth _bossHealth;

    private void Start()
    {
        _bossHealth = FindObjectOfType<EnemyHealth>();
    }

    private void Update()
    {
        // if (_bossHealth.GetHealth().x * 2 / 3 < _bossHealth.GetHealth().y)
        // {
        //     transform.position = Vector3.Lerp(walls[0].transform.position, transform.position, 1f);
        // }
    }
}