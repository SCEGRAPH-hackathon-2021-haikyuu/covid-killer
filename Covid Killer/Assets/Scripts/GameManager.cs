using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class GameManager : NetworkBehaviour
{
    public GameObject winScreen;
    public GameObject loseScreen;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetWin()
    {
        winScreen.SetActive(true);
    }

    public void SetLose()
    {
        loseScreen.SetActive(true);
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
