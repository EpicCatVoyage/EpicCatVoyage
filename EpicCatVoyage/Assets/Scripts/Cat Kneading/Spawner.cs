using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
using debug = UnityEngine.Debug;

public class Spawner : MonoBehaviour
{
    public GameObject GameController;
    public Transform[] spawnPoints;
    public GameObject footPrint;
    public GameObject foot;

    

    public Stopwatch checkTiming;

    public float footSpeed;

    //public float clearTime; //클리어 시간

    private float timeSpawns; //다음 발자국이 나오는 시간을 조절
    public float startSpawnTime;
    public float minTimeBetweenSpawns;
    public float decrease;

    private Transform ex_randomSpawn;
    private Transform randomSpawnPoint;
    public bool GameStart;

    
    // Start is called before the first frame update
    void Start()
    {
        checkTiming = new Stopwatch();
        randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        GameStart = GameController.GetComponent<GameController>().GameStart;
        if (GameStart){
            if (timeSpawns <= 0)
            {
                //spawn footprints
                while (randomSpawnPoint == ex_randomSpawn)
                {
                    randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                }
                
                var vector = randomSpawnPoint.position;
                vector.z = 0;
                randomSpawnPoint.position = vector;

                footSpeed = Random.Range(1, 3);

                var clone = Instantiate(footPrint, randomSpawnPoint.position, Quaternion.identity);
                clone.SetActive(true);
                // print("발자국 생성 완료");
                StartCoroutine(SpawnFoot(randomSpawnPoint));
                Destroy(clone, 3 / footSpeed + 0.35f);
                


                if (startSpawnTime > minTimeBetweenSpawns)
                {
                    startSpawnTime -= decrease;
                }

                timeSpawns = startSpawnTime;

            }
            else
            {
                //시간을 카운트해줌.
                timeSpawns -= Time.deltaTime;
            }
            ex_randomSpawn = randomSpawnPoint;
            //clearTime -= Time.deltaTime; //이것도 필요 만약 클리어 시간이 따로 정해져 있다면.
        }
    }

    IEnumerator SpawnFoot(Transform randomSpawnPoint)
    {
        yield return new WaitForSeconds(0.3f);
        // print("타이머 시작");
        checkTiming.Start();
        var vector = randomSpawnPoint.position;
        vector.z = -1;
        randomSpawnPoint.position = vector;
        var clone = Instantiate(foot, randomSpawnPoint.position, Quaternion.identity);
        clone.SetActive(true);
        Destroy(clone, 3/footSpeed+0.05f);
        
    }

}
