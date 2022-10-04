using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Newtonsoft.Json;

public class Level_Controller : MonoBehaviour
{
    public Text studentLike;
    public Text bakeryOwnerLike;
    public Text workerLike;
    public Text fishOwnerLike;

    public List<NPCdata> npcData = new List<NPCdata>();

    private void Start()
    {
        npcData.Add(new NPCdata("김초딩", 10, "초등학생", 10));
        npcData.Add(new NPCdata("김빵", 68, "빵집 주인", 5));
        npcData.Add(new NPCdata("김직장", 33, "직장인", 0));
        npcData.Add(new NPCdata("김생선", 48, "생선가게주인", -5));

        SaveNPCdataToJson();
        LoadNPCdataFromJson();

        //json data 불러오기
        studentLike.text = "호감도 : " + npcData[0].friendship_level.ToString();
        bakeryOwnerLike.text = "호감도 : " + npcData[1].friendship_level.ToString();
        workerLike.text = "호감도 : " + npcData[2].friendship_level.ToString();
        fishOwnerLike.text = "호감도 : " + npcData[3].friendship_level.ToString();
    }

    public void SaveNPCdataToJson()
    {
        string jdata = JsonConvert.SerializeObject(npcData);
        File.WriteAllText(Application.dataPath + "/JSON_files/NPCdata.json", jdata);
    }

    public void LoadNPCdataFromJson()
    {
        string jdata = File.ReadAllText(Application.dataPath + "/JSON_files/NPCdata.json");
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
