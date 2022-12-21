using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArrowGame : MonoBehaviour
{
    bool gameRunning;
    string pushedArrow;
    public SpawnArrows spawner;
    public bool check;
    public GameObject[] repositionArrows;
    public GameObject wrongscriptholder;
    public GameObject gamescriptholder;
    public AudioSource audioSource1;
    public AudioSource audioSource2;
    public static int reloadedNum = 0;
    private delegate IEnumerator CoroutineDelegate();

    void OnEnable()
    {
        wrongscriptholder = GameObject.Find("wrongscriptholder");
        wrongscriptholder.SetActive(false);
        gameRunning = false;
        check = true;
        GameObject[] scriptObj = GameObject.FindGameObjectsWithTag("ArrowGameScript");
        if (scriptObj.Length > 1)
        {
            Destroy(scriptObj[0]);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)||Input.GetKeyDown(KeyCode.DownArrow)||Input.GetKeyDown(KeyCode.RightArrow)||Input.GetKeyDown(KeyCode.LeftArrow)||Input.GetKeyDown(KeyCode.UpArrow))
        {
            gameRunning = true;
        }
        if (gameRunning == true) 
        {
            gameMotherBoard();
        }
        gameRunning = false;
        
        if (reloadedNum >= 4)
        {
            Destroy(gamescriptholder);
            Destroy(GameObject.Find("gameMusicHolder"));
            SceneManager.LoadScene("ArrowGame_Ending");
        }
        
    }

    void gameMotherBoard()
    {
        checkPushResult();
        compare();
        print(check);
        if (check == true)
        {
            changePosition();
        } 
        if (spawner.nameOfArrow.Count == 0)
        {
            reloadedNum ++;
            levelManual();  
            print ("NEW LEVEL");
            print(reloadedNum);
            print(SpawnArrows.spawnNum);
            print(Timer.gameTime);
            print(CatPosition.x);
            SceneManager.LoadScene("MainGame");  
        }
    }


    void checkPushResult()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            pushedArrow = "uparrow";
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            pushedArrow = "downarrow";
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            pushedArrow = "leftarrow";
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            pushedArrow = "rightarrow";
        }
    }

    bool compare()
    {
        if (spawner.nameOfArrow[0]==pushedArrow)
        {
            spawner.nameOfArrow.Remove(spawner.nameOfArrow[0]);
            spawner.showArrow.Remove(spawner.showArrow[0]);
            check = true;
            audioSource1.Play();
        }
        else
        {
            check = false;
            wrongscriptholder.SetActive(true);
            audioSource2.Play();
        }
        return check;
    }

    void changePosition()
    {
        repositionArrows = GameObject.FindGameObjectsWithTag("arrows");
        for (int x = 0; x < repositionArrows.Length; x++)
        {
            if (repositionArrows[x].name == "first")
            {
                Destroy(repositionArrows[x]);
            }
            if (repositionArrows[x].name == "second")
            {
                repositionArrows[x].transform.position = spawner.spawnPoints[0].position;
            }
            if (repositionArrows[x].name == "third")
            {
                repositionArrows[x].transform.position = spawner.spawnPoints[1].position;
            }
            if (repositionArrows[x].name == "fourth")
            {
                repositionArrows[x].transform.position = spawner.spawnPoints[2].position;
            }
            if (repositionArrows[x].name == "fifth")
            {
                repositionArrows[x].transform.position = spawner.spawnPoints[3].position;
            }  
        }
        if (spawner.showArrow.Count >= 5)
        {
            Instantiate(spawner.showArrow[4], spawner.spawnPoints[4].position, Quaternion.identity);
        }
    }

    void levelManual()
    {
        if (reloadedNum == 1)
        {
            SpawnArrows.spawnNum = 10;
            Timer.gameTime = 4;
            CatPosition.x = 0.4f;
            
        }
        if (reloadedNum == 2)
        {
            SpawnArrows.spawnNum = 15;
            Timer.gameTime = 6;
            CatPosition.x = 0.3f;
            
        }
        if (reloadedNum == 3)
        {
            SpawnArrows.spawnNum = 22;
            Timer.gameTime = 8;
            CatPosition.x = 0.1f;
            
        }
        if (reloadedNum == 4)
        {
            SceneManager.LoadScene("ArrowGame_Ending");  
            
        }
        
    }

}
