using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.UI;

[System.Serializable]
public class StoreItem
{
    public StoreItem(string _Type, string _Name, string _Explain, string _Price, string _Number, bool _isUsing)
    {
        Type = _Type; Name = _Name; Explain = _Explain; Price = _Price; Number = _Number; isUsing = _isUsing;
    }
    public string Type, Name, Explain, Price, Number;
    public bool isUsing;
}
[System.Serializable]
public class Coin
{
    public Coin(string _Money)
    {
        Money = _Money;
    }
    public string Money;
}
[System.Serializable]
public class Hungry
{
    public Hungry(string _HP)
    {
        HP = _HP;
    }
    public string HP;
}

public class StoreGameManager : MonoBehaviour
{

    public TextAsset ItemDatabase;
    public TextAsset CoinData;
    public TextAsset HungryData;
    public List<StoreItem> AllItemList, StoreItemList, CurItemList, MyItemList;
    public List<Coin> CoinList;
    public List<Hungry> HPList;
    public string curType = "Snack";
    public GameObject[] Slot;
    public Image[] TabImage, ItemImage;
    public Sprite TabIdleSprite, TabSelectSprite;
    public Sprite[] ItemSprite;
    public GameObject ExplainPanel;
    public RectTransform CanvasRect;
    IEnumerator PointerCoroutine;
    RectTransform ExplainRect;
    public GameObject[] Coin;
    public GameObject[] Hungry;
    public GameObject BuyPanel;


    void Start()
    {
        // 상점 아이템 리스트 불러오기
        string[] line = ItemDatabase.text.Substring(0, ItemDatabase.text.Length - 1).Split('\n');
        for(int i =0; i< line.Length; i++)
        {
            string[] row = line[i].Split('\t');

            AllItemList.Add(new StoreItem(row[0], row[1], row[2], row[3], row[4], row[5] == "TRUE"));
        }
        /*CoinList.Add(new Coin("5000"));
        HPList.Add(new Hungry("50"));*/

        /*// 돈 정보 불러오기
        string[] line_coin = CoinData.text.Substring(0, CoinData.text.Length - 1).Split('\n');
        for (int i = 0; i < line_coin.Length; i++)
        {
            string[] row_coin = line_coin[i].Split('\t');
            CoinList.Add(new Coin(row_coin[0]));
        }

        // 체력 정보 불러오기
        string[] line_hungry = HungryData.text.Substring(0, HungryData.text.Length - 1).Split('\n');
        for (int i = 0; i < line_hungry.Length; i++)
        {
            string[] row_hungry = line_hungry[i].Split('\t');
            HPList.Add(new Hungry(row_hungry[0]));
            print(HPList[0].HP);
        }*/

        Load();

        // 돈 출력하기
        Coin[0].GetComponentInChildren<Text>().text = CoinList[0].Money;
        print(CoinList[0].Money);

        // 배고픔 출력하기
        Hungry[0].GetComponentInChildren<Text>().text = HPList[0].HP;
        print(HPList[0].HP);




        ExplainRect = ExplainPanel.GetComponent<RectTransform>();
    }

    private void Update()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(CanvasRect, Input.mousePosition, Camera.main, out Vector2 anchoredPos);
        ExplainRect.anchoredPosition = anchoredPos + new Vector2(-180, -165);
    }

    // 슬롯 클릭하면 구매할 수 있게
    public void SlotClick(int slotNum)
    {
        StoreItem curItem = CurItemList[slotNum];
        BuyPanel.SetActive(true);
        BuyPanel.transform.GetChild(1).GetComponent<Text>().text = CurItemList[slotNum].Name;
    }

    public void updateStateInfo()
    {

    }

    // 구매하기 버튼
    // 구매하기 눌렀을 때
    public void BuyClick(int slotNum)
    {
        print(BuyPanel.transform.GetChild(1).GetComponent<Text>().text);
        StoreItem curItem = MyItemList.Find(x => x.Name == BuyPanel.transform.GetChild(1).GetComponent<Text>().text);
        

        // 아이템을 가지고 있을때는 개수만 1개 추가
        if (curItem != null)
        {
            // 아이템 추가
            curItem.Number = (int.Parse(curItem.Number) + 1).ToString();
            // 돈 감소
            CoinList[0].Money = (int.Parse(CoinList[0].Money) - int.Parse(curItem.Price)).ToString();

            // 돈 갱신
            Coin[0].GetComponentInChildren<Text>().text = CoinList[0].Money;
        }

        // 없을 때는 아이템 자체를 추가
        else
        {
            print("else 임");
            /*StoreItem curAllItem = AllItemList.Find(x => x.Name == curItem.Name);
            curAllItem.Number = "1";
            if (curAllItem != null)
            {
                // 아이템 추가
                MyItemList.Add(curItem);
                // 돈 감소
                CoinList[0].Money = (int.Parse(CoinList[0].Money) - int.Parse(curItem.Price)).ToString();

            }*/

        }

        Save();
        BuyPanel.SetActive(false);
    }
    // 취소하기 버튼
    public void CancleClick(int slotNum)
    {
        BuyPanel.SetActive(false);
    }

    public void TabClick(string tabName)
    {
        // 현재 아이템 리스트에 클릭한 타입만 추가
        curType = tabName;
        CurItemList = AllItemList.FindAll(x => x.Type == tabName);

        // 슬롯과 텍스트 보이기
        for(int i =0; i< Slot.Length; i++)
        {
            bool isExist = i < CurItemList.Count;
            Slot[i].SetActive(isExist);
            Slot[i].GetComponentInChildren<Text>().text = isExist ? CurItemList[i].Name : "";
            Slot[i].transform.GetChild(2).GetComponentInChildren<Text>().text = isExist ? CurItemList[i].Price : "";

            if (isExist)
            {
                ItemImage[i].sprite = ItemSprite[AllItemList.FindIndex(x => x.Name == CurItemList[i].Name)];
            }


        }

        // 탭 이미지
        int tabNum = 0;
        switch (tabName)
        {
            case "Snack": tabNum = 0; break;
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
        ExplainPanel.transform.GetChild(2).GetComponent<Image>().sprite = Slot[slotNum].transform.GetChild(3).GetComponent<Image>().sprite;
        // 가격
        ExplainPanel.transform.GetChild(3).GetComponent<Text>().text = CurItemList[slotNum].Price;
        // 설명
        ExplainPanel.transform.GetChild(4).GetComponent<Text>().text = CurItemList[slotNum].Explain;
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
        // 상점 전체리스트 저장
        string jdata = JsonConvert.SerializeObject(AllItemList);
        File.WriteAllText(Application.dataPath + "/JSON_files/StoreItemText.txt", jdata);

        // 인벤토리 정보 저장
        string jdata_my = JsonConvert.SerializeObject(MyItemList);
        File.WriteAllText(Application.dataPath + "/JSON_files/MyItemText.txt", jdata_my);

        // 돈 정보 저장
        string jdata_coin = JsonConvert.SerializeObject(CoinList);
        File.WriteAllText(Application.dataPath + "/JSON_files/CoinText.txt", jdata_coin);

        // 체력 정보 저장
        string jdata_hp = JsonConvert.SerializeObject(HPList);
        File.WriteAllText(Application.dataPath + "/JSON_files/HPText.txt", jdata_hp);

    }

    void Load()
    {
        string jdata = File.ReadAllText(Application.dataPath + "/JSON_files/StoreItemText.txt");
        StoreItemList = JsonConvert.DeserializeObject<List<StoreItem>>(jdata);

        string jdata_my = File.ReadAllText(Application.dataPath + "/JSON_files/MyItemText.txt");
        MyItemList = JsonConvert.DeserializeObject<List<StoreItem>>(jdata_my);

        string jdata_coin = File.ReadAllText(Application.dataPath + "/JSON_files/CoinText.txt");
        CoinList = JsonConvert.DeserializeObject<List<Coin>>(jdata_coin);

        string jdata_hp = File.ReadAllText(Application.dataPath + "/JSON_files/HPText.txt");
        HPList = JsonConvert.DeserializeObject<List<Hungry>>(jdata_hp);

        

        TabClick(curType);
    }

}
