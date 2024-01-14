using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIndicatorController : MonoBehaviour
{
    public GameObject targetIndicator;
    private readonly List<GameObject> _enemyIndicators = new List<GameObject>();
    private GameObject[] _enemyPoints;
    private int _currentEnemies = 2000000;

    void Update()
    {
        // TODO Change FindGameObjectsWithTag to something efficient
        int enemyTagLength = GameObject.FindGameObjectsWithTag("Enemy").Length;
        
        //Debug.Log("Number of Enemies: " + enemyTagLength);
        if (enemyTagLength > 6) return;
        if (enemyTagLength < _currentEnemies)
        {
            int i = 0;
            _currentEnemies = enemyTagLength;
            DestroyIndicators();
            _enemyPoints = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemyPoint in _enemyPoints)
            {
                _enemyIndicators.Add(Instantiate(targetIndicator, transform));
                _enemyIndicators[i].GetComponent<TargetIndicator>().SetTarget(enemyPoint.transform);
                i++;
            }
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
}
