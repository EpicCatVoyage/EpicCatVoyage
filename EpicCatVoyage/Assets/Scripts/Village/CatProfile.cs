using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatProfile : MonoBehaviour
{
    public GameObject[] cats;


    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject obj in cats)
        {
            obj.SetActive(false);
        }

        cats[StageManager.getStage() - 1].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
