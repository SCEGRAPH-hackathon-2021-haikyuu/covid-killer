using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Projectile : NetworkBehaviour {
    //public float speedForce = 20f;
    private Rigidbody rb;

    private Vector3 shootDir;
    public int damage = 2;

    // Start is called before the first frame update
    void Awake()
    {
        if (gameObject.GetComponent<Rigidbody>() != null)
            rb = gameObject.GetComponent<Rigidbody>();

        /*if (rb != null)
            rb.AddForce(transform.forward * speedForce, ForceMode.Impulse);*/
    }
    /*
    private void Update()
    {
        transform.position += shootDir * speedForce * Time.deltaTime;
    }

    public void SetShootDir(Vector3 shootDir)
    {
        this.shootDir = shootDir;
    }
    */
    private void OnCollisionEnter(Collision col)
    {
        //print(col.gameObject.name);
        if (col.gameObject.GetComponent<Unit>() != null)
        {
            col.gameObject.GetComponent<Unit>().TakeDmg(damage);
        }
        Destroy(gameObject);
        //print("dead");
    }
}
