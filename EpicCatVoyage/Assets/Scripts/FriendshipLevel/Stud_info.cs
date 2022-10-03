using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Level_Controller;
using Newtonsoft.Json;
using System.IO;

public class Stud_info : MonoBehaviour
{
    public List<NPCdata> npcData = new List<NPCdata>();

    public GameObject studInfoScreen;
    public Text studInfoLike;
    public Text studInfoTalk;
    public Animator screenAnim;

    // Start is called before the first frame update
    void Start()
    {
        screenAnim = studInfoScreen.GetComponent<Animator>();
        string jdata = File.ReadAllText(Application.dataPath + "/JSON_files/NPCdata.json");
        npcData = JsonConvert.DeserializeObject<List<NPCdata>>(jdata);
        studInfoLike.text = "È£°¨µµ : " + npcData[0].friendship_level.ToString();
    }

    public void clickStud()
    {
        screenAnim.SetTrigger("upStudent");
    }

    public void clickBtn()
    {
        screenAnim.SetTrigger("downStudent");
    }
}
