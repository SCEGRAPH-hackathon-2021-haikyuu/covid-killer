using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ButtonHandler : NetworkBehaviour
{
    public void Restart()
    {
        foreach (GameObject bullet in GameObject.FindGameObjectsWithTag("Bullet"))
        {
            Destroy(bullet);
        }

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(enemy);
        }
        WaveSpawner waveSpawner = GameObject.Find("WaveManager").GetComponent<WaveSpawner>();
        waveSpawner.nextWave = 0;
        waveSpawner.waveCountdown = waveSpawner.timeBetweenWaves;
        waveSpawner.state = WaveSpawner.SpawnState.COUNTING;

        if (GameObject.FindGameObjectsWithTag("Player").Length > 1)
        {
            foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
            {
                player.GetComponent<Unit>().RpcRespawn();
            }
        }
        else
        {
            foreach (Player i in Resources.FindObjectsOfTypeAll<Player>())
            {
                if (i.gameObject.activeSelf == false)
                {
                    i.GetComponent<Unit>().RpcRespawn();
                }
            }
        }


        Destroy(gameObject);
        /*if (GameObject.FindGameObjectWithTag("WinScreen") != null)
        {
            Destroy(GameObject.FindGameObjectWithTag("WinScreen"));
        }
        else if (GameObject.FindGameObjectWithTag("LoseScreen") != null)
        {
            Destroy(GameObject.FindGameObjectWithTag("LoseScreen"));
        }*/
    }
}
