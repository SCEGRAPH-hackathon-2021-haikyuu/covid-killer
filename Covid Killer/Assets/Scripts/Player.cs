using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    // Start is called before the first frame update
    public float movementSpeed;
    public float rotationSpeed;

    // Update is called once per frame
    void Update() {
        // make player face mouse
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray rayline = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitDist = 0.0f;

        if(playerPlane.Raycast(rayline, out hitDist)) {
            Vector3 targetPoint = rayline.GetPoint(hitDist);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            targetRotation.x = 0;
            targetRotation.z = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        //Player Movement
        if(Input.GetKey(KeyCode.W)) {
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
        }

    }
}
