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
    public List<ShopItem> ShopItemList, MyItemList, CurItemList;
    public string curType = "Snack";
    public GameObject[] Slot;
    public Image[] TabImage, ShopItemImage;
    public Sprite TabIdleSprite, TabSelectSprite;
    public Sprite[] ShopItemSprite;
    public GameObject ExplainPanel;
    public RectTransform CanvasRect;
    IEnumerator PointerCoroutine;
    RectTransform ExplainRect;



    void Start()
    {
        // ���� ������ ����Ʈ �ҷ�����
        string[] line = ShopItemDatabase.text.Substring(0, ShopItemDatabase.text.Length - 1).Split('\n');
        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');

            ShopItemList.Add(new ShopItem(row[0], row[1], row[2], row[3], row[4] == "TRUE"));
        }
        //print(Slot.Length);
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

    }


    public void TabClick(string tabName)
    {
        // ���� ������ ����Ʈ�� Ŭ���� Ÿ�Ը� �߰�
        curType = tabName;
        CurItemList = ShopItemList.FindAll(x => x.Type == tabName);

        // ���԰� �ؽ�Ʈ ���̱�
        for(int i=0; i < Slot.Length; i++)
        {
            bool isExist = i < CurItemList.Count;
            Slot[i].SetActive(isExist);
            Slot[i].GetComponentInChildren<Text>().text = isExist ? CurItemList[i].Name : "";
            Slot[i].transform.GetChild(3).GetComponentInChildren<Text>().text = isExist ? CurItemList[i].Number : "";

            // ������ �̹���
            if (isExist)
            {

                //ShopItemImage[i].sprite = ShopItemSprite[ShopItemList.FindIndex(x => x.Name == CurItemList[i].Name)];
                /*try
                {
                    ShopItemImage[i].sprite = ShopItemSprite[ShopItemList.FindIndex(x => x.Name == CurItemList[i].Name)];
                }
                catch (IndexOutOfRangeException e)
                {
                    print("���ܸ޽��� : {0}", e.Message);
                    print("���ܰ� �߻��� ��(namespace): {0}", e.Source);
                    print("���ܰ� �߻��� ��(method): {0}", e.TargetSite);
                    print("���ܰ� �߻��� ��(line): {0}", e.StackTrace);
                }*/


            }
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
        string jdata = JsonConvert.SerializeObject(ShopItemList);
        File.WriteAllText(Application.dataPath + "/JSON_files/ShopItemText.txt",jdata);
    }

    void Load()
    {
        string jdata = File.ReadAllText(Application.dataPath + "/JSON_files/ShopItemText.txt");
        ShopItemList = JsonConvert.DeserializeObject<List<ShopItem>>(jdata);


        TabClick(curType);
    }
}
