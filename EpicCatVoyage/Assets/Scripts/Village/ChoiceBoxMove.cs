using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceBoxMove : MonoBehaviour
{
    Animator anim;
    DialogTrigger DT;

    public GameObject[] npcArray = new GameObject[5];
    public static int npc = 0;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void closeChoiceBox()
    {
        anim.SetBool("choice", false);
    }

    public void clickTalk()
    {
        DT = npcArray[npc].GetComponent<DialogTrigger>();
        Debug.Log(npc);
        DT.clickTalk();
    }
}
