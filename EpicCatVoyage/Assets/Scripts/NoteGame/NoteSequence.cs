using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteSequence : MonoBehaviour
{
    [HideInInspector]
    public List <string> existingNote = new List <string> ();
    [HideInInspector]
    public List <string> noteSequence = new List <string> ();
    
    private string addedNote;
    public static int numOfQ;
    public static float waitSeconds;

    public GameObject enterNoteHolder;
    public Button[] buttons;
    public GameObject meow;

    void Awake()
    {
        existingNote.Add("Doe");
        existingNote.Add("Rae");
        existingNote.Add("Mi");
        existingNote.Add("Fa");
        existingNote.Add("Sol");
        meow.SetActive(false);
        enterNoteHolder.SetActive(false);
        foreach(Button x in buttons)
        {
            x.enabled = false;
        }
        if (levelChange.stage == 1)
        {
            numOfQ = 5;
            waitSeconds = 1f; 
        }
        Question(numOfQ);
    }

    public void Question(int i)
    {
        for (int x = 0; x < i; x++)
        {
            addedNote = existingNote[Random.Range(0,5)];
            noteSequence.Add(addedNote);
            print(addedNote);
        }
    }

    IEnumerator Start()
    {
        meow.SetActive(true);
        for (int i = 0; i < numOfQ; i++)
        {
            FindObjectOfType<NoteGameAudioManager>().Play(noteSequence[i]);
            yield return new WaitForSeconds (waitSeconds);
        }
        enterNoteHolder.SetActive(true);
        foreach(Button x in buttons)
        {
            x.enabled = true;
        }
        meow.SetActive(false);
    }

}
