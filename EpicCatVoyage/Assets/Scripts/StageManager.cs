using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    // 스테이지 클리어 확인용도
    public static bool[] clearStage = { false, false, false, false };
    private static int stage = 1;

    public void setStage1() {
        stage = 1;
        StoreInfo.setFriendship(10);    // 각각 stage 마다 호감도 설정

        // 초기 배고픔, coin 설정
        StoreInfo.setHungry(100);
        StoreInfo.setCoin(4000);

        // 초기 바나나
        StoreInfo.setInventory();
    }
    public void setStage2() { 
        stage = 2;
        StoreInfo.setFriendship(5);
    }
    public void setStage3() { 
        stage = 3;
        StoreInfo.setFriendship(0);
    } 
    public void setStage4() { 
        stage = 4;
        StoreInfo.setFriendship(-5);
    }

    public static int getStage()
    {
        return stage;
    }
}
