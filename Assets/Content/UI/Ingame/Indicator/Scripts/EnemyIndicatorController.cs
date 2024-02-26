using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class EnemyIndicatorController : MonoBehaviour
{
    public GameObject targetIndicator;
    private readonly List<GameObject> _enemyIndicators = new List<GameObject>();
    private int _currentEnemies = Int32.MaxValue;

    private const int MaxEnemiesToMark = 5;
    private bool _enemiesSpawned = false;

    private void Start()
    {
        EventManager.OnEnemiesSpawned.Subscribe(UpdateEnemieCounter);
    }

    void Update()
    {
        // TODO Change FindGameObjectsWithTag to something efficient
        EnemyBehaviourTree[] enemies = FindObjectsOfType<EnemyBehaviourTree>();

        if (enemies.Length > MaxEnemiesToMark)
            return;

        if (enemies.Length == 0)
        {
            DestroyIndicators();
            return;
        }

        if (enemies.Length < _currentEnemies | _enemiesSpawned)
        {
            int i = 0;
            _currentEnemies = enemies.Length;
            DestroyIndicators();
            foreach (EnemyBehaviourTree enemy in enemies)
            {
                _enemyIndicators.Add(Instantiate(targetIndicator, transform));
                _enemyIndicators[i].GetComponent<TargetIndicator>().SetTarget(enemy.transform);
                i++;
            }

            _enemiesSpawned = false;
        }
    }

    private void DestroyIndicators()
    {
        foreach (GameObject indicator in _enemyIndicators)
        {
            Destroy(indicator);
        }

        _enemyIndicators.Clear();
    }

    void UpdateEnemieCounter()
    {
        _enemiesSpawned = true;
    }
}