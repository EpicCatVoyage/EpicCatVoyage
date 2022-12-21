using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockSystem : MonoBehaviour
{
    public GameObject[] locks = new GameObject[3];

    // Start is called before the first frame update
    void Start()
    {
        checkStage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void checkStage()
    {
        int currentStage = StageManager.getStage();

        if(StageManager.clearStage[currentStage - 1] == true)
        {
            locks[currentStage - 1].SetActive(false);
        }
    }
}
