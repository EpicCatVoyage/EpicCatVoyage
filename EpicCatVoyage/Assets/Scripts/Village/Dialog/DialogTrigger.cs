using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    DialogManager DM;

    public GameObject mentBox;

    public Dialog dia;
    int diaNum = 1;

    // Start is called before the first frame update
    void Awake()
    {
        DM = gameObject.GetComponent<DialogManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void clickNPC()
    {
        Debug.Log(DM); //�� ���߿� ����
        mentBox.SetActive(true);
        DM.dialogSet(dia, diaNum);
        Debug.Log("�Լ� ��");
    }
    /*
    void Send()
    {
        DM.dialogSet(dia, diaNum);
    }
    */
}
