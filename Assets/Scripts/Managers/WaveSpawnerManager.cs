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

    // Start is called before the first frame update
    void Start()
    {
        waveCountdown = timeBetweenWaves;
        state = SpawningStates.Counting;
    }

    // Update is called once per frame
    void Update()
    {
        if(state == SpawningStates.Waiting)
        {
            if (!EnemiesAreAlive())
            {
                Debug.Log("Wave Complete!");
            }
            else
            {
                return;
            }
        }

        if(waveCountdown <= 0)
        {
            if(state != SpawningStates.Spawning)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
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
        Debug.Log("Spawning the enemy " + enemyToSpawn.name);
        Instantiate(enemyToSpawn, transform.position, transform.rotation);
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

}

[System.Serializable]

public class Wave
{
    public string name;
    public GameObject[] enemies;
    public int amountOfEnemies;
    public float spawnDelay;
}
