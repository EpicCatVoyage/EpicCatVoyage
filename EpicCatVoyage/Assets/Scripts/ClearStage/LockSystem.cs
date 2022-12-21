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
        int check = 0;
        foreach (GameObject l in locks){
            if (StageManager.clearStage[check] == true)
            {
                l.SetActive(false);
            }
            else
            {
                l.SetActive(true);
            }
            check++;
        }
    }
}
