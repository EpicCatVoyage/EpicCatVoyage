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
    public Item(string _Type, string _Name, string _Explain, string _Number, bool _isUsing)
    { Type = _Type; Name = _Name; Explain = _Explain; Number = _Number; isUsing = _isUsing; }

    // Ÿ��, �̸�, ����, ����, ��뿩��
    public string Type, Name, Explain, Number;
    public bool isUsing;
}

public class InventoryGameManager : MonoBehaviour
{
    public TextAsset ItemDatabase;
    public List<Item> AllItemList, MyItemList, curItemList;
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

    //�����
    public InputField ItemNameInput, ItemNumberInput;


    void Start()
    {
        //print(ItemDatabase.text);

        // ��ü ������ ����Ʈ �ҷ�����
        // ������ ���� �����
        string[] line = ItemDatabase.text.Substring(0, ItemDatabase.text.Length - 1).Split('\n');
        print(line.Length);
      
        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');

            AllItemList.Add(new Item(row[0], row[1], row[2], row[3], row[4] == "TRUE"));
        }
        Load();
        //ĳ��
        //ExplainRect = ExplainPanel.GetComponent<RectTransform>();

    }

    private void Update()
    {
        

        RectTransformUtility.ScreenPointToLocalPointInRectangle(CanvasRect, Input.mousePosition, Camera.main, out Vector2 anchoredPos);
        ExplainPanel.GetComponent<RectTransform>().anchoredPosition = anchoredPos + new Vector2(-180, -165);
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

    public void SlotClick(int slotNum)
    {
        
        Item CurItem = curItemList[slotNum];
        Item UsingItem = curItemList.Find(x => x.isUsing == true);

        if(curType == "Home")
        {

            
            /*if(UsingItem!= null)
            {
                UsingItem.isUsing = false;
            }*/
            
            // ����, ���� ����
            if(CurItem.isUsing == true)
            {
                CurItem.isUsing = false;

            }
            else
            {
                CurItem.isUsing = true;
            }

        }
        else{
            if(UsingItem!= null)
            {
                UsingItem.isUsing = false;
            }
        }

        Save();
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
            Slot[i].GetComponentInChildren<Text>().text = isExist ? curItemList[i].Name + "/" + curItemList[i].isUsing : "";

            if (isExist)
            {
                // ������ �̹���
                ItemImage[i].sprite = ItemSprite[AllItemList.FindIndex(x => x.Name == curItemList[i].Name)];

                // ���ٹ̱⸸ ��������� �߰�
                if(curType == "Home")
                {
                    UsingImage[i].SetActive(curItemList[i].isUsing);
                }
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
        StopCoroutine(PointerEnterDelay(slotNum));
        ExplainPanel.SetActive(false);
    }

    void Save()
    {
        string jdata = JsonConvert.SerializeObject(MyItemList);
        File.WriteAllText(Application.dataPath + "/JSON_files/MyItemText.txt", jdata);

        TabClick(curType);


    }

    void Load()
    {
        string jdata = File.ReadAllText(Application.dataPath + "/JSON_files/MyItemText.txt");
        MyItemList = JsonConvert.DeserializeObject<List<Item>>(jdata);

        TabClick(curType);
    }


}

