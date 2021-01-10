using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Music : MonoBehaviour
{
    public AudioSource menuMusic;
    public AudioSource battleMusic;
    public AudioSource defeatMusic;
    public AudioSource victoryMusic;
    
    // Start is called before the first frame update
    void Start()
    {
        playMenuMusic();
    }


    public void playMenuMusic() {
        battleMusic.Stop();
        defeatMusic.Stop();
        victoryMusic.Stop();
        menuMusic.Play();
    }

    public void playBattleMusic() {
        defeatMusic.Stop();
        victoryMusic.Stop();
        menuMusic.Stop();
        battleMusic.Play();
    }

    public void playDefeatMusic() {
        victoryMusic.Stop();
        menuMusic.Stop();
        battleMusic.Stop();
        defeatMusic.Play();
    }

    public void playVictoryMusic() {
        menuMusic.Stop();
        battleMusic.Stop();
        defeatMusic.Stop();
        victoryMusic.Play();
    }
}
