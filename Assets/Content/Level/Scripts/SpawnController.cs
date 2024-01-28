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
    private const float EliteSpawnRateThreshold = 0.5f;

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
        float enemyFillrate = Mathf.Clamp(fillrate, 0f, 1f);
        float elitePercentage = Mathf.InverseLerp(EliteSpawnRateThreshold, 1f, fillrate);

        Debug.Log("<color=green>total spawn points: " + _allSpawnPoints.Count + "</color>");
        Debug.Log("<color=yellow>enemyFillrate: " + enemyFillrate + "</color>");
        Debug.Log("<color=cyan>elitePercentage: " + elitePercentage + "</color>");

        // Go over spawn point collections, create collection subsets, then accumulate subsets to ensure at least one enemy in every room.
        List<Transform> randomSpawnPoints = new();
        foreach (List<Transform> spawnPointCollection in _spawnPointCollections)
        {
            int spawnPointCount = Mathf.RoundToInt(spawnPointCollection.Count * enemyFillrate);
            List<Transform> spawnPointCollectionRandomSubset = spawnPointCollection.OrderBy(x => Random.Range(0, int.MaxValue)).Take(spawnPointCount).ToList();
            randomSpawnPoints.AddRange(spawnPointCollectionRandomSubset);
        }

        Debug.Log("<color=red>randomSpawnPoints: " + randomSpawnPoints.Count + "</color>");

        int baseEnemySpawnCount = Mathf.RoundToInt(randomSpawnPoints.Count * (1f - elitePercentage));

        List<Transform> baseEnemySpawnPoints = randomSpawnPoints.Take(baseEnemySpawnCount).ToList();
        // All spawn points except the ones used for base enemies are the elite spawn points
        List<Transform> eliteEnemySpawnPoints = randomSpawnPoints.Except(baseEnemySpawnPoints).ToList();

        Debug.Log("<color=yellow>baseEnemySpawnPoints: " + baseEnemySpawnPoints.Count + "</color>");
        Debug.Log("<color=cyan>eliteEnemySpawnPoints: " + eliteEnemySpawnPoints.Count + "</color>");

        foreach (Transform spawnPoint in baseEnemySpawnPoints)
            Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], spawnPoint.position, Quaternion.identity, null);
        foreach (Transform spawnPoint in eliteEnemySpawnPoints)
            Instantiate(eliteEnemyPrefabs[Random.Range(0, eliteEnemyPrefabs.Length)], spawnPoint.position, Quaternion.identity, null);

        EventManager.OnEnemyDeath.Subscribe(OnEnemyDeath);
    }

    public static bool CheckEnemiesAlive()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length > 0;
    }

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