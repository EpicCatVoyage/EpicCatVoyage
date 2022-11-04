using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����ȭ
[System.Serializable]
public class Item
{
    // ������
    public Item(string _Type, string _Name, string _Explain, string _Number, bool _isUsing)
    {Type = _Type; Name = _Name; Explain = _Explain; Number = _Number; isUsing = _isUsing; }

    // Ÿ��, �̸�, ����, ����, ��뿩��
    public string Type, Name, Explain, Number;
    public bool isUsing;
}

public class InventoryGameManager : MonoBehaviour
{
    public TextAsset ItemDatabase;
    public List<Item> AllItemList;

    void Start()
    {
        //print(ItemDatabase.text);

        // ��ü ������ ����Ʈ �ҷ�����
        // ������ ���� �����
        string[] line = ItemDatabase.text.Substring(0, ItemDatabase.text.Length - 1).Split('\n');
        // print(line.Length);
        for(int i=0; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');

            AllItemList.Add(new Item(row[0], row[1], row[2], row[3], row[4] == "TRUE"));
        }
    }

    
}
