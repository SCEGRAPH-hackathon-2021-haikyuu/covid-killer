using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING};

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform[] enemies;
        public int count;
        public float rate;

    }

    public Wave[] waves;
    private int nextWave = 0;

    public Transform[] spawnPointsStationary;
    public Transform[] spawnPointsChase;

    public float timeBetweenWaves = 5f;
    private float waveCountdown;

    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.COUNTING;

    private Transform lastSpawn;

    private void Start()
    {
        if (spawnPointsStationary.Length <= 0)
        {
            Debug.LogError("No spawn points referenced");
        }

        waveCountdown = timeBetweenWaves;
    }

    private void Update()
    {
        if (state == SpawnState.WAITING)
        {
            // Check if enemies are still alive
            if (!EnemyIsAlive())
            {
                // Begin a new round
                WaveCompleted();
            }
            else
            {
                // Update will not go past this line if there are still enemies alive
                return;
            }
        }

        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                //Start Spawning Wave
                StartCoroutine(SpawnWave(waves[nextWave]));
            } 
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {
        print("Wave Completed! Moving to next Wave.");
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if(nextWave + 1 > waves.Length - 1)
        {
            print("All waves completed!");
        }
        else
        {
            nextWave++;
        }
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <=0 )
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }

        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        print("Spawning Wave: " + _wave.name);
        state = SpawnState.SPAWNING;

        //Spawn
        for (int i=0; i<_wave.count; i++)
        {
            SpawnEnemy(ChooseEnemy(_wave.enemies));
            yield return new WaitForSeconds(1f/_wave.rate);
        }

        state = SpawnState.WAITING;
        yield break;
    }

    Transform ChooseEnemy(Transform[] e)
    {
        Transform enemy = e[Random.Range(0, e.Length)];
        return enemy;
    }

    void SpawnEnemy(Transform _enemy)
    {
        //Spawn Enemy
        print("Spawning Enemy: " + _enemy.name);

        // Stationary Spawns are Default Spawn Points
        Transform _sp = ChooseSpawn(spawnPointsStationary);

        if (nextWave == 1)
        {
            _sp = ChooseSpawn(spawnPointsChase);
        }
        else if (nextWave == 2)
        {
            int randomNum = Random.Range(1,4);
            if (randomNum % 2 == 0)
            {
                _sp = ChooseSpawn(spawnPointsStationary);
            }
            else
            {
                _sp = ChooseSpawn(spawnPointsChase);
            }
        }

        Instantiate(_enemy, _sp.position, transform.rotation);
    }

    Transform ChooseSpawn(Transform[] spawns)
    {
        int randomIndex = Random.Range(0, spawns.Length);
        Transform sp = spawns[randomIndex];

        if (lastSpawn != null && sp == lastSpawn)
        {
            if (randomIndex + 1 > spawns.Length - 1)
            {
                sp = spawns[0];
            }
            else
            {
                sp = spawns[randomIndex+1];
            }
        }
        lastSpawn = sp;
        return sp;
    }
}
