using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public Slider timerSlider;
    public static float gameTime = 10;
    private bool stopTimer;
    // Start is called before the first frame update
    void OnEnable()
    {
        stopTimer = false;   
        timerSlider.maxValue = gameTime;
        timerSlider.value = gameTime; 
    }

    // Update is called once per frame
    void Update()
    {
        float time = gameTime - Time.timeSinceLevelLoad;  

        if(time <= 0)
        {
            stopTimer = true;
            Destroy(GameObject.Find("gameMusicHolder"));
            SceneManager.LoadScene("TimeOver");
        }
        if (stopTimer == false)
        {
            timerSlider.value = time;
        }

    }
}
