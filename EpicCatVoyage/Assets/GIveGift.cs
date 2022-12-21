using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Newtonsoft.Json;
/*using UnityEngine.Random;*/

// 직렬화
[System.Serializable]
public class Gift
{
    // 생성자
    public Gift(string _Type, string _Name, string _Explain, string _Price, string _Number, string _Exp, bool _isUsing)
    {
        Type = _Type; Name = _Name; Explain = _Explain; Price = _Price; Number = _Number; Exp = _Exp; isUsing = _isUsing;
    }

    // 타입, 이름, 설명, 개수, 사용여부
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
        // 내 아이템 리스트에서 Gift 중 랜덤으로 하나 뽑기

        GiftList = MyItemList.FindAll(x => x.Type == "Gift");
        print(GiftList.Count + "선물 개수");
        
        int i = Random.Range(0,GiftList.Count);
        if (i > 0)
        {
            Gift GiftItem = GiftList[i];
            print(GiftItem.Name + "아이템");

            // 아이템 삭제하기

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

                // 뭘 선물로 줬는지 보이게
                usingGift.SetActive(true);
                usingGift.transform.GetChild(1).GetComponent<Text>().text = GiftItem.Name;

            }
            
        }
        else
        {
            // 선물이 없습니다.
            usingGift.SetActive(true);
            usingGift.transform.GetChild(1).GetComponent<Text>().text = "선물";
            usingGift.transform.GetChild(0).GetComponent<Text>().text = "이 없습니다.";
        }







    }

    public void PanelClick()
    {
        usingGift.SetActive(false);
    }

    void Save()
    {
        // 내 아이템 저장
        // 인벤토리 정보 저장
        string jdata_my = JsonConvert.SerializeObject(MyItemList);
        File.WriteAllText(Application.streamingAssetsPath + "/JSON_files/MyItemText.txt", jdata_my);

    }

    void Load()
    {
        // 내 아이템 가져오기
        string jdata_my = File.ReadAllText(Application.streamingAssetsPath + "/JSON_files/MyItemText.txt");
        MyItemList = JsonConvert.DeserializeObject<List<Gift>>(jdata_my);

    }
}
