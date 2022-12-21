using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Clear_1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("StageSelect", 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StageSelect()
    {
        SceneManager.LoadScene("StageChoice");
    }
}
