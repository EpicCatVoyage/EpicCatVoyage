using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MoveController : MonoBehaviour
{
    string[] game = { "NoteGameMain", "MainGame", "Baking", "CatchMouse", "miniGame_kneading" };

    public GameObject Center;
    public GameObject City;
    public GameObject Home;
    public GameObject Market;
    public GameObject Residential;
    public GameObject School;
    public GameObject UI;
    public GameObject Fade;

    // 배고픔
    public List<Hungryhp> HPList;
    public GameObject hungryBar;
    public Text hungryStat;

    // 호감도
    public List<NPCdata> npcData = new List<NPCdata>();
    public GameObject loveBar;
    public Text loveStat;

    // stage 확인용 - 클래스 이름 고쳐야함
    public static int stage = 3;

    private Animator anim;
    private bool animState = false;


    // Start is called before the first frame update
    void Start()
    {
        anim = UI.GetComponent<Animator>();
        anim.SetBool("State", animState);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void startGame()
    {
        int num = Random.Range(0, 5);
        Fade f = Fade.GetComponent<Fade>();
        f.L_Fadeout(game[num]);
    }

    public void moveUI()
    {
        if (animState == false)
            animState = true;
        else
            animState = false;

        anim.SetBool("State", animState);

        // 배고픔 불러오기
        string jdata_hp = File.ReadAllText(Application.streamingAssetsPath + "/JSON_files/HPText.txt");
        HPList = JsonConvert.DeserializeObject<List<Hungryhp>>(jdata_hp);
        int hungry = int.Parse(HPList[0].HP);
        hungryStat.text = hungry.ToString();
        // 0일때 width = 6.2 // 100일때 width = 620

        if (hungry > 0)
        {
            if(hungry > 5)
            {
                hungryStat.color = Color.white;
            }
            hungryBar.transform.localScale = new Vector3(hungry, 1, 1);
            hungryBar.transform.position = new Vector3(hungry * 6.2f / 2f + 645, 1161.30f, 1);
        }


        // 호감도 불러오기
        string jdata = File.ReadAllText(Application.streamingAssetsPath + "/JSON_files/NPCdata.json");
        npcData = JsonConvert.DeserializeObject<List<NPCdata>>(jdata);
        int like = npcData[stage - 1].friendship_level;
        loveStat.text = like.ToString();
        if (like > 0)
        {
            if (like > 5)
            {
                loveStat.color = Color.white;
            }
            loveBar.transform.localScale = new Vector3(like, 1, 1);
            loveBar.transform.position = new Vector3(like * 6.2f / 2f + 645, 1267.11f, 1);
        }



    }

    public void ToCenter()
    {
        Center.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ToCity()
    {
        City.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ToHome()
    {
        Home.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ToMarket()
    {
        Market.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ToResidential()
    {
        Residential.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ToSchool()
    {
        School.SetActive(true);
        gameObject.SetActive(false);
    }
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
