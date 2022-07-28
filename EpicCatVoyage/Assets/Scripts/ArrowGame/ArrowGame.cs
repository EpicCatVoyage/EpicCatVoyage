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
    public AudioSource audioSource1;
    public AudioSource audioSource2;

    void OnEnable()
    {
        print("1");
        wrongscriptholder = GameObject.Find("wrongscriptholder");
        wrongscriptholder.SetActive(false);
        gameRunning = false;
        check = true;
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
        
    }

    void gameMotherBoard()
    {
        print("2");
        checkPushResult();
        compare();
        print(check);
        if (check == true)
        {
            changePosition();
        } 
        if (spawner.nameOfArrow.Count == 0)
        {
            SceneManager.LoadScene("NextLevel");      
        }
    }


    void checkPushResult()
    {
        print("3");
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
        print("4");
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
        print("5");
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

}
