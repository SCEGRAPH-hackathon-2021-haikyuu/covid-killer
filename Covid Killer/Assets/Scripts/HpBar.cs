using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class HpBar : NetworkBehaviour
{
    public Slider slider;

    public void SetMaxHp(int hp)
    {
        slider.maxValue = hp;
        slider.value = hp;
    }

    public void SetHp(int hp)
    {
        slider.value = hp;
    }
}
