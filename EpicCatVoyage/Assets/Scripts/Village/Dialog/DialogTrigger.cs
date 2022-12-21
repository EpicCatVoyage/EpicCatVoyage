using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    DialogManager DM;

    public GameObject mentBox;
    public Dialog2[] dia2 = new Dialog2[2];
    public Dialog[] charm = new Dialog[2];
    Animator anim;

    int diaNum = 1; //1.��ȭ�ϱ�, 2.�����ֱ� 3.�ֱ��θ���
    int r = 0; //��ȭ �� ȣ������ ���� dia ����. (Dialog2)


    void Awake()
    {
        DM = gameObject.GetComponent<DialogManager>();
        anim = mentBox.GetComponent<Animator>();
    }

    private void openMentBox() { anim.SetBool("Trigger", true); }
    public void closeMentBox() { anim.SetBool("Trigger", false); }

    public void clickTalk()
    {
        diaNum = 1;
        openMentBox();
        if (StoreInfo.getFriendship() < 25)
        {
            r = Random.Range(0, dia2.GetLength(0)-1);
            DM.dialogSet(dia2[0].dia[r], diaNum);
        }
        else if (StoreInfo.getFriendship() < 50)
        {
            r = Random.Range(0, dia2.GetLength(1)-1);
            DM.dialogSet(dia2[1].dia[r], diaNum);
        }
        else if (StoreInfo.getFriendship() < 75)
        {
            r = Random.Range(0, dia2.GetLength(2)-1);
            DM.dialogSet(dia2[2].dia[r], diaNum);
        }
        else if (StoreInfo.getFriendship() <= 100)
        {
            r = Random.Range(0, dia2.GetLength(3)-1);
            DM.dialogSet(dia2[3].dia[r], diaNum);
        }
    }

    public void clickPresent() //���� Ŭ����. diaNum�� 2�� �ش�.
    {
        diaNum = 2;
    }

    public void clickCharm() //�ֱ��θ���. diaNum�� 3���� �ش�.
    {
        diaNum = 3;
        
    }

}
/* ��� ����
 * 1. ���ϴ� ��ü���� DialogTrigger�� DialogManager ��ũ��Ʈ�� �߰�
 * 2. �ν����� â���� Manager ����
 *      2-1. ChoiceBox
 *      2-2. Name���� ȭ���� �̸��� ǥ���� ������Ʈ(Text) �߰�
 *      2-3. Ment���� ��ȭ�� ǥ���� ������Ʈ(Text) �߰�
 * 3. �ν����� â���� Trigger ����
 *      3-1. Name���� ȭ���� �̸� ����
 *      3-2. Sentences���� ��ȭ ����. �� �ڽ��� ǥ��, ���� ��ư�� ������ ���� �ڽ� ǥ��
 *      3-3. MentBox���� ��ȭ�� ǥ���� â ������Ʈ �߰�.
   4.��ȭ���� ����
        a. Trigger ����
        4-1. ��ȭ�� �����ϴ� ��ü�� ���� �Լ��� ����/ ����
            ex) NPC�� ��ȭ�ڽ� ������ clickNPC
        �Լ� ������ �Ʒ� ������ ���� �ּž� �մϴ�. (������ ��� ���������)

        4-2. �Լ� ����
            clickNPC()�� ���� ���� �� DM.dialogSet(dia, diaNum)���� diaNum�� �� ��ȣ�� ����, 
        
        b. Manager ����
        4-3. choiceEnd() �Լ� ���� endNum == �� ��ȣ case�ۼ�
            �Լ��� ������ ��ȭ ������ �� �߻��Ǳ� ���ϴ� �̺�Ʈ�Դϴ�.
 */