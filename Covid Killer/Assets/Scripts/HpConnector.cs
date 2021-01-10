using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpConnector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.name == "P1 Health Bar")
        {
            if (GameObject.Find("Player1"))
            {
                if (GameObject.Find("Player1").GetComponent<Unit>().hpBar == null)
                {
                    GameObject.Find("Player1").GetComponent<Unit>().SetHpBar(GetComponent<HpBar>());
                }
            }
        }
    }
}
