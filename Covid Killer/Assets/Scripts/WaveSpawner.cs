using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class WaveSpawner : NetworkBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING};

    public GameObject music;

    [System.Serializable]
    public class Wave
    {
        public string name;
        public GameObject[] enemies;
        public int count;
        public float rate;

    }

    public Wave[] waves;
    private bool musicStarted = false;
    public int nextWave = 0;

    public Transform[] spawnPointsStationary;
    public Transform[] spawnPointsChase;

    public float timeBetweenWaves = 5f;
    public float waveCountdown;

    private float searchCountdown = 1f;

    public SpawnState state = SpawnState.COUNTING;

    private Transform lastSpawn;

    private void OnStartServer()
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
            //nextWave = 0;
            /*GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().SetWin();
            return;*/
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

    GameObject ChooseEnemy(GameObject[] e)
    {
        GameObject enemy = e[Random.Range(0, e.Length)];
        return enemy;
    }

    void SpawnEnemy(GameObject _enemy)
    {
        //Spawn Enemy
        print("Spawning Enemy: " + _enemy.name);

        // Stationary Spawns are Default Spawn Points
        Transform _sp = ChooseSpawn(spawnPointsStationary);
        if(nextWave == 0 && !musicStarted) {
            this.playMusic();
        }

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

        GameObject enemy = Instantiate(_enemy, _sp.position, transform.rotation);
        NetworkServer.Spawn(enemy);
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

    [ClientRpc]
    void playMusic() {
        music.GetComponent<Music>().playBattleMusic();
        musicStarted = true;
    }
}
