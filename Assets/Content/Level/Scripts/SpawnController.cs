using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private GameObject player;
    [SerializeField] private bool spawnAtStart = true;
    [SerializeField] private bool respawnEnemiesIndefinitely = false;

    private static SpawnController _instance;
    
    private const float EnemySpawnFillrateIncreasePerDifficultyLevelInPercent = 0.035f;
    private const float BaseEnemySpawnFillrateInPercent = 0.5f;

    private readonly List<List<Transform>> _spawnPointCollections = new List<List<Transform>>();
    private readonly List<Transform> _allSpawnPoints = new List<Transform>();


    private void Awake()
    {
        _instance = this;
        
        // Get the different spawn point collections inside the level.
        // This assumes the following structure inside the hierarchy:
        // - SpawnController (GameObject with this script)
        //     - Room.1
        //         - SpawnPoint
        //         - SpawnPoint
        //         - ...
        //     - Room.2
        //         - SpawnPoint
        //         - SpawnPoint
        //         - ...
        //     - ...
        
        foreach (Transform roomTransform in transform)
        {
            List<Transform> spawnPointCollection = new List<Transform>();
            foreach (Transform spawnPointTransform in roomTransform)
            {
                spawnPointCollection.Add(spawnPointTransform);
                _allSpawnPoints.Add(spawnPointTransform);
            }

            _spawnPointCollections.Add(spawnPointCollection);
        }
    }

    /// <summary>
    /// SANDBOX ONLY
    /// </summary>
    /// <param name="spawnCount"></param>
    public void SpawnEnemies(int spawnCount)
    {
        SpawnEnemiesAtSpawnPointCollection(_allSpawnPoints, spawnCount);
    }
    
    public static void SpawnEnemies()
    {
        _instance.SpawnEnemies(BaseEnemySpawnFillrateInPercent + EnemySpawnFillrateIncreasePerDifficultyLevelInPercent * ProgressionManager.DifficultyLevel);
    }
    

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fillrate"></param>
    public void SpawnEnemies(float fillrate)
    {
        
        float spawnFillrate = Mathf.Clamp(fillrate, 0f, 1f);

        foreach (List<Transform> spawnPointCollection in _spawnPointCollections)
        {
            // Calculate how many spawn points should be used
            int spawnCount = Mathf.RoundToInt(spawnPointCollection.Count * spawnFillrate);

            SpawnEnemiesAtSpawnPointCollection(spawnPointCollection, spawnCount);
        }
        
        EventManager.OnEnemyDeath.Subscribe(OnEnemyDeath);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="spawnPointCollection"></param>
    /// <param name="spawnCount"></param>
    private void SpawnEnemiesAtSpawnPointCollection(List<Transform> spawnPointCollection, int spawnCount)
    {
        // Get random collection of spawn points
        List<Transform> randomSpawnpointTransforms = spawnPointCollection.OrderBy(x => Random.Range(0, int.MaxValue)).Take(spawnCount).ToList();

        foreach (Transform spawnPointTransform in randomSpawnpointTransforms)
        {
            GameObject spawnedEnemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], spawnPointTransform.position, Quaternion.identity, null);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static bool CheckEnemiesAlive()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length > 0;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="position"></param>
    private void OnEnemyDeath(Vector3 position)
    {
        if (respawnEnemiesIndefinitely)
            SpawnEnemies(1);
    }

    private void OnDestroy()
    {
        EventManager.OnEnemyDeath.Unsubscribe(OnEnemyDeath);
    }
}