using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour {
    // Start is called before the first frame update
    public float movementSpeed;
    public float rotationSpeed;

    public float startTimeShoot = 0.3f;
    private float currentTimeShoot = 0f;
    public GameObject syringePrefab;
    private Vector3 lookDir = new Vector3(0,0,0);

    // Update is called once per frame
    void FixedUpdate() {
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

            if (currentTimeShoot <= 0)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    GameObject projectile = Instantiate(syringePrefab, transform.position, transform.rotation);
                    projectile.GetComponent<Projectile>().SetShootDir(lookDir);
                    currentTimeShoot = startTimeShoot;
                }
            }
            else
            {
                currentTimeShoot -= Time.deltaTime;
            }
        }
    }
}
