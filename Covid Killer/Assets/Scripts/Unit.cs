﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int maxHp = 100;
    public int currentHp;

    // Can be left Null for enemies
    public HpBar hpBar;
    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHp;

        if(gameObject.name == "Player1")
        {
            hpBar = GameObject.Find("P1 Health Bar").GetComponent<HpBar>();
        }
        else if (gameObject.name == "Player2")
        {
            hpBar = GameObject.Find("P2 Health Bar").GetComponent<HpBar>();
        }

        if (hpBar != null)
        {
            hpBar.SetMaxHp(maxHp);
        } 
    }

    void Update()
    {
        if (currentHp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDmg(int dmg)
    {
        if (currentHp > 0)
        {
            currentHp -= dmg;
            if (hpBar != null)
                hpBar.SetHp(currentHp);
        }
    }
}
