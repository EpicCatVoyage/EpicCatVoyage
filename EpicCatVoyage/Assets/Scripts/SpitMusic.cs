using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitMusic : MonoBehaviour
{
    GameObject removeMusic;
    void Start()
    {
        removeMusic = GameObject.Find("MusicHolder");
        Destroy(removeMusic);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
