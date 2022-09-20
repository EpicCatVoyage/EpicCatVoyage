using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raeButton : MonoBehaviour
{
    public AudioSource raePlay;
    public enterNote enterNote;
    public void raeStart()
    {
        print("Rae");
        raePlay.Play();
        enterNote.pressWhat = "Rae";
        enterNote.gameRunning = true;
    }
}
