using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    DialogManager DM;

    public GameObject mentBox;
    public Dialog2[] dia2 = new Dialog2[2];
    Animator anim;

    int diaNum = 1; //대화하기
    int r = 0; //대화 중 호감도에 따른 dia 묶음. (Dialog2)


    void Awake()
    {
        DM = gameObject.GetComponent<DialogManager>();
        anim = GetComponent<Animator>();
    }


    private void openMentBox() { anim.SetBool("Trigger", true); }
    private void closeMentBox() { anim.SetBool("Trigger", false); }

    public void clickTalk()
    {
        Debug.Log(DM); //너 나중에 보자
        openMentBox();
        if (StoreInfo.getFriendship() < 25)
        {
            r = Random.Range(0, dia2.GetLength(0));
            DM.dialogSet(dia2[0].dia[r], diaNum);
        }
        else if (StoreInfo.getFriendship() < 50)
        {
            r = Random.Range(0, dia2.GetLength(1));
            DM.dialogSet(dia2[1].dia[r], diaNum);
        }
        else if (StoreInfo.getFriendship() < 75)
        {
            r = Random.Range(0, dia2.GetLength(2));
            DM.dialogSet(dia2[2].dia[r], diaNum);
        }
        else if (StoreInfo.getFriendship() <= 100)
        {
            r = Random.Range(0, dia2.GetLength(3));
            DM.dialogSet(dia2[3].dia[r], diaNum);
        }
    }

    public void clickPresent() //선물 클릭시. diaNum은 2로 준다.
    {
        ;
    }

    public void clickCharm() //애교부리기. diaNum은 3으로 준다.
    {
        ;
    }

}
/* 사용 설명서
 * 1. 말하는 주체에게 DialogTrigger와 DialogManager 스크립트를 추가
 * 2. 인스펙터 창에서 Manager 세팅
 *      2-1. ChoiceBox
 *      2-2. Name에는 화자의 이름을 표시할 오브젝트(Text) 추가
 *      2-3. Ment에는 대화를 표시할 오브젝트(Text) 추가
 * 3. 인스펙터 창에서 Trigger 세팅
 *      3-1. Name에는 화자의 이름 적기
 *      3-2. Sentences에는 대화 적기. 한 박스씩 표기, 다음 버튼을 누르면 다음 박스 표기
 *      3-3. MentBox에는 대화를 표시할 창 오브젝트 추가.
   4.대화유형 설정
        a. Trigger 설정
        4-1. 대화를 시작하는 주체에 따라 함수를 적용/ 생성
            ex) NPC의 대화박스 생성시 clickNPC
        함수 생성시 아래 절차도 따라 주셔야 합니다. (적용일 경우 여기까지만)

        4-2. 함수 생성
            clickNPC()의 내용 복사 후 DM.dialogSet(dia, diaNum)에서 diaNum에 새 번호를 기입, 
        
        b. Manager 설정
        4-3. choiceEnd() 함수 내에 endNum == 새 번호 case작성
            함수의 내용은 대화 마무리 후 발생되기 원하는 이벤트입니다.
 */