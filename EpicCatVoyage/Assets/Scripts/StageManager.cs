using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    // �������� Ŭ���� Ȯ�ο뵵
    public static bool[] clearStage = { false, false, false, false };
    private static int stage = 1;

    public void setStage1() {
        stage = 1;
        StoreInfo.setFriendship(10);    // ���� stage ���� ȣ���� ����

        // �ʱ� �����, coin ����
        StoreInfo.setHungry(100);
        StoreInfo.setCoin(4000);

        // �ʱ� �ٳ���
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
