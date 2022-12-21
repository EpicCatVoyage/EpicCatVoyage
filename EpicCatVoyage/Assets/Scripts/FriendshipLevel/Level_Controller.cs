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

    public List<NPCdata> npcData;

    private void Start()
    {
        npcData = StoreInfo.getFriendshipList();
        print(npcData);
        //json data �ҷ�����
        studentLike.text = "ȣ���� : " + npcData[0].friendship_level.ToString();
        bakeryOwnerLike.text = "ȣ���� : " + npcData[1].friendship_level.ToString();
        workerLike.text = "ȣ���� : " + npcData[2].friendship_level.ToString();
        fishOwnerLike.text = "ȣ���� : " + npcData[3].friendship_level.ToString();
    }

    public void SaveNPCdataToJson()
    {
        string jdata = JsonConvert.SerializeObject(npcData);
        File.WriteAllText(Application.streamingAssetsPath + "/JSON_files/NPCdata.json", jdata);
    }

    public void LoadNPCdataFromJson()
    {
        string jdata = File.ReadAllText(Application.streamingAssetsPath + "/JSON_files/NPCdata.json");
        npcData = JsonConvert.DeserializeObject<List<NPCdata>>(jdata);
    }
}
