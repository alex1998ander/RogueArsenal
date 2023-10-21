using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private GameObject allSpawns = null;
    [SerializeField] private GameObject[] enemyPrefabs;

    private const float EnemySpawnFillrateIncreasePerDifficultyLevelInPercent = 0.035f;
    private const float BaseEnemySpawnFillrateInPercent = 0.5f;

    // private const int SpawnCount = 3;

    void Start()
    {
        SpawnEnemies();
    }

    /// <summary>
    /// Sets enemies at random positions in the different rooms
    /// </summary>
    void SpawnEnemies()
    {
        // Calculate how many percent of the available spawn points are used to spawn enemies depending on the current difficulty level (Cap at 100 percent)
        float spawnFillrate = Mathf.Min(1f, BaseEnemySpawnFillrateInPercent + EnemySpawnFillrateIncreasePerDifficultyLevelInPercent * ProgressionManager.DifficultyLevel);

        // First count the number of spawn points
        int totalSpawnpoints = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform roomTransform = transform.GetChild(i);
            for (int j = 0; j < roomTransform.childCount; j++)
            {
                totalSpawnpoints++;
            }
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform roomTransform = transform.GetChild(i);

            // Calculate how many enemies should be spawned (Can't exceed the number of spawn points)
            int spawnCount = Mathf.RoundToInt(roomTransform.childCount * spawnFillrate);

            // Get all spawn points
            List<Transform> allSpawnpointTransforms = new List<Transform>();
            foreach (Transform spawnpointTransform in roomTransform)
            {
                allSpawnpointTransforms.Add(spawnpointTransform);
            }

            List<Transform> randomSpawnpointTransforms = allSpawnpointTransforms.OrderBy(x => Random.Range(0, int.MaxValue)).Take(spawnCount).ToList();

            foreach (Transform spawnPointTransform in randomSpawnpointTransforms)
            {
                Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], spawnPointTransform.position, Quaternion.identity);
            }
        }
    }

    public static bool CheckEnemiesAlive()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length > 0;
    }
}