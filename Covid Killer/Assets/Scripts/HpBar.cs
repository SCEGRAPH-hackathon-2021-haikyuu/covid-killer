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
        this.slider.maxValue = hp;
        this.slider.value = hp;
    }

    public void SetHp(int hp)
    {
        this.slider.value = hp;
    }
}
