using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //인스펙터 뷰에 노출
public class Dialog //그냥 하나의 자료형...?입니다
{
    [TextArea(3, 10)]

    public string name;
    public string[] sentences;

    //사용법은 트리거에 있습니다.
}
