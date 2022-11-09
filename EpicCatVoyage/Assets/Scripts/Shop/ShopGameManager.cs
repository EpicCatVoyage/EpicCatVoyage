using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.UI;


[System.Serializable]
public class ShopItem
{
    // ������
    public ShopItem(string _Type, string _Name, string _Explain, string _Number, bool _isUsing)
    {
        Type = _Type; Name = _Name; Explain = _Explain; Number = _Number; isUsing = _isUsing;
    }

    public string Type, Name, Explain, Number;
    public bool isUsing;
    
}




public class ShopGameManager : MonoBehaviour
{
    public TextAsset ShopItemDatabase;
    public List<ShopItem> AllShopItemList, ShopItemList, CurItemList, MyItemList;
    public string curType = "Snack";
    public GameObject[] Slot;
    public Image[] TabImage, ShopItemImage;
    public Sprite TabIdleSprite, TabSelectSprite;
    public Sprite[] ShopItemSprite;
    public GameObject ExplainPanel;
    public GameObject BuyPanel;
    public RectTransform CanvasRect;
    IEnumerator PointerCoroutine;
    RectTransform ExplainRect;
    string shopfilePath;
    public GameObject buyBtn;
    public GameObject cancleBtn;



    void Start()
    {
        // ���� ������ ����Ʈ �ҷ�����
        string[] line = ShopItemDatabase.text.Substring(0, ShopItemDatabase.text.Length - 1).Split('\n');
        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');

            AllShopItemList.Add(new ShopItem(row[0], row[1], row[2], row[3], row[4] == "TRUE"));
        }
        shopfilePath = Application.persistentDataPath + "/ShopItemText.txt";
        //Save();
        Load();
        ExplainRect = ExplainPanel.GetComponent<RectTransform>();
    }

    private void Update()
    {

        RectTransformUtility.ScreenPointToLocalPointInRectangle(CanvasRect, Input.mousePosition, Camera.main, out Vector2 anchoredPos);
        ExplainRect.anchoredPosition = anchoredPos + new Vector2(-180, -165);
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
                curAllItem.Number = "1";
                MyItemList.Add(curItem);
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
        // ���� ������ ����Ʈ�� Ŭ���� Ÿ�Ը� �߰�
        curType = tabName;
        CurItemList = AllShopItemList.FindAll(x => x.Type == tabName);

        // ���԰� �ؽ�Ʈ ���̱�
        for(int i=0; i < Slot.Length; i++)
        {
            bool isExist = i < CurItemList.Count;
            Slot[i].SetActive(isExist);
            Slot[i].GetComponentInChildren<Text>().text = isExist ? CurItemList[i].Name : "";
            Slot[i].transform.GetChild(3).GetComponentInChildren<Text>().text = isExist ? CurItemList[i].Number : "";

            // ������ �̹���
            /*if (isExist)
            {

                //ShopItemImage[i].sprite = ShopItemSprite[ShopItemList.FindIndex(x => x.Name == CurItemList[i].Name)];
                *//*try
                {
                    ShopItemImage[i].sprite = ShopItemSprite[ShopItemList.FindIndex(x => x.Name == CurItemList[i].Name)];
                }
                catch (IndexOutOfRangeException e)
                {
                    print("���ܸ޽��� : {0}", e.Message);
                    print("���ܰ� �߻��� ��(namespace): {0}", e.Source);
                    print("���ܰ� �߻��� ��(method): {0}", e.TargetSite);
                    print("���ܰ� �߻��� ��(line): {0}", e.StackTrace);
                }*//*


            }*/
        }

        // �� �̹���
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

    // ���콺 �÷������� ����â
    public void PointerEnter(int slotNum)
    {
        PointerCoroutine = PointerEnterDelay(slotNum);
        StartCoroutine(PointerCoroutine);

        // �̸�
        ExplainPanel.GetComponentInChildren<Text>().text = CurItemList[slotNum].Name;
        // �̹���
        ExplainPanel.transform.GetChild(2).GetComponent<Image>().sprite = Slot[slotNum].transform.GetChild(1).GetComponent<Image>().sprite;
        // ����
        ExplainPanel.transform.GetChild(3).GetComponent<Text>().text = CurItemList[slotNum].Number;
        // ����
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

        //File.WriteAllText(shopfilePath,"����");
        string jdata = JsonConvert.SerializeObject(ShopItemList);
        File.WriteAllText(Application.dataPath + "/JSON_files/ShopItemText.txt", jdata);

        string jdata_my = JsonConvert.SerializeObject(MyItemList);
        File.WriteAllText(Application.dataPath + "/JSON_files/MyItemText.txt", jdata_my);
        TabClick(curType);
    }

    void Load()
    {
        /*if (!File.Exists(shopfilePath))
        {
            ResetItemClick();
            return;
        }*/
        //string jdata = File.ReadAllText(shopfilePath);
        //ShopItemList = JsonConvert.DeserializeObject<List<ShopItem>>(jdata);

        string jdata = File.ReadAllText(Application.dataPath + "/JSON_files/ShopItemText.txt");
        ShopItemList = JsonConvert.DeserializeObject<List<ShopItem>>(jdata);

        string jdata_my = File.ReadAllText(Application.dataPath + "/JSON_files/MyItemText.txt");
        MyItemList = JsonConvert.DeserializeObject<List<ShopItem>>(jdata_my);


        TabClick(curType);
    }
}
