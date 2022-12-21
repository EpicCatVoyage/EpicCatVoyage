using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;

public class GameController : MonoBehaviour
{
    public bool GameStart;

    public GameObject spawner;
    public GameObject score;
    public Text Score;
    public GameObject Ending;

    public GameObject[] result;

    private Animator Endinganim;

    public int totalScore = 0;

    public Stopwatch checkTiming;

    public float perfectTime;

    // Start is called before the first frame update
    void Start()
    {
        Endinganim = Ending.GetComponent<Animator>();
        GameStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckScore(float GameFootSpeed, float timing, Vector3 mousePosition)
    {
        /*checkTiming = spawner.GetComponent<Spawner>().checkTiming;
        timing = checkTiming.ElapsedMilliseconds;*/

        perfectTime = 2300 / GameFootSpeed;

        /*print("GameFootSpeed = " + GameFootSpeed);
        print("perfectTime = " + perfectTime);
        print("Timing = " + timing);*/

        // perfect / good / normal / bad 타이밍은 플레이 해보고 조정하면 될 듯
        if (GameStart)
        {
            var vector = mousePosition;
            vector.z = -8;
            mousePosition = vector;
            
            if (perfectTime - timing < 300 && perfectTime - timing > -300)
            {
                //print("timing : perfect");
                totalScore += 10;
                var clone = Instantiate(result[0], mousePosition, Quaternion.identity);
                Destroy(clone, .6f);
            }
            else if (perfectTime - timing < 500 && perfectTime - timing > -500)
            {
                //print("timing : good");
                totalScore += 8;
                var clone = Instantiate(result[1], mousePosition, Quaternion.identity);
                Destroy(clone, .6f);
            }
            else if (perfectTime - timing < 700 && perfectTime - timing > -700)
            {
                //print("timing : normal");
                totalScore += 4;
                var clone = Instantiate(result[2], mousePosition, Quaternion.identity);
                Destroy(clone, .6f);
            }
            else
            {
                //print("timing : bad timing");
                totalScore += 2;
                var clone = Instantiate(result[3], mousePosition, Quaternion.identity);
                Destroy(clone, .6f);
            }
            Score.text = "score: " + totalScore.ToString();

            // 게임 클리어 점수 설정 해야함
            if (totalScore >= 100)
            {
                StoreInfo.setFriendship(StoreInfo.getFriendship() + 10);
                StoreInfo.setCoin(StoreInfo.getCoin() + 1000);
                GameStart = false;
                Endinganim.SetTrigger("Ending");
                Invoke("HeartMoving", 2f);
            }
        }
        
    }

    public void HeartMoving()
    {
        Endinganim.SetTrigger("heart");
    }
}
