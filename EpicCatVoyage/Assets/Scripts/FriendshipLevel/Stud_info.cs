using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Stud_info : MonoBehaviour
{
    public List<NPCdata> npcData = new List<NPCdata>();

    public GameObject studInfoScreen;
    public Text studInfoLike;
    public Text studInfoTalk;
    public string[] talks;
    public Animator screenAnim;

    // Start is called before the first frame update
    void Start()
    {
        screenAnim = studInfoScreen.GetComponent<Animator>();
        string jdata = File.ReadAllText(Application.streamingAssetsPath + "/JSON_files/NPCdata.json");
        npcData = JsonConvert.DeserializeObject<List<NPCdata>>(jdata);
        studInfoLike.text = "È£°¨µµ : " + npcData[0].friendship_level.ToString();

        int index = (npcData[0].friendship_level - 1) / 25;
        studInfoTalk.text = talks[index];
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
