using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Newtonsoft.Json;
/*using UnityEngine.Random;*/

// ����ȭ
[System.Serializable]
public class Gift
{
    // ������
    public Gift(string _Type, string _Name, string _Explain, string _Price, string _Number, string _Exp, bool _isUsing)
    {
        Type = _Type; Name = _Name; Explain = _Explain; Price = _Price; Number = _Number; Exp = _Exp; isUsing = _isUsing;
    }

    // Ÿ��, �̸�, ����, ����, ��뿩��
    public string Type, Name, Explain, Price, Number, Exp;
    public bool isUsing;
}

public class GIveGift : MonoBehaviour
{
    public List<Gift> MyItemList, GiftList;
    public GameObject usingGift;
    // Start is called before the first frame update
    void Start()
    {
        Load();
        //Give();


    }

    public void Give()
    {
        // �� ������ ����Ʈ���� Gift �� �������� �ϳ� �̱�

        GiftList = MyItemList.FindAll(x => x.Type == "Gift");
        print(GiftList.Count + "���� ����");
        
        int i = Random.Range(0,GiftList.Count);
        if (i > 0)
        {
            Gift GiftItem = GiftList[i];
            print(GiftItem.Name + "������");

            // ������ �����ϱ�

            if (GiftItem != null)
            {
                int curNumber = int.Parse(GiftItem.Number) - 1;

                if (curNumber <= 0)
                {
                    MyItemList.Remove(GiftItem);

                }
                else
                {
                    GiftItem.Number = curNumber.ToString();
                }

                Save();

                // �� ������ ����� ���̰�
                usingGift.SetActive(true);
                usingGift.transform.GetChild(1).GetComponent<Text>().text = GiftItem.Name;

            }
            
        }
        else
        {
            // ������ �����ϴ�.
            usingGift.SetActive(true);
            usingGift.transform.GetChild(1).GetComponent<Text>().text = "����";
            usingGift.transform.GetChild(0).GetComponent<Text>().text = "�� �����ϴ�.";
        }







    }

    public void PanelClick()
    {
        usingGift.SetActive(false);
    }

    void Save()
    {
        // �� ������ ����
        // �κ��丮 ���� ����
        string jdata_my = JsonConvert.SerializeObject(MyItemList);
        File.WriteAllText(Application.streamingAssetsPath + "/JSON_files/MyItemText.txt", jdata_my);

    }

    void Load()
    {
        // �� ������ ��������
        string jdata_my = File.ReadAllText(Application.streamingAssetsPath + "/JSON_files/MyItemText.txt");
        MyItemList = JsonConvert.DeserializeObject<List<Gift>>(jdata_my);

    }
}
