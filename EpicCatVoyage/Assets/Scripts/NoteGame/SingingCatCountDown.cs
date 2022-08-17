using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SingingCatCountDown : MonoBehaviour
{
    public Text changingText;
    // Start is called before the first frame update
    void Start()
    {
        changingText.text = "Ready?";
        StartCoroutine(countingDown());
    }


    IEnumerator countingDown()
    {
        yield return new WaitForSeconds(1);
        changingText.text = "3";
        yield return new WaitForSeconds(1);
        changingText.text = "2";
        yield return new WaitForSeconds(1);
        changingText.text = "1";
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("NoteGameMain"); 
    }
}
