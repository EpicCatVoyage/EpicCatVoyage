using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MoveController : MonoBehaviour
{
    string[] game = { "HTP_NoteGame", "HTP_ArrowGame", "HTP_Baking", "HTP_CatchMouse", "miniGame_kneading" };

    public GameObject Center;
    public GameObject City;
    public GameObject Home;
    public GameObject Market;
    public GameObject Residential;
    public GameObject School;
    public GameObject UI;
    public GameObject Fade;

    // �����
    public List<Hungryhp> HPList;
    public GameObject hungryBar;
    public Text hungryStat;

    // ȣ����
    public List<NPCdata> npcData = new List<NPCdata>();
    public GameObject loveBar;
    public Text loveStat;

    // stage Ȯ�ο� - Ŭ���� �̸� ���ľ���
    // public static int stage = 3;

    // coin txt
    public Text coinTxt;
    public List<CoinMoney> coinList;

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

        // ����� �ҷ�����
        string jdata_hp = File.ReadAllText(Application.streamingAssetsPath + "/JSON_files/HPText.txt");
        HPList = JsonConvert.DeserializeObject<List<Hungryhp>>(jdata_hp);
        int hungry = int.Parse(HPList[0].HP);
        hungryStat.text = hungry.ToString();
        // 0�϶� width = 6.2 // 100�϶� width = 620

        // print(hungryBar.transform.position);
        // print(loveBar.transform.position);

        if (hungry > 0)
        {
            if(hungry > 5)
            {
                hungryStat.color = Color.white;
            }
            hungryBar.transform.localScale = new Vector3(hungry, 1, 1);
            hungryBar.transform.position = new Vector3(hungry * 6.2f / 2f + 645, 1161.30f, 1);
        }


        // ȣ���� �ҷ�����
        string jdata = File.ReadAllText(Application.streamingAssetsPath + "/JSON_files/NPCdata.json");
        npcData = JsonConvert.DeserializeObject<List<NPCdata>>(jdata);
        int like = npcData[StageManager.getStage() - 1].friendship_level;
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

        string jdata_coin = File.ReadAllText(Application.streamingAssetsPath + "/JSON_files/CoinText.txt");
        coinList = JsonConvert.DeserializeObject<List<CoinMoney>>(jdata_coin);
        int coin = int.Parse(coinList[0].Money);
        coinTxt.text = coin.ToString();
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
