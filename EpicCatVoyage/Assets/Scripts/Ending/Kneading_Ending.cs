using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Level_Controller;
using Newtonsoft.Json;
using System.IO;


public class Kneading_Ending : MonoBehaviour
{

    public List<NPCdata> npcData = new List<NPCdata>();

    // Start is called before the first frame update
    void Start()
    {
        string jdata = File.ReadAllText(Application.dataPath + "/JSON_files/NPCdata.json");
        npcData = JsonConvert.DeserializeObject<List<NPCdata>>(jdata);
        // friendship_test();       // 이런식으로 호감도 조작 가능
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void friendship_test()
    {
        npcData[0].friendship_level += 100;
        string jdata = JsonConvert.SerializeObject(npcData);
        File.WriteAllText(Application.dataPath + "/JSON_files/NPCdata.json", jdata);
    }
}
