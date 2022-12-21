using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    private static int stage = 1;

    public void setStage1() { stage = 1; }
    public void setStage2() { stage = 2; }
    public void setStage3() { stage = 3; } 
    public void setStage4() { stage = 4; }

    public static int getStage()
    {
        return stage;
    }
}
