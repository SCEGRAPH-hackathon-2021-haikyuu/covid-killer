using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMountPoint : MonoBehaviour
{
    public GameObject MountPoint;

    void Start()
    {
        if (transform.parent.GetComponent<Player>().isLocalPlayer)
        {
            transform.parent.name = "Player" + Random.Range(0.0f, 1.0f);
            Transform cameraTransform = UnityEngine.Camera.main.gameObject.transform;  //Find main camera which is part of the scene instead of the prefab
            /*cameraTransform.parent = MountPoint.transform;  //Make the camera a child of the mount point
            cameraTransform.position = MountPoint.transform.position;  //Set position/rotation same as the mount point
            cameraTransform.rotation = MountPoint.transform.rotation;*/

            cameraTransform.gameObject.GetComponent<Camera>().SetPlayer(transform.parent);
        }
    }
}
