using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public GameObject choiceBox;
    public Text nameText;
    public Text mentText;
    public int npcNum = 0;
    //npcNum은 현재 대화 중인 npc 정보입니다.
    //0은 초딩, 1은 할머니, 2는 직장인, 3은 생선가게 아저씨, 4은 멍멍이

    Animator anim;
    DialogTrigger DT;
    private Queue<string> mentList = new Queue<string>();
    private Queue<string> nameList = new Queue<string>();
    private int endNum;

    void Awake() {
        anim = choiceBox.GetComponent<Animator>();
        DT = gameObject.GetComponent<DialogTrigger>();
    }

    public void dialogSet(Dialog dia, int num)
    {
        Debug.Log("Set");
        endNum = num; //대화 종료 시 처리 결정.

        foreach (string name in dia.name) //문장 별 화자 저장.
            nameList.Enqueue(name);

        foreach(string str in dia.sentences) //대화 문장 저장.
            mentList.Enqueue(str);

        clickNext();
    }

    public void openChoiceBox()
    {
        anim.SetBool("choice", true);
        ChoiceBoxMove.npc = this.npcNum;
    }

    public void clickNext()
    {
        if (mentList.Count == 0) //현재 대화가 끝났는지 확인.
        {
            choiceEnd(); //해당 함수에서 어떻게 맺을 지 결정.
            return;
        }

        nameText.text = nameList.Dequeue(); //문장별 화자 이름 출력.
        string str = mentList.Dequeue(); //문장 출력
        StartCoroutine(printMent(str)); //문장 화면에 나타내기.
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

    void choiceEnd() //어떻게 맺을지 결정.
    {
        if (endNum == 1) //�ֱ��θ���, �����ϱ�, �̴ϰ��� �ϱ� ���� ���̽� �ڽ� ����.
        {
            DT.closeMentBox();
        }
        else if (endNum == 2)
        {
           DT.closeMentBox();
        }
        else if (endNum == 3)
        {
            if (StoreInfo.charm == true)
            {
                StoreInfo.setFriendship(StoreInfo.getFriendship() + 5);
                StoreInfo.charm = false;
            }
            DT.closeMentBox();
        }
        else
        {
            Debug.Log("그런 선택지는 없다");
        }
    }
}
