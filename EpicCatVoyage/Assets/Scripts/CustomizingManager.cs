using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Newtonsoft.Json;


[System.Serializable]
public class HomeItem
{
    public HomeItem(string _Type, string _Name, string _Explain, string _Price, string _Number, string _Exp, bool _isUsing)
    {
        Type = _Type; Name = _Name; Explain = _Explain; Price = _Price; Number = _Number; Exp = _Exp; isUsing = _isUsing;
    }
    public string Type, Name, Explain, Price, Number, Exp;
    public bool isUsing;
}

public class CustomizingManager : MonoBehaviour
{

    /*public TextAsset ItemDatabase;*/
    public List<HomeItem> StoreItemList, MyItemList, CurItemList;
    public GameObject[] Slot, UsingImage, UsingItemImage;
    public Image[] ItemImage;
    public Sprite[] ItemSprite;
    public GameObject box;

    void Start()
    {
        // 전체 아이템 리스트 불러오기
        /*string[] line = ItemDatabase.text.Substring(0, ItemDatabase.text.Length - 1).Split('\n');
        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');

            AllItemList.Add(new HomeItem(row[0], row[1], row[2], row[3], row[4], row[5], row[6] == "TRUE"));
        }*/

        

        //CurItemList = AllItemList.FindAll(x => x.Type == "Home");
        //CurItemList = MyItemList.FindAll(x => x.Type == "Home");

        /*// 슬롯과 텍스트 보이기
        for (int i = 0; i < Slot.Length; i++)
        {
            bool isExist = i < CurItemList.Count;
            Slot[i].SetActive(isExist);
            Slot[i].GetComponentInChildren<Text>().text = isExist ? CurItemList[i].Name + "/" + CurItemList[i].isUsing : "";
            //Slot[i].GetComponentInChildren<Text>().text = isExist ? CurItemList[i].Name : "";

            if (isExist)
            {
                ItemImage[i].sprite = ItemSprite[AllItemList.FindIndex(x => x.Name == CurItemList[i].Name)];
            }


        }*/


        Load();
    }

    public void slotUpdate()
    {
        CurItemList = MyItemList.FindAll(x => x.Type == "Home");
        // 슬롯과 텍스트 보이기
        for (int i = 0; i < Slot.Length; i++)
        {
            bool isExist = i < CurItemList.Count;
            Slot[i].SetActive(isExist);
            Slot[i].GetComponentInChildren<Text>().text = isExist ? CurItemList[i].Name : "";
            //Slot[i].GetComponentInChildren<Text>().text = isExist ? CurItemList[i].Name : "";

            if (isExist)
            {
                ItemImage[i].sprite = ItemSprite[StoreItemList.FindIndex(x => x.Name == CurItemList[i].Name)];
                UsingImage[i].SetActive(CurItemList[i].isUsing);
                //box.SetActive(false);

                //UsingItemImage[0].SetActive(true);

                if (CurItemList[i].isUsing == true)
                {
                    UsingItemImage[i].SetActive(true);
                }
                else
                {
                    UsingItemImage[i].SetActive(false);
                }
            }


        }

        // 선택 되어있는 아이템들 보이게
       /* ItemImage[i].sprite = ItemSprite[AllItemList.FindIndex(x => x.Name == CurItemList[i].Name)];*/

        // 선택 안되어있는 아이템 안보이게

    }


    public void SlotClick(int slotNum)
    {
        // 슬롯 클릭하면 체크 표시 나오게 + true false 값 저장 왜 저장 안되지ㅣㅣ
        //CurItemList = MyItemList.FindAll(x => x.Type == "Home");
        HomeItem CurItem = CurItemList[slotNum];
        print(CurItem.isUsing);
        HomeItem UsingItem = CurItemList.Find(x => x.isUsing == true);
        if(CurItem.isUsing == true)
        {
            CurItem.isUsing = false;
            print(CurItem.isUsing + "t에서 f");

        }
        else
        {
            CurItem.isUsing = true;
            print(CurItem.isUsing + "f에서 t");
        }
        Save();
        

        // 슬롯 이름이 다른 canvas의 아이템과 같은거면 클릭한 값의 아이템 보이게

    }


    void Save()
    {
        /*// 아이템 전체리스트 저장
        string jdata = JsonConvert.SerializeObject(SItemList);
        File.WriteAllText(Application.streamingAssetsPath + "/JSON_files/StoreItemText.txt", jdata);*/

        // 인벤토리 정보 저장
        string jdata_my = JsonConvert.SerializeObject(MyItemList);
        File.WriteAllText(Application.streamingAssetsPath + "/JSON_files/MyItemText.txt", jdata_my);

        /*// 돈 정보 저장
        string jdata_coin = JsonConvert.SerializeObject(CoinList);
        File.WriteAllText(Application.dataPath + "/JSON_files/CoinText.txt", jdata_coin);

        // 체력 정보 저장
        string jdata_hp = JsonConvert.SerializeObject(HPList);
        File.WriteAllText(Application.dataPath + "/JSON_files/HPText.txt", jdata_hp);*/

        slotUpdate();

    }

    void Load()
    {
        string jdata = File.ReadAllText(Application.streamingAssetsPath + "/JSON_files/StoreItemText.txt");
        StoreItemList = JsonConvert.DeserializeObject<List<HomeItem>>(jdata);

        string jdata_my = File.ReadAllText(Application.streamingAssetsPath + "/JSON_files/MyItemText.txt");
        MyItemList = JsonConvert.DeserializeObject<List<HomeItem>>(jdata_my);

        slotUpdate();
        /*string jdata_coin = File.ReadAllText(Application.dataPath + "/JSON_files/CoinText.txt");
        CoinList = JsonConvert.DeserializeObject<List<Coin>>(jdata_coin);

        string jdata_hp = File.ReadAllText(Application.dataPath + "/JSON_files/HPText.txt");
        HPList = JsonConvert.DeserializeObject<List<Hungry>>(jdata_hp);*/

    }
}
