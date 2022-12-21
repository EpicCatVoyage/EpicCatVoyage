using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    public Dialog diaF;
    public Dialog diaT;
    public GameObject shop;

    DialogManager DM;
    int diaNum = 2;

    // Start is called before the first frame update
    void Start()
    {
        DM = gameObject.GetComponent<DialogManager>();
    }

    public void ClickDog()
    {
        if (StoreInfo.dog == false)
            dogFirst();
        else 
            dogTrue();
    }

    private void dogFirst() //강아지 처음. diaNum은 2로 준다.
    {
        StoreInfo.dog = true;
        DM.dialogSet(diaF, diaNum);
    }

    private void dogTrue()
    {
        DM.dialogSet(diaT, diaNum);
    }
}
