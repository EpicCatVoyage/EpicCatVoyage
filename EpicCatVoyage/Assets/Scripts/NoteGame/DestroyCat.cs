using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCat : MonoBehaviour
{
    private GameObject singingCat;
    void Start()
    {
        singingCat = GameObject.FindWithTag("singingCatGameMusic");
        Destroy(singingCat);
    }

}
