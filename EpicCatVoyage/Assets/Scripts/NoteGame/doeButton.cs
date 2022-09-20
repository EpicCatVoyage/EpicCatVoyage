using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doeButton : MonoBehaviour
{
    public AudioSource doePlay;
    public enterNote enterNote;
    public void doeStart()
    {
        print("Doe");
        doePlay.Play();
        enterNote.pressWhat = "Doe";
        enterNote.gameRunning = true;
    }
}
