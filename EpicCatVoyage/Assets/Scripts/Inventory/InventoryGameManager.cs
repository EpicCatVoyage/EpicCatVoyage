using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Newtonsoft.Json;

// ����ȭ
[System.Serializable]
public class Item
{
    // ������
    public Item(string _Type, string _Name, string _Explain, string _Price, string _Number, string _Exp, bool _isUsing)
    {
        Type = _Type; Name = _Name; Explain = _Explain; Price = _Price; Number = _Number; Exp = _Exp; isUsing = _isUsing;
    }

    // Ÿ��, �̸�, ����, ����, ��뿩��
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
    // ���� ���� �����ִ��� ó������ ����
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

    //�����
    public InputField ItemNameInput, ItemNumberInput;


    void Start()
    {

        // ��ü ������ ����Ʈ �ҷ�����
        // ������ ���� �����
        /*string[] line = ItemDatabase.text.Substring(0, ItemDatabase.text.Length - 1).Split('\n');
        print(line.Length);
      
        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');

            AllItemList.Add(new Item(row[0], row[1], row[2], row[3], row[4], row[5], row[6] == "TRUE"));
        }*/
        Load();

        // �� ����ϱ�
        Coin[0].GetComponentInChildren<Text>().text = CoinList[0].Money;
        print(CoinList[0].Money);

        // ����� ����ϱ�
        Hungry[0].GetComponentInChildren<Text>().text = HPList[0].HP;
        print(HPList[0].HP);
        //ĳ��
        ExplainRect = ExplainPanel.GetComponent<RectTransform>();

    }

    private void Update()
    {
       
        RectTransformUtility.ScreenPointToLocalPointInRectangle(CanvasRect, Input.mousePosition, Camera.main, out Vector2 anchoredPos);
        ExplainRect.anchoredPosition = anchoredPos + new Vector2(-180, -165);
    }

    // �����
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
            // ��ü���� ���� �������� ã�� �� �����ۿ� �߰�
            Item curAllItem = AllItemList.Find(x => x.Name == ItemNameInput.text);
            // �ڵ� 1�� �߰�
           

            if(curAllItem != null)
            {
                curAllItem.Number = ItemNumberInput.text;
                MyItemList.Add(curAllItem);
            }
        }
        
        Save();
    }

    // �����
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

    //�����
    public void ResetItemClick()
    {
        Item BasicItem = AllItemList.Find(x => x.Name == "��Ź");
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

    // ����ϱ� ��ư ������ ��
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
                // hp ����
                HPList[0].HP = curHp.ToString();
                // hp ����
                Hungry[0].GetComponentInChildren<Text>().text = HPList[0].HP;
            }
            else
            {
                // hp ����
                HPList[0].HP = "100";
                // hp ����
                Hungry[0].GetComponentInChildren<Text>().text = HPList[0].HP;
            }
            /*// hp ����
            HPList[0].HP = (int.Parse(HPList[0].HP) + int.Parse(curItem.Exp)).ToString();

            // hp ����
            Hungry[0].GetComponentInChildren<Text>().text = HPList[0].HP;*/

            Save();
            HowPanel.SetActive(false);
        }
    }

    // �ȱ� ��ư ������ ��
    public void SellingItemClick()
    {
        Item curItem = MyItemList.Find(x => x.Name == HowPanel.transform.GetChild(1).GetComponent<Text>().text);
        if (curItem != null)
        {
            int curNumber = int.Parse(curItem.Number) - 1;
            print("���� ���� : " + curNumber);

            if (curNumber < 1)
            {
                print("1������");
                MyItemList.Remove(curItem);

            }
            else
            {
                print("1�� �̻��̿���");
                curItem.Number = curNumber.ToString();
            }

            // �� ����
            CoinList[0].Money = (int.Parse(CoinList[0].Money) + int.Parse(curItem.Price)*0.8 ).ToString();

            // �� ����
            Coin[0].GetComponentInChildren<Text>().text = CoinList[0].Money;

            Save();
            HowPanel.SetActive(false);
        }
    }


    // ������ �� ���� �ٲٱ�
    public void TabClick(string tabName)
    {
        // ���� ������ ����Ʈ�� Ŭ���� Ÿ�Ը� �߰�
        curType = tabName;
        curItemList = MyItemList.FindAll(x => x.Type == tabName);

        // ���԰� �ؽ�Ʈ ���̱�
        for(int i=0; i < Slot.Length; i++)
        {
            bool isExist = i < curItemList.Count;
            Slot[i].SetActive(isExist);
            Slot[i].GetComponentInChildren<Text>().text = isExist ? curItemList[i].Name : "";
            //Slot[i].GetComponentInChildren<Text>().text = isExist ? curItemList[i].Name + "/" + curItemList[i].isUsing : "";

            if (isExist)
            {
                // ������ �̹���
                ItemImage[i].sprite = ItemSprite[AllItemList.FindIndex(x => x.Name == curItemList[i].Name)];
                /*UsingImage[i].SetActive(curItemList[i].isUsing);*/
                // ���ٹ̱⸸ ��������� �߰�
                /*if (curType == "Home")
                {
                    UsingImage[i].SetActive(curItemList[i].isUsing);
                }*/
            }
        }

        // �� ���� �ٲٱ�
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

    // ���� ���̱�
    public void PointerEnter(int slotNum)
    {
        // ���Կ� ���콺 �ø��� 0.5�� �Ŀ� ����â
        PointerCoroutine = PointerEnterDelay(slotNum);
        StartCoroutine(PointerCoroutine);

        // �̸�
        ExplainPanel.GetComponentInChildren<Text>().text = curItemList[slotNum].Name;
        // �̹���
        ExplainPanel.transform.GetChild(2).GetComponent<Image>().sprite = Slot[slotNum].transform.GetChild(1).GetComponent<Image>().sprite;
        // ����
        ExplainPanel.transform.GetChild(3).GetComponent<Text>().text = curItemList[slotNum].Number + "��";
        // ����
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

        // �κ��丮 ���� ����
        string jdata_my = JsonConvert.SerializeObject(MyItemList);
        File.WriteAllText(Application.streamingAssetsPath + "/JSON_files/MyItemText.txt", jdata_my);

        // �� ���� ����
        string jdata_coin = JsonConvert.SerializeObject(CoinList);
        File.WriteAllText(Application.streamingAssetsPath + "/JSON_files/CoinText.txt", jdata_coin);

        // ü�� ���� ����
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

