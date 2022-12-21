using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Level_Controller;
using Newtonsoft.Json;
using System.IO;

public class Bakery_info : MonoBehaviour
{
    public List<NPCdata> npcData;

    public GameObject BakeryInfoScreen;
    public Text BakeryInfoLike;
    public Text BakeryInfoTalk;
    public string[] talks;
    public Animator screenAnim;

    // Start is called before the first frame update
    void Start()
    {
        screenAnim = BakeryInfoScreen.GetComponent<Animator>();
        npcData = StoreInfo.getFriendshipList();
        BakeryInfoLike.text = "È£°¨µµ : " + npcData[1].friendship_level.ToString();

        int index = (npcData[1].friendship_level - 1) / 25;
        BakeryInfoTalk.text = talks[index];
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
