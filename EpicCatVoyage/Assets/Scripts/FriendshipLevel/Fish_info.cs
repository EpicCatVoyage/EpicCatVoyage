using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Level_Controller;
using Newtonsoft.Json;
using System.IO;

public class Fish_info : MonoBehaviour
{
    public List<NPCdata> npcData = new List<NPCdata>();

    public GameObject fishInfoScreen;
    public Text fishInfoLike;
    public Text fishInfoTalk;
    public Animator screenAnim;

    // Start is called before the first frame update
    void Start()
    {
        screenAnim = fishInfoScreen.GetComponent<Animator>();
        string jdata = File.ReadAllText(Application.streamingAssetsPath + "/JSON_files/NPCdata.json");
        npcData = JsonConvert.DeserializeObject<List<NPCdata>>(jdata);
        fishInfoLike.text = "È£°¨µµ : " + npcData[3].friendship_level.ToString();
    }

    public void clickFish()
    {
        screenAnim.SetTrigger("downFish");
    }

    public void clickBtn()
    {
        screenAnim.SetTrigger("upFish");
    }
}
