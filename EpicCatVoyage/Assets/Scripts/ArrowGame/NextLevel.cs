using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public static int reloadedNum = 0;
    private delegate IEnumerator CoroutineDelegate();
    public SpawnArrows spawner;
    // Start is called before the first frame update
    void OnEnable()
    {
        reloadedNum ++;
        print ("NEW LEVEL");
        Invoke ("levelManual", 7.0f);
        if (reloadedNum == 4)
        {
            Destroy(GameObject.Find("gameMusicHolder"));
            SceneManager.LoadScene("ArrowGame_Ending");
        }       
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void levelManual()
    {
        if (reloadedNum == 1)
        {
            SpawnArrows.spawnNum = 10;
            Timer.gameTime = 4;
            CatPosition.x = 0.4f;
            SceneManager.LoadScene("MainGame");
        }
        if (reloadedNum == 2)
        {
            SpawnArrows.spawnNum = 15;
            Timer.gameTime = 6;
            CatPosition.x = 0.3f;
            SceneManager.LoadScene("MainGame");
        }
        if (reloadedNum == 3)
        {
            SpawnArrows.spawnNum = 22;
            Timer.gameTime = 8;
            CatPosition.x = 0.1f;
            SceneManager.LoadScene("MainGame");
        }
        
    }
}
