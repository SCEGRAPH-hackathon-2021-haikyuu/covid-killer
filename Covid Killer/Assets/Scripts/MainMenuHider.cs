using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MainMenuHider : NetworkBehaviour
{
    public void HideMenu()
    {
        gameObject.SetActive(false);
    }
}
