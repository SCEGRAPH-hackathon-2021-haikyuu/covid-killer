using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Unit : NetworkBehaviour
{
    public int maxHp = 100;

    [SyncVar]
    public int currentHp;

    // Can be left Null for enemies
    public HpBar hpBar;

    private Vector3 initialPosition;
    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHp;

        if (hpBar != null)
        {
            hpBar.SetMaxHp(maxHp);
        }

        initialPosition = transform.position;
    }

    /*
    void Update()
    {
        if (this.currentHp <= 0)
        {
            if (gameObject.CompareTag("Player"))
            {
                gameObject.SetActive(false);
                //currentHp = maxHp;
                //RpcRespawn();
            }
            else
            {
                Destroy(gameObject);
            }
            if (this.isClientOnly)
            {
                GameObject.Find("Network Manager").GetComponent<NetworkManager>().StopClient();
            }
            GameObject.Find("Game Manager").GetComponent<GameManager>().SetLose();*/
            
            /*if (transform.parent.GetComponent<Player>().isLocalPlayer)
            {
                Destroy(gameObject);
                GameObject Otherplayer = GameObject.FindGameObjectWithTag("Player");
                if(Otherplayer!= null)
                {
                    Transform cameraTransform = UnityEngine.Camera.main.gameObject.transform;
                    cameraTransform.gameObject.GetComponent<Camera>().SetPlayer(Otherplayer.transform);
                }
            }
        }
    } 
    */

    [ClientRpc]
    public void RpcRespawn()
    {
        gameObject.SetActive(true);
        currentHp = maxHp;
        hpBar.SetHp(currentHp);
        transform.position = initialPosition;
    }

    public void TakeDmg(int dmg)
    {
        if(this.isServer) {
            this.currentHp -= dmg;
            if (hpBar != null)
                hpBar.SetHp(currentHp);
            if(this.currentHp <= 0) {
                Destroy(this.gameObject);
            }
        }
    }

    public void SetHpBar(HpBar bar)
    {
        hpBar = bar;
    }
}
