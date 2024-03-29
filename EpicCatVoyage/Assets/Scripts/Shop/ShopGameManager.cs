using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.UI;


[System.Serializable]
public class ShopItem
{
    // 생성자
    public ShopItem(string _Type, string _Name, string _Explain, string _Number, bool _isUsing)
    {
        Type = _Type; Name = _Name; Explain = _Explain; Number = _Number; isUsing = _isUsing;
    }

    public string Type, Name, Explain, Number;
    public bool isUsing;
    
}

[System.Serializable]
public class Cat
{
    public Cat(string _Type, string _Money, string _Hungry)
    {
        Type = _Type; Money = _Money;  Hungry = _Hungry;
    }

    public string Type, Money, Hungry;
}




public class ShopGameManager : MonoBehaviour
{
    public TextAsset ShopItemDatabase;
    public TextAsset CatData;
    public List<Cat> CatList;
    public List<ShopItem> AllShopItemList, ShopItemList, CurItemList, MyItemList;
    public string curType = "Snack";
    public GameObject[] Slot;
    public GameObject[] Money; 
    public Image[] TabImage, ShopItemImage;
    public Sprite TabIdleSprite, TabSelectSprite;
    public Sprite[] ShopItemSprite;
    public GameObject ExplainPanel;
    public GameObject BuyPanel;
    public RectTransform CanvasRect;
    IEnumerator PointerCoroutine;
    RectTransform ExplainRect;
    string shopfilePath;
    string catfilePath;
    public GameObject buyBtn;
    public GameObject cancleBtn;



    void Start()
    {
        // 상점 아이템 리스트 불러오기
        string[] line = ShopItemDatabase.text.Substring(0, ShopItemDatabase.text.Length - 1).Split('\n');
        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');

            AllShopItemList.Add(new ShopItem(row[0], row[1], row[2], row[3], row[4] == "TRUE"));
        }
        Load();
        

        // 고양이 정보(돈) 불러오기
        string[] cat = CatData.text.Substring(0, CatData.text.Length - 1).Split('\n');
        for(int i = 0; i < cat.Length; i++)
        {
            string[] cat_row = cat[i].Split('\t');

            CatList.Add(new Cat(cat_row[0], cat_row[1], cat_row[2]));
        }
        Load();
        //Save();
        
        ExplainRect = ExplainPanel.GetComponent<RectTransform>();

        // 돈 출력하기
        Money[0].GetComponentInChildren<Text>().text = CatList[0].Money;
    }

    private void Update()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(CanvasRect, Input.mousePosition, Camera.main, out Vector2 anchoredPos);
        ExplainRect.anchoredPosition = anchoredPos + new Vector2(-180, -165);
        Money[0].GetComponentInChildren<Text>().text = CatList[0].Money;
    }
    public void ResetItemClick()
    {
        ShopItem BasicItem = AllShopItemList.Find(x => x.Name == "");
        ShopItemList = new List<ShopItem>()
        {
            BasicItem
        };
        Save();
        Load();
    }



    public void SlotClick(int slotNum)
    {
        BuyPanel.SetActive(true);
    }

    // 구매하기 눌렀을 때
    public void BuyClick(int slotNum)
    {
        ShopItem curItem = CurItemList[slotNum];
        if(curItem != null)
        {
            curItem.Number = (int.Parse(curItem.Number) + 1).ToString();
        }
        else
        {
            ShopItem curAllItem = AllShopItemList.Find(x => x.Name == curItem.Name);
            if(curAllItem != null)
            {
                //curAllItem.Number = "1";
                MyItemList.Add(curItem);
                CatList[0].Money = (int.Parse(CatList[0].Money) - int.Parse(curItem.Number)).ToString();

            }
            
        }
        
        Save();
        BuyPanel.SetActive(false);
    }

    public void CancleClick(int slotNum)
    {
        BuyPanel.SetActive(false);
    }


    public void TabClick(string tabName)
    {
        // 현재 아이템 리스트에 클릭한 타입만 추가
        curType = tabName;
        CurItemList = AllShopItemList.FindAll(x => x.Type == tabName);

        // 슬롯과 텍스트 보이기
        for(int i=0; i < Slot.Length; i++)
        {
            bool isExist = i < CurItemList.Count;
            Slot[i].SetActive(isExist);
            Slot[i].GetComponentInChildren<Text>().text = isExist ? CurItemList[i].Name : "";
            Slot[i].transform.GetChild(3).GetComponentInChildren<Text>().text = isExist ? CurItemList[i].Number : "";

            // 아이템 이미지
            if (isExist)
            {

                //ShopItemImage[i].sprite = ShopItemSprite[ShopItemList.FindIndex(x => x.Name == CurItemList[i].Name)];
                /*try
                {
                    ShopItemImage[i].sprite = ShopItemSprite[ShopItemList.FindIndex(x => x.Name == CurItemList[i].Name)];
                }
                catch (IndexOutOfRangeException e)
                {
                    print("예외메시지 : {0}", e.Message);
                    print("예외가 발생한 곳(namespace): {0}", e.Source);
                    print("예외가 발생한 곳(method): {0}", e.TargetSite);
                    print("예외가 발생한 곳(line): {0}", e.StackTrace);
                }*/


            }
        }

        // 탭 이미지
        int tabNum = 0;
        switch (tabName)
        {
            case "Snack" : tabNum = 0; break;
            case "Home": tabNum = 1; break;
        }

        for(int i=0; i < TabImage.Length; i++)
        {
            TabImage[i].sprite = i == tabNum ? TabSelectSprite : TabIdleSprite;
        }

    }

    // 마우스 올려놓으면 설명창
    public void PointerEnter(int slotNum)
    {
        PointerCoroutine = PointerEnterDelay(slotNum);
        StartCoroutine(PointerCoroutine);

        // 이름
        ExplainPanel.GetComponentInChildren<Text>().text = CurItemList[slotNum].Name;
        // 이미지
        ExplainPanel.transform.GetChild(2).GetComponent<Image>().sprite = Slot[slotNum].transform.GetChild(1).GetComponent<Image>().sprite;
        // 개수
        ExplainPanel.transform.GetChild(3).GetComponent<Text>().text = CurItemList[slotNum].Number;
        // 설명
        ExplainPanel.transform.GetChild(4).GetComponent<Text>().text = CurItemList[slotNum].Explain;
    }

    IEnumerator PointerEnterDelay(int slotNum)
    {
        yield return new WaitForSeconds(0.5f);
        ExplainPanel.gameObject.SetActive(true);
    }

    public void PointerExit(int slotNum)
    {
        StopCoroutine(PointerCoroutine);
        ExplainPanel.SetActive(false);
    }

    void Save()
    {
        // 상점 전체
        string jdata = JsonConvert.SerializeObject(ShopItemList);
        File.WriteAllText(Application.dataPath + "/JSON_files/ShopItemText.txt", jdata);

        // 내아이템 리스트
        string jdata_my = JsonConvert.SerializeObject(MyItemList);
        File.WriteAllText(Application.dataPath + "/JSON_files/MyItemText.txt", jdata_my);
        TabClick(curType);

        // 고양이 리스트
        string jdata_cat = JsonConvert.SerializeObject(CatList);
        File.WriteAllText(Application.dataPath + "/JSON_files/CatData.txt", jdata_cat);
    }

    void Load()
    {

        string jdata = File.ReadAllText(Application.dataPath + "/JSON_files/ShopItemText.txt");
        ShopItemList = JsonConvert.DeserializeObject<List<ShopItem>>(jdata);

        string jdata_my = File.ReadAllText(Application.dataPath + "/JSON_files/MyItemText.txt");
        MyItemList = JsonConvert.DeserializeObject<List<ShopItem>>(jdata_my);

        // 여기서 오류남 왜지?????
        string jdata_cat = File.ReadAllText(Application.dataPath + "/JSON_files/CatData.txt");
        CatList = JsonConvert.DeserializeObject<List<Cat>>(jdata_cat);

        TabClick(curType);
    }
}
