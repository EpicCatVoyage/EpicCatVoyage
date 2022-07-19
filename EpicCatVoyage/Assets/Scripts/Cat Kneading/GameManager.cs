using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Animator anim;

    public void footClick()
    {
        anim = GetComponent<Animator>();
        //터지는 애니메이션 추가하기
        Destroy(this);
    }
}
