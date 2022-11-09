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

    public void dialogSet(Dialog dia, int num)
    {
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
        if (endNum == 1) //애교부리기, 선물하기, 미니게임 하기 등의 초이스 박스 등장.
        {
            openChoiceBox();
        }
        else
        {
            Debug.Log("아직 업뎃되지 않은 대화 유형입니다");
        }
    }

    void openChoiceBox()
    {
        choiceBox.SetActive(true);
    }
}
