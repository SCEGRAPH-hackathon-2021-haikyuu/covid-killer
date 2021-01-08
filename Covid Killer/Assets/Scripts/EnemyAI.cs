using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent enemy;
    private Rigidbody rb;
    [SerializeField]private GameObject[] players;
    public GameObject projectilePrefab;
    public bool followPlayer = false;
    public bool shootPlayer = false;
    public float startTime = 1.5f;
    [SerializeField] private float timeToShoot;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        players = GameObject.FindGameObjectsWithTag("Player");
        timeToShoot = startTime;
    }

    // Update is called once per frame
    void Update()
    {
        players = GameObject.FindGameObjectsWithTag("Player");

        if (players.Length > 0)
        {
            Transform closestPlayer = GetClosestPlayer(players, transform);
            Vector3 lookDir = (closestPlayer.position - transform.position).normalized;
            float angle = Mathf.Atan2(lookDir.x, lookDir.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f,angle,0f);

            if (followPlayer)
            {
                enemy.SetDestination(closestPlayer.position);
            }
        }

        if (shootPlayer && projectilePrefab != null)
        {
            if (timeToShoot <= 0)
            {
                Instantiate(projectilePrefab, transform.position, transform.rotation);
                timeToShoot = startTime;
            }
            else
            {
                timeToShoot -= Time.deltaTime;
            }
        }
    }

    Transform GetClosestPlayer(GameObject[] players, Transform enemy)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = enemy.position;
        foreach (GameObject potentialTarget in players)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget.transform;
            }
        }
        return bestTarget;
    }
}
