using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour {
    public GameObject hpPrefab;

    public float movementSpeed;
    public float rotationSpeed;

    public float startTimeShoot = 0.3f;
    private float currentTimeShoot = 0f;
    public GameObject syringePrefab;
    private Vector3 lookDir = new Vector3(0,0,0);

    private void Start()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("HPSpawn"))
        {
            if (g.transform.childCount <= 0)
            {
                GameObject hpBar = Instantiate(hpPrefab, g.transform.position, g.transform.rotation);
                hpBar.transform.parent = g.transform;
                GetComponent<Unit>().SetHpBar(hpBar.GetComponent<HpBar>());
                return;
            }
        }
    }

    // Update is called once per frame
    void Update() {
        if (this.isLocalPlayer)
        {
            // make player face mouse
            Plane playerPlane = new Plane(Vector3.up, transform.position);
            Ray rayline = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
            float hitDist = 0.0f;

            if (playerPlane.Raycast(rayline, out hitDist))
            {
                Vector3 targetPoint = rayline.GetPoint(hitDist);
                Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
                lookDir = (targetPoint - transform.position).normalized;
                targetRotation.x = 0;
                targetRotation.z = 0;
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

            //Player Movement
            if (Input.GetKey(KeyCode.W))
            { //foward
                transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.S))
            { //backpedal
                transform.Translate(Vector3.back * movementSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.A))
            { // strafe left
                transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.D))
            { //strafe right
                transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
            }

            // shooting code
            if (currentTimeShoot <= 0)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    this.CmdShoot();
                }
            }
            else
            {
                currentTimeShoot -= Time.deltaTime;
            }
        }
    }

    [Command]
    void CmdShoot() {
        // shooting code
        GameObject projectile = Instantiate(syringePrefab, transform.position, transform.rotation);
        projectile.GetComponent<Projectile>().SetShootDir(lookDir);
        NetworkServer.Spawn(projectile);
        currentTimeShoot = startTimeShoot;
    }
}
