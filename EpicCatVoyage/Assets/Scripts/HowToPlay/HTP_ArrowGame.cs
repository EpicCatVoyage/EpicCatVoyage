using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HTP_ArrowGame : MonoBehaviour
{
    public Text[] txt;
    public string[] m_text;

    public void Start()
    {
        StartCoroutine(_typing());
    }

    public void clickOK()
    {
        SceneManager.LoadScene("MainGame"); //정확히 어떤 씬으로 이동해야되는지 확인 필요
    }


    IEnumerator _typing()
    {
        for (int j = 0; j < 2; j++)
        {
            for (int i = 0; i <= m_text[j].Length; i++)
            {
                txt[j].text = m_text[j].Substring(0, i);

                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
