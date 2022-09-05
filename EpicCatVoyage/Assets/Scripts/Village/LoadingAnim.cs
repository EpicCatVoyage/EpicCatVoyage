using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingAnim : MonoBehaviour
{
    private Animator anim;
    private int W;

    // Start is called before the first frame update
    void Start()
    {
        W = Random.Range(1, 2); //���� �ִϸ��̼� 1��

        anim = GetComponent<Animator>();
    }

    void Update()
    {
        anim.SetInteger("what",W);
    }
}
