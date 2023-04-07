using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves;

    public Transform[] spawnpoints;

    [Header("Info")]
    public float nextSpawnTime;

    public int currentWave = 0;

    public bool randomWave;

    // Start is called before the first frame update
    void Start()
    {
        nextSpawnTime = waves[currentWave].secondsBeforeWave;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (GameController.Instance.gameStarted && Time.time >= nextSpawnTime)
        {
            SpawnWave();
            IncrementWave();

            nextSpawnTime = Time.time + waves[currentWave].secondsBeforeWave;
        }*/
        if (Time.time >= nextSpawnTime)
        {
            SpawnWave();
            IncrementWave();

            nextSpawnTime = Time.time + waves[currentWave].secondsBeforeWave;
        }
    }

    public void SpawnWave()
    {
        for (int i = 0; i < waves[currentWave].numberToSpawn; i++)
        {
            int enemyIndex = Random.Range(0, waves[currentWave].enemies.Length);
            int randomPoint = Random.Range(0, spawnpoints.Length);

            Instantiate(waves[currentWave].enemies[enemyIndex], spawnpoints[randomPoint].position,
                spawnpoints[randomPoint].rotation, gameObject.transform.parent);
        }
    }

    public void IncrementWave()
    {
        if (randomWave)
        {
            currentWave = Random.Range(0, waves.Length);
            return;
        }
        
        if (currentWave + 1 < waves.Length)
        {
            currentWave++;
        }
        else
        {
            randomWave = true;
        }
    }

    public void ReInitialize()
    {
        currentWave = 0;
        nextSpawnTime = waves[currentWave].secondsBeforeWave;
        randomWave = false;
    }
}
