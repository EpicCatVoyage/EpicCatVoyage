using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldMusic : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    
}
