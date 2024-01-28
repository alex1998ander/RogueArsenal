using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private GameObject[] eliteEnemyPrefabs;
    [SerializeField] private bool respawnEnemiesIndefinitely;

    private static SpawnController _instance;

    // In percent
    private const float EnemySpawnRateIncreasePerDifficulty = 0.047f;

    // In percent
    private const float BaseEnemySpawnRate = 0.3f;

    // If calculated spawn rate exceeds this threshold, start spawning elite enemies instead. At calculated spawn rate reaches 100%, only spawn elites.
    private const float EliteSpawnRateThreshold = 0.3f;

    private readonly List<List<Transform>> _spawnPointCollections = new();
    private readonly List<Transform> _allSpawnPoints = new();

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
        //SpawnEnemiesAtSpawnPointCollection(_allSpawnPoints, spawnCount);
    }

    public static void SpawnEnemies()
    {
        _instance.SpawnEnemies(BaseEnemySpawnRate + EnemySpawnRateIncreasePerDifficulty * ProgressionManager.DifficultyLevel);
    }

    public void SpawnEnemies(float fillrate)
    {
        float eliteEnemyFillrate = Mathf.InverseLerp(EliteSpawnRateThreshold, 1f, fillrate);

        // Subtract eliteEnemyFillrate to gradually replace normal enemies with elites
        float baseEnemyFillrate = Mathf.Clamp(fillrate, 0f, 1f) - eliteEnemyFillrate;
        
        Debug.Log("<color=yellow>baseEnemyFillrate: " + baseEnemyFillrate + "</color>");
        Debug.Log("<color=green>eliteEnemyFillrate: " + eliteEnemyFillrate + "</color>");

        foreach (List<Transform> spawnPointCollection in _spawnPointCollections)
        {
            int baseEnemySpawnCount = Mathf.RoundToInt(spawnPointCollection.Count * baseEnemyFillrate);
            int eliteEnemySpawnCount = Mathf.RoundToInt(spawnPointCollection.Count * eliteEnemyFillrate);
            List<Transform> randomSpawnPoints = spawnPointCollection.OrderBy(x => Random.Range(0, int.MaxValue)).Take(baseEnemySpawnCount + eliteEnemySpawnCount).ToList();

            int spawnPointIndex = 0;
            for (int i = 0; i < baseEnemySpawnCount; i++)
                Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], randomSpawnPoints[spawnPointIndex++].position, Quaternion.identity, null);

            for (int i = 0; i < eliteEnemySpawnCount; i++)
                Instantiate(eliteEnemyPrefabs[Random.Range(0, eliteEnemyPrefabs.Length)], randomSpawnPoints[spawnPointIndex++].position, Quaternion.identity, null);
        }

        EventManager.OnEnemyDeath.Subscribe(OnEnemyDeath);
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