using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class solButton : MonoBehaviour
{
    public AudioSource solPlay;
    public enterNote enterNote;
    public void solStart()
    {
        print("Sol");
        solPlay.Play();
        enterNote.pressWhat = "Sol";
        enterNote.gameRunning = true;
    }
}
