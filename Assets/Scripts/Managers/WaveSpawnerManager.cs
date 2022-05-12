using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawnerManager : MonoBehaviour
{

    public Wave[] waves;
    public int nextWave = 0;

    [SerializeField] float timeBetweenWaves = 5f;
    [SerializeField] float waveCountdown;
    private float timeBetweenCheckingEnemies = 1f;

    private enum SpawningStates { Spawning, Waiting, Counting}
    private SpawningStates state;
    [SerializeField] Transform[] spawnPoints;
    private bool wavesComplete = false;

    // Start is called before the first frame update
    void Start()
    {
        waveCountdown = timeBetweenWaves;
        state = SpawningStates.Counting;
    }

    // Update is called once per frame
    void Update()
    {
        //if(state == SpawningStates.Waiting)
        //{
        //    if (!EnemiesAreAlive())
        //    {
        //        StartNewWave();
        //    }
        //    else
        //    {
        //        return;
        //    }
        //}

        //if (!wavesComplete)
        //{
        //    if(waveCountdown <= 0)
        //    {
        //        if(state != SpawningStates.Spawning)
        //        {
        //            StartCoroutine(SpawnWave(waves[nextWave]));
        //        }
        //    }
        //    else
        //    {
        //        waveCountdown -= Time.deltaTime;
        //    }

        //}
    }

    IEnumerator SpawnWave(Wave waveToSpawn)
    {
        state = SpawningStates.Spawning;

        for(int i = 0; i < waveToSpawn.amountOfEnemies; i++)
        {
            int randomEnemyNumber = Random.Range(0, waveToSpawn.enemies.Length);
            SpawnEnemy(waveToSpawn.enemies[randomEnemyNumber]);
            
            yield return new WaitForSeconds(waveToSpawn.spawnDelay);
        }

        state = SpawningStates.Waiting;

    }

    void SpawnEnemy(GameObject enemyToSpawn)
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemyToSpawn, spawnPoint.position, spawnPoint.rotation);
    }

    private bool EnemiesAreAlive()
    {
        timeBetweenCheckingEnemies -= Time.deltaTime;

        if(timeBetweenCheckingEnemies <= 0)
        {
            timeBetweenCheckingEnemies = 1f;

            if (FindObjectsOfType<EnemyController>().Length == 0)
            {
                return false;
            }

        }
            return true;        
    }

    //void StartNewWave()
    //{
    //    state = SpawningStates.Counting;

    //    waveCountdown = timeBetweenWaves;

    //    if(nextWave + 1 == waves.Length)
    //    {
    //        wavesComplete = true;
    //        LevelManager.instance.LevelPicker();
    //    }
    //    else
    //    {
    //        nextWave++;
    //    }
    //}

}

[System.Serializable]

public class Wave
{
    public string name;
    public GameObject[] enemies;
    public int amountOfEnemies;
    public float spawnDelay;
}
