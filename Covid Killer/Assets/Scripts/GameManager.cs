using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class GameManager : NetworkBehaviour
{
    public GameObject winScreen;
    public GameObject loseScreen;

    public Transform winPos;
    public Transform losePos;

    bool over = false;

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Player").Length <= 0 && over == false)
        {
            foreach (Player i in Resources.FindObjectsOfTypeAll<Player>())
            {
                if (i.gameObject.activeSelf == false && over == false)
                {
                    SetLose();
                }
            }
        }
    }

    public void SetWin()
    {
        over = true;
        print("Win");
        GameObject winPref = Instantiate(winScreen, winPos.position, winPos.rotation);
        winPref.transform.parent = winPos;
        NetworkServer.Spawn(winPref);
        //winScreen.SetActive(true);
    }

    public void SetLose()
    {
        over = true;
        print("Lose");
        GameObject losePref = Instantiate(loseScreen, losePos.position, losePos.rotation);
        losePref.transform.parent = losePos;
        NetworkServer.Spawn(losePref);
        //loseScreen.SetActive(true);
    }

    public void Menu()
    {
        if (this.isServer)
        {
            GameObject.Find("Network Manager").GetComponent<NetworkManager>().StopHost();
        }
        else if (this.isClientOnly)
        {
            GameObject.Find("Network Manager").GetComponent<NetworkManager>().StopClient();
        }
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
