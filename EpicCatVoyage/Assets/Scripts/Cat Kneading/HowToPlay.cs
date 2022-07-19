using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlay : MonoBehaviour
{
    public GameObject click;
    // Start is called before the first frame update
    void Start()
    {
        click.SetActive(false);
        InvokeRepeating("howToPlay", 0f, 3f);
    }

    void howToPlay()
    {
        click.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
