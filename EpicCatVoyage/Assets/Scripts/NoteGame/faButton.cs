using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class faButton : MonoBehaviour
{
    public AudioSource faPlay;
    public enterNote enterNote;
    public void faStart()
    {
        print("Fa");
        faPlay.Play();
        enterNote.pressWhat = "Fa";
        enterNote.gameRunning = true;
    }
}
