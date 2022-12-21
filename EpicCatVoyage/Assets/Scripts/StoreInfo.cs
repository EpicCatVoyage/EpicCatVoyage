using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class StoreInfo : MonoBehaviour
{
    private static int currentStage = 1;

    private static int friendship = 0;
    private static int coin = 0;
    private static int hungry = 0;

    public static List<CoinMoney> CoinList;
    public static List<Hungryhp> HPList;
    public static List<NPCdata> npcData;

    public static void init()
    {
        coinHPLoad();
        LoadNPCdataFromJson();
    }

    public static void setFriendship(int score)
    {
        init();
        currentStage = StageManager.getStage();
        // print("currentStage" + currentStage); // test
        friendship = score;
        npcData[currentStage - 1].friendship_level = friendship;
        SaveNPCdataToJson();
    }

    public static void setCoin(int getCoin)
    {
        init();
        coin = getCoin;
        CoinList[0].Money = coin.ToString();
        coinHPSave();
    }

    public static void setHungry(int h)
    {
        init();
        hungry = h;
        HPList[0].HP = h.ToString();
        coinHPSave();
    }

    public static int getFriendship()
    {
        init();
        currentStage = StageManager.getStage();
        LoadNPCdataFromJson();
        friendship = npcData[currentStage - 1].friendship_level;
        return friendship;
    }

    public static List<NPCdata> getFriendshipList()
    {
        init();
        LoadNPCdataFromJson();
        return npcData;
    }

    public static int getCoin()
    {
        coinHPLoad();
        coin = int.Parse(CoinList[0].Money);
        return coin;
    }

    public static int getHungry()
    {
        coinHPLoad();
        hungry = int.Parse(HPList[0].HP);
        return hungry;
    }


    private static void coinHPSave()
    {

        // 돈 정보 저장
        string jdata_coin = JsonConvert.SerializeObject(CoinList);
        File.WriteAllText(Application.streamingAssetsPath + "/JSON_files/CoinText.txt", jdata_coin);

        // 체력 정보 저장
        string jdata_hp = JsonConvert.SerializeObject(HPList);
        File.WriteAllText(Application.streamingAssetsPath + "/JSON_files/HPText.txt", jdata_hp);


    }

    private static void coinHPLoad()
    {

        string jdata_coin = File.ReadAllText(Application.streamingAssetsPath + "/JSON_files/CoinText.txt");
        CoinList = JsonConvert.DeserializeObject<List<CoinMoney>>(jdata_coin);

        string jdata_hp = File.ReadAllText(Application.streamingAssetsPath + "/JSON_files/HPText.txt");
        HPList = JsonConvert.DeserializeObject<List<Hungryhp>>(jdata_hp);

    }

    private static void SaveNPCdataToJson()
    {
        string jdata = JsonConvert.SerializeObject(npcData);
        File.WriteAllText(Application.streamingAssetsPath + "/JSON_files/NPCdata.json", jdata);
    }

    private static void LoadNPCdataFromJson()
    {
        string jdata = File.ReadAllText(Application.streamingAssetsPath + "/JSON_files/NPCdata.json");
        npcData = JsonConvert.DeserializeObject<List<NPCdata>>(jdata);
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
