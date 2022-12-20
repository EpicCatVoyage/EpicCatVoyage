using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatProfile : MonoBehaviour
{
    public GameObject[] cats;

    // stage 확인용 - 클래스 이름 고쳐야함
    public static int stage = 3;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject obj in cats)
        {
            obj.SetActive(false);
        }

        cats[stage - 1].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
