using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Animator anim;

    public void footClick()
    {
        anim = GetComponent<Animator>();
        //������ �ִϸ��̼� �߰��ϱ�
        Destroy(this);
    }
}
