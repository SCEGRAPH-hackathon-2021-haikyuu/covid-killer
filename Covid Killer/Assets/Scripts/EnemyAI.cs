using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Mirror;

public class EnemyAI : NetworkBehaviour
{
    private NavMeshAgent enemy;
    [SerializeField]private GameObject[] players;
    public GameObject projectilePrefab;
    public bool followPlayer = false;
    public bool shootPlayer = false;
    public float startTimeShoot = 1.5f;
    [SerializeField] private float timeToShoot = 0;

    public int dmgTouch = 10;
    public float startTimeTouch = 1f;
    [SerializeField] private float timeToTouch = 0;

    private Vector3 lookDir = new Vector3(0,0,0);

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        players = GameObject.FindGameObjectsWithTag("Player");
        timeToShoot = startTimeShoot;
    }

    // Update is called once per frame
    void Update()
    {
        players = GameObject.FindGameObjectsWithTag("Player");

        if (players.Length > 0)
        {
            Transform closestPlayer = GetClosestPlayer(players, transform);
            lookDir = (closestPlayer.position - transform.position).normalized;
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
                print("enemy hi");
                CmdShoot();
            }
            else
            {
                timeToShoot -= Time.deltaTime;
            }
        }
    }

    Transform GetClosestPlayer(GameObject[] p, Transform e)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = e.position;
        foreach (GameObject potentialTarget in p)
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

    private void OnCollisionStay(Collision col)
    {
        if (timeToTouch <= 0)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                col.gameObject.GetComponent<Unit>().TakeDmg(dmgTouch);
                timeToTouch = startTimeTouch;
            }
        }
        else
        {
            timeToTouch -= Time.deltaTime;
        }
    }

    //[Command]
    void CmdShoot(){
        // shooting code
        print("enemy Shoot");
        GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        projectile.GetComponent<Projectile>().SetShootDir(lookDir);
        NetworkServer.Spawn(projectile);
        timeToShoot = startTimeShoot;
    }
}
