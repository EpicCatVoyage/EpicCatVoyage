using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour
{
    public GameObject fadeimage;

    private Image image;

    Color color;

    private void Awake()
    {
        image = fadeimage.GetComponent<Image>();
        color = image.color;

        fadeimage.SetActive(true); //씬 구성 마무리하면 지우고 image 활성화시키는 게 나을 듯
    }

    void Start()
    {
        StartCoroutine(Fadein());
    }

    // Update is called once per frame
    void Update()
    {

    }

    //만약 true면 시행.
    IEnumerator Fadein() //화면이 나오는 것. 
    {
        while (color.a > 0)
        {
            color.a -= Time.deltaTime;
            image.color = color;

            yield return null;
        }

        fadeimage.SetActive(false);
    }

    IEnumerator Fadeout(string Name, bool Loading) //씬 이동
    {
        while (color.a < 1)
        {
            color.a += Time.deltaTime;
            image.color = color;

            yield return null;
        }

        if (color.a >= 1)
        {
            if (Loading)
            LoadingSceneController.LoadScene(Name); //로딩씬을 이용한 로딩.
            else
            SceneManager.LoadScene(Name);
            yield break;
        }
    }

    public void B_Fadeout(string Name)
    {
        fadeimage.SetActive(true);
        StartCoroutine(Fadeout(Name, false));
    }

    public void L_Fadeout(string Name)
    {
        fadeimage.SetActive(true);
        StartCoroutine(Fadeout(Name, true));
    }
}
