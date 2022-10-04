using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public GameObject Set;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void buttonStart()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void buttonSetting()
    {
        if (Set.activeSelf == true)
            Set.SetActive(false);
        else
            Set.SetActive(true);
    }
}
