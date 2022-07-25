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
        npcData.Add(new NPCdata("김초딩", 10, "초등학생", 10));
        npcData.Add(new NPCdata("김빵", 68, "빵집 주인", 5));
        npcData.Add(new NPCdata("김직장", 33, "직장인", 0));
        npcData.Add(new NPCdata("김생선", 48, "생선가게주인", -5));

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
