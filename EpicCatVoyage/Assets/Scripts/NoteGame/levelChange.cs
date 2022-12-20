using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelChange : MonoBehaviour
{
    public enterNote enterNote;
    public GameObject noteSequenceHolder;
    public static int stage = 1;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        lvl();      
    }
    void lvl()
    {
        if (enterNote.clear == true)
        {
            stage ++;
            print(stage);
            if (stage == 2)
            {
                NoteSequence.waitSeconds = 0.8f;
                NoteSequence.numOfQ = 4;
                enterNote.clear = false;
                Invoke("RestartScript",1f);
            }
            if (stage == 3)
            {
                NoteSequence.waitSeconds = 0.6f;
                NoteSequence.numOfQ = 5;   
                enterNote.clear = false;     
                Invoke("RestartScript",1f);  
            }
            if (stage == 4)
            {
                NoteSequence.waitSeconds = 0.4f;
                NoteSequence.numOfQ = 6;   
                enterNote.clear = false;    
                Invoke("RestartScript",1f); 
            }
            if (stage == 5)
            {
                NoteSequence.waitSeconds = 0.2f;
                NoteSequence.numOfQ = 7;   
                enterNote.clear = false; 
                Invoke("GameClear",1f);
            }
        }
    }

    void RestartScript()
    {
        SceneManager.LoadScene("NoteGameMain");    
    }

    void GameClear()
    {
        SceneManager.LoadScene("NoteGame_Ending");
    }
}
