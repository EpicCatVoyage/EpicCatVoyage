using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    DialogManager DM;

    public GameObject mentBox;

    public Dialog dia;
    int diaNum = 1; //npc���� ���� �� ���. ��Ʈ �� choice �ڽ� ����. (�����ϱ�, �̴ϰ����ϱ�, �ֱ� �θ���)

    void Awake()
    {
        DM = gameObject.GetComponent<DialogManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void clickNPC()
    {
        Debug.Log(DM); //�� ���߿� ����
        mentBox.SetActive(true);
        DM.dialogSet(dia, diaNum);
        Debug.Log("�Լ� ��");
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