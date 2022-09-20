using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miButton : MonoBehaviour
{
    public AudioSource miPlay;
    public enterNote enterNote;
    public void miStart()
    {
        print("Mi");
        miPlay.Play();
        enterNote.pressWhat = "Mi";
        enterNote.gameRunning = true;
    }
}
