using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {
    public Transform player;
    public float smooth = 0.3f;
    public float height;
    public float distance;
    private Vector3 velocity = Vector3.zero;

    void Update() {
        if (player != null)
        {
            Vector3 pos = new Vector3();
            pos.x = player.position.x;
            pos.z = player.position.z - distance;
            pos.y = player.position.y + height;
            this.transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, smooth);
            this.transform.LookAt(player.position);
        }
    }

    public void SetPlayer(Transform p)
    {
        player = p;
    }
}
