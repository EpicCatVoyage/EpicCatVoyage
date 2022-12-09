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

public class StoreGameManager : MonoBehaviour
{

    public TextAsset ItemDatabase;
    public List<StoreItem> AllItemList, StoreItemList, CurItemList;
    public string curType = "Snack";
    public GameObject[] Slot;
    public Image[] TabImage, ItemImage;
    public Sprite TabIdleSprite, TabSelectSprite;
    public Sprite[] ItemSprite;
    public GameObject ExplainPanel;
    public RectTransform CanvasRect;
    IEnumerator PointerCoroutine;
    RectTransform ExplainRect;


    void Start()
    {
        // ���� ������ ����Ʈ �ҷ�����
        string[] line = ItemDatabase.text.Substring(0, ItemDatabase.text.Length - 1).Split('\n');
        for(int i =0; i< line.Length; i++)
        {
            string[] row = line[i].Split('\t');

            AllItemList.Add(new StoreItem(row[0], row[1], row[2], row[3], row[4], row[5] == "TRUE"));
        }



        Load();

        ExplainRect = ExplainPanel.GetComponent<RectTransform>();
    }

    private void Update()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(CanvasRect, Input.mousePosition, Camera.main, out Vector2 anchoredPos);
        ExplainRect.anchoredPosition = anchoredPos + new Vector2(-180, -165);
    }

    public void SlotClick(int slotNum)
    {
        //BuyPanel.SetActive(true);
    }

    public void TabClick(string tabName)
    {
        // ���� ������ ����Ʈ�� Ŭ���� Ÿ�Ը� �߰�
        curType = tabName;
        CurItemList = AllItemList.FindAll(x => x.Type == tabName);

        // ���԰� �ؽ�Ʈ ���̱�
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

        // �� �̹���
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

    // ���콺 �÷������� ����â
    public void PointerEnter(int slotNum)
    {
        PointerCoroutine = PointerEnterDelay(slotNum);
        StartCoroutine(PointerCoroutine);

        // �̸�
        ExplainPanel.GetComponentInChildren<Text>().text = CurItemList[slotNum].Name;
        // �̹���
        ExplainPanel.transform.GetChild(2).GetComponent<Image>().sprite = Slot[slotNum].transform.GetChild(3).GetComponent<Image>().sprite;
        // ����
        ExplainPanel.transform.GetChild(3).GetComponent<Text>().text = CurItemList[slotNum].Price;
        // ����
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
        // ���� ��ü����Ʈ ����
        string jdata = JsonConvert.SerializeObject(AllItemList);
        File.WriteAllText(Application.dataPath + "/JSON_files/StoreItemText.txt", jdata);
    }

    void Load()
    {
        string jdata = File.ReadAllText(Application.dataPath + "/JSON_files/StoreItemText.txt");
        StoreItemList = JsonConvert.DeserializeObject<List<StoreItem>>(jdata);

        TabClick(curType);
    }

}
