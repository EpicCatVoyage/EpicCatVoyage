using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //�ν����� �信 ����
public class Dialog //�׳� �ϳ��� �ڷ���...?�Դϴ�
{
    [TextArea(3, 10)]

    public string[] name; // = new string[2];
    public string[] sentences; // = new string[2];

    //������ Ʈ���ſ� �ֽ��ϴ�.
}
[System.Serializable]
public class Dialog2
{
    public Dialog[] dia; // = new Dialog[2];
}
