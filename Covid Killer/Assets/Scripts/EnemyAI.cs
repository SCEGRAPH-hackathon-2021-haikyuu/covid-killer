using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent enemy;
    public Transform player1;
    public Transform player2;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        if (GameObject.Find("Player1") != null)
        {
            player1 = GameObject.Find("Player1").GetComponent<Transform>();
        }

        if (GameObject.Find("Player2") != null)
        {
            player2 = GameObject.Find("Player2").GetComponent<Transform>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
