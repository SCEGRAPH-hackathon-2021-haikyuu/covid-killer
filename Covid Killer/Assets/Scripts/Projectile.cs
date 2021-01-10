using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Projectile : NetworkBehaviour {
   
    private void OnCollisionEnter(Collision col)
    {
        //print(col.gameObject.name);
        if (col.gameObject.GetComponent<Unit>() != null)
        {
            col.gameObject.GetComponent<Unit>().TakeDmg(2);
        }
        Destroy(this.gameObject);
        //print("dead");
    }
}
