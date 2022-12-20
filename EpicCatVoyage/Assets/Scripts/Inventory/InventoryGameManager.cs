using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Newtonsoft.Json;

// 직렬화
[System.Serializable]
public class Item
{
    // 생성자
    public Item(string _Type, string _Name, string _Explain, string _Price, string _Number, string _Exp, bool _isUsing)
    {
        Type = _Type; Name = _Name; Explain = _Explain; Price = _Price; Number = _Number; Exp = _Exp; isUsing = _isUsing;
    }

    // 타입, 이름, 설명, 개수, 사용여부
    public string Type, Name, Explain, Price, Number, Exp;
    public bool isUsing;
}

[System.Serializable]
public class CoinMoney
{
    public CoinMoney(string _Money)
    {
        Money = _Money;
    }
    public string Money;
}

[System.Serializable]
public class Hungryhp
{
    public Hungryhp(string _HP)
    {
        HP = _HP;
    }
    public string HP;
}

public class InventoryGameManager : MonoBehaviour
{
    //public TextAsset ItemDatabase;
    public List<Item> AllItemList, MyItemList, curItemList;
    public List<CoinMoney> CoinList;
    public List<Hungryhp> HPList;
    // 현재 뭐가 눌려있는지 처음에는 간식
    public string curType = "Snack";
    public GameObject[] Slot, UsingImage;

    public Image[] TabImage, ItemImage;
    public Sprite TabIdleSprite, TabSelectSprite;
    public Sprite[] ItemSprite;
    public GameObject ExplainPanel;
    public RectTransform[] SlotPos;
    public RectTransform CanvasRect;
    IEnumerator PointerCoroutine;
    RectTransform ExplainRect;
    public GameObject[] Coin;
    public GameObject[] Hungry;
    public GameObject HowPanel;
    public GameObject UseBtn;
    public GameObject BackBtn;
    public GameObject SellBtn;

    //디버그
    public InputField ItemNameInput, ItemNumberInput;


    void Start()
    {

        // 전체 아이템 리스트 불러오기
        // 마지막 엔터 지우기
        /*string[] line = ItemDatabase.text.Substring(0, ItemDatabase.text.Length - 1).Split('\n');
        print(line.Length);
      
        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');

            AllItemList.Add(new Item(row[0], row[1], row[2], row[3], row[4], row[5], row[6] == "TRUE"));
        }*/
        Load();

        // 돈 출력하기
        Coin[0].GetComponentInChildren<Text>().text = CoinList[0].Money;
        print(CoinList[0].Money);

        // 배고픔 출력하기
        Hungry[0].GetComponentInChildren<Text>().text = HPList[0].HP;
        print(HPList[0].HP);
        //캐싱
        ExplainRect = ExplainPanel.GetComponent<RectTransform>();

    }

    private void Update()
    {
       
        RectTransformUtility.ScreenPointToLocalPointInRectangle(CanvasRect, Input.mousePosition, Camera.main, out Vector2 anchoredPos);
        ExplainRect.anchoredPosition = anchoredPos + new Vector2(-180, -165);
    }

    // 디버그
    public void GetItemClick()
    {
        Item curItem = MyItemList.Find(x => x.Name == ItemNameInput.text);
        ItemNumberInput.text = ItemNumberInput.text == "" ? "1" : ItemNumberInput.text;
        

        if(curItem != null)
        {
            curItem.Number = (int.Parse(curItem.Number) + int.Parse(ItemNumberInput.text)).ToString();
        }
        else
        {
            // 전체에서 얻을 아이템을 찾아 내 아이템에 추가
            Item curAllItem = AllItemList.Find(x => x.Name == ItemNameInput.text);
            // 자동 1개 추가
           

            if(curAllItem != null)
            {
                curAllItem.Number = ItemNumberInput.text;
                MyItemList.Add(curAllItem);
            }
        }
        
        Save();
    }

    // 디버그
    public void RemoveItemClick()
    {
        Item curItem = MyItemList.Find(x => x.Name == ItemNameInput.text);
        if(curItem != null)
        {
            int curNumber = int.Parse(curItem.Number) - int.Parse(ItemNumberInput.text == "" ? "1" : ItemNumberInput.text);

            if(curNumber <= 0)
            {
                MyItemList.Remove(curItem);

            }
            else
            {
                curItem.Number = curNumber.ToString();
            }
            
            Save();
        }
    }

    //디버그
    public void ResetItemClick()
    {
        Item BasicItem = AllItemList.Find(x => x.Name == "식탁");
        MyItemList = new List<Item>()
        {
            BasicItem
        };
        Save();
    }

    public void PanelClick()
    {
        HowPanel.SetActive(false);
    }

    public void BackBtnClick()
    {
        HowPanel.SetActive(false);
    }

    public void SlotClick(int slotNum)
    {
        Item curItem = curItemList[slotNum];
        //HowPanel.SetActive(true);
        if (curType == "Snack")
        {
            HowPanel.SetActive(true);
            UseBtn.SetActive(true);
            SellBtn.SetActive(true);
            BackBtn.SetActive(false);
        }
        if(curType == "Gift")
        {
            HowPanel.SetActive(true);
            SellBtn.SetActive(false);
            BackBtn.SetActive(true);
        }
        
        if(curType == "Home")
        {

            HowPanel.SetActive(true);
            UseBtn.SetActive(false);
            BackBtn.SetActive(false);

        }
        HowPanel.transform.GetChild(1).GetComponent<Text>().text = curItemList[slotNum].Name;

        

        /*Item CurItem = curItemList[slotNum];
        Item UsingItem = curItemList.Find(x => x.isUsing == true);

        if(curType == "Snack")
        {
            if (UsingItem != null)
            {
                UsingItem.isUsing = false;
            }
            CurItem.isUsing = true;



        }
        else{
            CurItem.isUsing = !CurItem.isUsing;
            if(UsingItem!= null)
            {
                UsingItem.isUsing = false;
            }
        }

        Save();*/
    }

    // 사용하기 버튼 눌렀을 때
    public void UsingItemClick()
    {
        Item curItem = MyItemList.Find(x => x.Name == HowPanel.transform.GetChild(1).GetComponent<Text>().text);
        if (curItem != null)
        {
            int curNumber = int.Parse(curItem.Number) - 1;


            if (curNumber <= 0)
            {
                MyItemList.Remove(curItem);

            }
            else
            {
                curItem.Number = curNumber.ToString();
            }

            int curHp = (int.Parse(HPList[0].HP) + int.Parse(curItem.Exp));
            if (curHp < 100)
            {
                // hp 증가
                HPList[0].HP = curHp.ToString();
                // hp 갱신
                Hungry[0].GetComponentInChildren<Text>().text = HPList[0].HP;
            }
            else
            {
                // hp 증가
                HPList[0].HP = "100";
                // hp 갱신
                Hungry[0].GetComponentInChildren<Text>().text = HPList[0].HP;
            }
            /*// hp 증가
            HPList[0].HP = (int.Parse(HPList[0].HP) + int.Parse(curItem.Exp)).ToString();

            // hp 갱신
            Hungry[0].GetComponentInChildren<Text>().text = HPList[0].HP;*/

            Save();
            HowPanel.SetActive(false);
        }
    }

    // 팔기 버튼 눌렀을 때
    public void SellingItemClick()
    {
        Item curItem = MyItemList.Find(x => x.Name == HowPanel.transform.GetChild(1).GetComponent<Text>().text);
        if (curItem != null)
        {
            int curNumber = int.Parse(curItem.Number) - 1;
            print("현재 개수 : " + curNumber);

            if (curNumber < 1)
            {
                print("1개였음");
                MyItemList.Remove(curItem);

            }
            else
            {
                print("1개 이상이였음");
                curItem.Number = curNumber.ToString();
            }

            // 돈 증가
            CoinList[0].Money = (int.Parse(CoinList[0].Money) + int.Parse(curItem.Price)*0.8 ).ToString();

            // 돈 갱신
            Coin[0].GetComponentInChildren<Text>().text = CoinList[0].Money;

            Save();
            HowPanel.SetActive(false);
        }
    }


    // 아이템 탭 내용 바꾸기
    public void TabClick(string tabName)
    {
        // 현재 아이템 리스트에 클릭한 타입만 추가
        curType = tabName;
        curItemList = MyItemList.FindAll(x => x.Type == tabName);

        // 슬롯과 텍스트 보이기
        for(int i=0; i < Slot.Length; i++)
        {
            bool isExist = i < curItemList.Count;
            Slot[i].SetActive(isExist);
            Slot[i].GetComponentInChildren<Text>().text = isExist ? curItemList[i].Name : "";
            //Slot[i].GetComponentInChildren<Text>().text = isExist ? curItemList[i].Name + "/" + curItemList[i].isUsing : "";

            if (isExist)
            {
                // 아이템 이미지
                ItemImage[i].sprite = ItemSprite[AllItemList.FindIndex(x => x.Name == curItemList[i].Name)];
                /*UsingImage[i].SetActive(curItemList[i].isUsing);*/
                // 집꾸미기만 사용중인지 뜨게
                /*if (curType == "Home")
                {
                    UsingImage[i].SetActive(curItemList[i].isUsing);
                }*/
            }
        }

        // 탭 색깔 바꾸기
        int tabNum = 0;
        switch (tabName)
        {
            case "Snack": tabNum = 0; break;
            case "Home": tabNum = 1; break;
            case "Gift": tabNum= 2; break;
        }
        for (int i =0; i< TabImage.Length; i++)
        {
            TabImage[i].sprite = i == tabNum ? TabSelectSprite : TabIdleSprite;
        }

        

    }

    // 설명 보이기
    public void PointerEnter(int slotNum)
    {
        // 슬롯에 마우스 올리면 0.5초 후에 설명창
        PointerCoroutine = PointerEnterDelay(slotNum);
        StartCoroutine(PointerCoroutine);

        // 이름
        ExplainPanel.GetComponentInChildren<Text>().text = curItemList[slotNum].Name;
        // 이미지
        ExplainPanel.transform.GetChild(2).GetComponent<Image>().sprite = Slot[slotNum].transform.GetChild(1).GetComponent<Image>().sprite;
        // 개수
        ExplainPanel.transform.GetChild(3).GetComponent<Text>().text = curItemList[slotNum].Number + "개";
        // 설명
        ExplainPanel.transform.GetChild(4).GetComponent<Text>().text = curItemList[slotNum].Explain;
    }
        

    IEnumerator PointerEnterDelay(int slotNum)
    {
        yield return new WaitForSeconds(0.5f);
        ExplainPanel.SetActive(true);
    }

    public void PointerExit(int slotNum)
    {
        StopCoroutine(PointerCoroutine);
        ExplainPanel.SetActive(false);
    }

    void Save()
    {
        string jdata = JsonConvert.SerializeObject(MyItemList);
        File.WriteAllText(Application.streamingAssetsPath + "/JSON_files/MyItemText.txt", jdata);

        // 인벤토리 정보 저장
        string jdata_my = JsonConvert.SerializeObject(MyItemList);
        File.WriteAllText(Application.streamingAssetsPath + "/JSON_files/MyItemText.txt", jdata_my);

        // 돈 정보 저장
        string jdata_coin = JsonConvert.SerializeObject(CoinList);
        File.WriteAllText(Application.streamingAssetsPath + "/JSON_files/CoinText.txt", jdata_coin);

        // 체력 정보 저장
        string jdata_hp = JsonConvert.SerializeObject(HPList);
        File.WriteAllText(Application.streamingAssetsPath + "/JSON_files/HPText.txt", jdata_hp);

        TabClick(curType);


    }

    void Load()
    {
        string jdata = File.ReadAllText(Application.streamingAssetsPath + "/JSON_files/StoreItemText.txt");
        AllItemList = JsonConvert.DeserializeObject<List<Item>>(jdata);

        string jdata_my = File.ReadAllText(Application.streamingAssetsPath + "/JSON_files/MyItemText.txt");
        MyItemList = JsonConvert.DeserializeObject<List<Item>>(jdata_my);

        string jdata_coin = File.ReadAllText(Application.streamingAssetsPath + "/JSON_files/CoinText.txt");
        CoinList = JsonConvert.DeserializeObject<List<CoinMoney>>(jdata_coin);

        string jdata_hp = File.ReadAllText(Application.streamingAssetsPath + "/JSON_files/HPText.txt");
        HPList = JsonConvert.DeserializeObject<List<Hungryhp>>(jdata_hp);

        TabClick(curType);
    }


}

