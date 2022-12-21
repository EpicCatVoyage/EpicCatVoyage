using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

// 직렬화
[System.Serializable]
public class GiftItem
{
    // 생성자
    public GiftItem(string _Type, string _Name, string _Explain, string _Price, string _Number, string _Exp, bool _isUsing)
    {
        Type = _Type; Name = _Name; Explain = _Explain; Price = _Price; Number = _Number; Exp = _Exp; isUsing = _isUsing;
    }

    // 타입, 이름, 설명, 개수, 사용여부
    public string Type, Name, Explain, Price, Number, Exp;
    public bool isUsing;
}

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

    // 선물
    public List<GiftItem> AllItemList, GiftList, MyItemList;

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
        Load();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void startGame()
    {
        StoreInfo.setHungry(StoreInfo.getHungry() - 5); // 배고픔 감소
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
            if (hungry > 5 )
            {
                hungryStat.color = Color.white;
            }
            if (hungry > 100)
            {
                hungry = 100;
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
            if (like > 100)
            {
                like = 100;
            }
            loveBar.transform.localScale = new Vector3(like, 1, 1);
            loveBar.transform.position = new Vector3(like * 6.2f / 2f + 645, 1267.11f, 1);
        }

        string jdata_coin = File.ReadAllText(Application.streamingAssetsPath + "/JSON_files/CoinText.txt");
        coinList = JsonConvert.DeserializeObject<List<CoinMoney>>(jdata_coin);
        int coin = int.Parse(coinList[0].Money);
        coinTxt.text = coin.ToString();
    }

    public void getGift()
    {
        GiftList = AllItemList.FindAll(x => x.Type == "Gift");
        int i = Random.Range(1, 6);

        if (i < 2)
        {
            GiftItem GiftItem = MyItemList.Find(x => x.Type == "Gift");


            // 아이템을 가지고 있을때는 개수만 1개 추가
            if (GiftItem != null)
            {
                // 아이템 추가
                GiftItem.Number = (int.Parse(GiftItem.Number) + 1).ToString();
            }

            // 없을 때는 아이템 자체를 추가
            else
            {
                print("else 임");
                GiftItem GiftAllItem = AllItemList.Find(x => x.Type == "Gift");
                GiftAllItem.Number = "1";
                if (GiftAllItem != null)
                {
                    // 아이템 추가
                    MyItemList.Add(GiftAllItem);


                }

            }
        }
        Save();
    }

    public void ToCenter()
    {
        Center.SetActive(true);
        gameObject.SetActive(false);
        getGift();

        
    }

    public void ToCity()
    {
        City.SetActive(true);
        gameObject.SetActive(false);
        getGift();
    }

    public void ToHome()
    {
        Home.SetActive(true);
        gameObject.SetActive(false);
        getGift();
    }

    public void ToMarket()
    {
        Market.SetActive(true);
        gameObject.SetActive(false);
        getGift();
    }

    public void ToResidential()
    {
        Residential.SetActive(true);
        gameObject.SetActive(false);
        getGift();
    }

    public void ToSchool()
    {
        School.SetActive(true);
        gameObject.SetActive(false);
        getGift();
    }

    void Save()
    {
        // 인벤토리 정보 저장
        string jdata_my = JsonConvert.SerializeObject(MyItemList);
        File.WriteAllText(Application.streamingAssetsPath + "/JSON_files/MyItemText.txt", jdata_my);
    }

    void Load()
    {
        string jdata = File.ReadAllText(Application.streamingAssetsPath + "/JSON_files/StoreItemText.txt");
        AllItemList = JsonConvert.DeserializeObject<List<GiftItem>>(jdata);

        string jdata_my = File.ReadAllText(Application.streamingAssetsPath + "/JSON_files/MyItemText.txt");
        MyItemList = JsonConvert.DeserializeObject<List<GiftItem>>(jdata_my);
    }
}
