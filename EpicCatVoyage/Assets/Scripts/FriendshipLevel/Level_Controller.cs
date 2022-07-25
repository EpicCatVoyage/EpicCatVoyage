using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Newtonsoft.Json;

public class Level_Controller : MonoBehaviour
{
    public List<NPCdata> npcData = new List<NPCdata>();

    private void Start()
    {
        npcData.Add(new NPCdata("���ʵ�", 10, "�ʵ��л�", 10));
        npcData.Add(new NPCdata("�軧", 68, "���� ����", 5));
        npcData.Add(new NPCdata("������", 33, "������", 0));
        npcData.Add(new NPCdata("�����", 48, "������������", -5));

        SaveNPCdataToJson();
        LoadNPCdataFromJson();
    }

    public void SaveNPCdataToJson()
    {
        string jdata = JsonConvert.SerializeObject(npcData);
        File.WriteAllText(Application.dataPath + "/NPCdata.json", jdata);
    }

    public void LoadNPCdataFromJson()
    {
        string jdata = File.ReadAllText(Application.dataPath + "/NPCdata.json");
        npcData = JsonConvert.DeserializeObject<List<NPCdata>>(jdata);
    }

    [System.Serializable]
    public class NPCdata
    {
        public string name;
        public int age;
        public string job;
        public int friendship_level;

        public NPCdata(string name, int age, string job, int friendship_level)
        {
            this.name = name;
            this.age = age;
            this.job = job;
            this.friendship_level = friendship_level;
        }
    }
}
