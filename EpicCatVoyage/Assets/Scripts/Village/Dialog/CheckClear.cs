using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckClear : MonoBehaviour
{
    public void check()
    {
        // 스테이지 체크 함수
        if (StoreInfo.getFriendship() >= 100)
        {
            if (StageManager.getStage() == 1)
            {
                SceneManager.LoadScene("ClearStage1");
            }
            else if (StageManager.getStage() == 2)
            {
                SceneManager.LoadScene("ClearStage2");
            }
            else if (StageManager.getStage() == 3)
            {
                SceneManager.LoadScene("ClearStage3");
            }
            else if (StageManager.getStage() == 4)
            {
                SceneManager.LoadScene("ClearStage4");
            }
        }
        else if (StoreInfo.getFriendship() > 25)
            StoreInfo.charm = true;
        else if (StoreInfo.getFriendship() > 50)
            StoreInfo.charm = true;
        else if (StoreInfo.getFriendship() > 75)
            StoreInfo.charm = true;
        else
        {
            print("현재 호감도" + StoreInfo.getFriendship());
        }
    } 
}
