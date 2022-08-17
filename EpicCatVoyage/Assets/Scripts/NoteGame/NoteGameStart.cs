using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NoteGameStart : MonoBehaviour
{
    public void gameStarting()
    {
        SceneManager.LoadScene("NoteGameCountDown");    
    }
}
