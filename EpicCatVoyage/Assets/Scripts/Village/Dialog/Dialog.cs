using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //�ν����� �信 ����
public class Dialog
{
    [TextArea(3, 10)]

    public string name;
    public string[] sentences;
}
