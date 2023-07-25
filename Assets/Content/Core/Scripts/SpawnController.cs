using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnController : MonoBehaviour
{
    [SerializeField] public GameObject allSpawns = null;
    [SerializeField] public GameObject [] enemy;

    private const int SpawnCount = 3;
    private static int _mEnemyCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.OnEnemyDeath.Subscribe(EnemyDied);
        RandomSpawns();
        _mEnemyCount = 0;
    }

    /// <summary>
    /// Sets enemies at random positions in the different rooms
    /// </summary>
    void RandomSpawns()
    {
        Transform[] rooms = allSpawns.GetComponentsInChildren<Transform>();
        for (int i = 0; i < transform.childCount; i++)
        {
            //Debug.Log("Room_" + (i + 1));
            //Debug.Log("Number of Children: " + transform.GetChild(i).childCount);
            
            if (transform.GetChild(i).childCount <= 0) continue;
            
            HashSet<int> spawnPointsRoom = new HashSet<int>();
            for (int j = 0; j < SpawnCount; j++)
            {
                int rndmSpwn = Random.Range(0, transform.GetChild(i).childCount - 1);
                spawnPointsRoom.Add(rndmSpwn);
            }
            
            //Debug.Log("Room_" + (i+1) + " -> Number of spawnPointsRoom: " + spawnPointsRoom.Count);

            //Debug.Log(string.Join(", ", spawnPointsRoom));
            foreach (int spawnPoint in spawnPointsRoom)
            {
                Instantiate(enemy[Random.Range(0,2)], transform.GetChild(i).GetChild(spawnPoint).transform.position,
                    transform.GetChild(i).GetChild(spawnPoint).transform.rotation);
                _mEnemyCount++;
                //transform.GetChild(i).GetChild(spawnPoint).gameObject.SetActive(true);
            }
        }
    }

    private void EnemyDied(GameObject enemy)
    {
        _mEnemyCount--;

    }

    public static bool StillEnemiesLeft()
    {
        Debug.Log(_mEnemyCount > 0);
        return _mEnemyCount > 0;
    }

    private void OnDestroy()
    {
        EventManager.OnEnemyDeath.Unsubscribe(EnemyDied);
    }
}