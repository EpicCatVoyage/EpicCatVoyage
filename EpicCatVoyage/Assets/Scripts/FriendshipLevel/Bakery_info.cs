using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Level_Controller;
using Newtonsoft.Json;
using System.IO;

public class Bakery_info : MonoBehaviour
{
    public List<NPCdata> npcData = new List<NPCdata>();

    public GameObject BakeryInfoScreen;
    public Text BakeryInfoLike;
    public Text BakeryInfoTalk;
    public Animator screenAnim;

    // Start is called before the first frame update
    void Start()
    {
        screenAnim = BakeryInfoScreen.GetComponent<Animator>();
        string jdata = File.ReadAllText(Application.streamingAssetsPath + "/JSON_files/NPCdata.json");
        npcData = JsonConvert.DeserializeObject<List<NPCdata>>(jdata);
        BakeryInfoLike.text = "È£°¨µµ : " + npcData[1].friendship_level.ToString();
    }

    public void clickBake()
    {
        screenAnim.SetTrigger("downBakery");
    }

    public void clickBtn()
    {
        screenAnim.SetTrigger("upBakery");
    }
}
