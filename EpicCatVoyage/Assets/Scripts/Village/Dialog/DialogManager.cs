using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public GameObject choiceBox;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI mentText;

    private Queue<string> mentList = new Queue<string>();
    private int endNum;
    private int npc;

    public void dialogSet(Dialog dia, int num, int npc)
    {
        this.npc = npc;
        nameText.text = dia.name;
        foreach(string str in dia.sentences)
            mentList.Enqueue(str);
        endNum = num;

        clickNext();
    }

    public void clickNext()
    {
        if (mentList.Count == 0)
        {
            choiceEnd();
            return;
        }
        
        string str = mentList.Dequeue();
        StartCoroutine(printMent(str));
    }

    IEnumerator printMent(string ment)
    {
        mentText.text = "";

        foreach (char c in ment.ToCharArray())
        {
            mentText.text += c;
            yield return null;
        }
    }

    void choiceEnd()
    {
        if (endNum == 1) //�ֱ��θ���, �����ϱ�, �̴ϰ��� �ϱ� ���� ���̽� �ڽ� ����.
        {
            openChoiceBox();
        }
        else
        {
            Debug.Log("���� �������� ���� ��ȭ �����Դϴ�");
        }
    }

    void openChoiceBox()
    {
        Animator anim = choiceBox.GetComponent<Animator>();
        anim.SetBool("choice", true);
    }
}
