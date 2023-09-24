using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnController : MonoBehaviour
{
    [SerializeField] public GameObject allSpawns = null;
    [SerializeField] public GameObject[] enemy;

    private const int SpawnCount = 3;

    void Start()
    {
        RandomSpawns();
    }

    /// <summary>
    /// Sets enemies at random positions in the different rooms
    /// </summary>
    void RandomSpawns()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).childCount <= 0) continue;

            HashSet<int> spawnPointsRoom = new HashSet<int>();
            for (int j = 0; j < SpawnCount; j++)
            {
                int rndmSpwn = Random.Range(0, transform.GetChild(i).childCount - 1);
                spawnPointsRoom.Add(rndmSpwn);
            }

            foreach (int spawnPoint in spawnPointsRoom)
            {
                Instantiate(enemy[Random.Range(0, 2)], transform.GetChild(i).GetChild(spawnPoint).transform.position,
                    transform.GetChild(i).GetChild(spawnPoint).transform.rotation);
            }
        }
    }

    public static bool StillEnemiesLeft()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length > 0;
    }
}