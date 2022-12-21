using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryMusicToSpecific : MonoBehaviour
{
    void Start()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("gameMusic");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
    }
    public void keepPlaying()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
