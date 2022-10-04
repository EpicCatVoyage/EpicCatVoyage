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
        W = Random.Range(1, 2); //현재 애니메이션 1개

        anim = GetComponent<Animator>();
    }

    void Update()
    {
        anim.SetInteger("what",W);
    }
}
