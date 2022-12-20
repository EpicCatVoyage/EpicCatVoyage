using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Level_Controller;
using Newtonsoft.Json;
using System.IO;

public class Worker_info : MonoBehaviour
{
    public List<NPCdata> npcData = new List<NPCdata>();

    public GameObject workInfoScreen;
    public Text workInfoLike;
    public Text workInfoTalk;
    public Animator screenAnim;

    // Start is called before the first frame update
    void Start()
    {
        screenAnim = workInfoScreen.GetComponent<Animator>();
        string jdata = File.ReadAllText(Application.streamingAssetsPath + "/JSON_files/NPCdata.json");
        npcData = JsonConvert.DeserializeObject<List<NPCdata>>(jdata);
        workInfoLike.text = "ȣ���� : " + npcData[2].friendship_level.ToString();
    }

    public void clickWork()
    {
        screenAnim.SetTrigger("upWorker");
    }

    public void clickBtn()
    {
        screenAnim.SetTrigger("downWorker");
    }
}
