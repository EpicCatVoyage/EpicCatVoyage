using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryMusic : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
