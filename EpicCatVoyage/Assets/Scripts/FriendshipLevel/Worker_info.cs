using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Level_Controller;
using Newtonsoft.Json;
using System.IO;

public class Worker_info : MonoBehaviour
{
    public List<NPCdata> npcData;

    public GameObject workInfoScreen;
    public Text workInfoLike;
    public string[] talks;
    public Text workInfoTalk;
    public Animator screenAnim;

    // Start is called before the first frame update
    void Start()
    {
        screenAnim = workInfoScreen.GetComponent<Animator>();
        npcData = StoreInfo.getFriendshipList();
        workInfoLike.text = "È£°¨µµ : " + npcData[2].friendship_level.ToString(); 

        int index = (npcData[2].friendship_level - 1) / 25;
        workInfoTalk.text = talks[index];
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
