using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 직렬화
[System.Serializable]
public class Item
{
    // 생성자
    public Item(string _Type, string _Name, string _Explain, string _Number, bool _isUsing)
    {Type = _Type; Name = _Name; Explain = _Explain; Number = _Number; isUsing = _isUsing; }

    // 타입, 이름, 설명, 개수, 사용여부
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

        // 전체 아이템 리스트 불러오기
        // 마지막 엔터 지우기
        string[] line = ItemDatabase.text.Substring(0, ItemDatabase.text.Length - 1).Split('\n');
        // print(line.Length);
        for(int i=0; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');

            AllItemList.Add(new Item(row[0], row[1], row[2], row[3], row[4] == "TRUE"));
        }
    }

    
}
