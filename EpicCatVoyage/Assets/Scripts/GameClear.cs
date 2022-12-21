using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClear : MonoBehaviour
{
    public GameObject window;

    // Start is called before the first frame update
    void Start()
    {
        window.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void fin()
    {
        window.SetActive(true);
    }
}
