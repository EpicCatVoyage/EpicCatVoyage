using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enterNote : MonoBehaviour
{
    public NoteSequence NoteSequence;
    public string pressWhat;
    public bool gameRunning;
    public static bool clear;

    void OnEnable()
    {
        gameRunning = false;
        clear = false;
    }

    void Update()
    {
        if (gameRunning == true) 
        {
            gameMotherBoard();
        }
        gameRunning = false;
    }

    void gameMotherBoard()
    {
        Compare(pressWhat);
        if (NoteSequence.noteSequence.Count == 0)
        {
            clear = true;
            print("Done");
        }
    }

    void Compare(string answer)
    {
        if (answer == NoteSequence.noteSequence[0] && NoteSequence.noteSequence.Count != 0)
        {
            NoteSequence.noteSequence.Remove(NoteSequence.noteSequence[0]);        
            print("Right");
        }
        else if (answer != NoteSequence.noteSequence[0] && NoteSequence.noteSequence.Count != 0)
        {
            print("Wrong");
            Invoke("Fail",1f);
        }
    }  

    void Fail()
    {
        SceneManager.LoadScene("NoteGame_Ending");
    }

}
