using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class click_event : MonoBehaviour
{
    public Text[] txt;
    public string[] m_text;
    public GameObject HowToPlay;
    public GameObject GameController;

    public void Start()
    {
        StartCoroutine(_typing());
    }

    public void clickOK()
    {
        HowToPlay.SetActive(false);
        
        GameController.GetComponent<GameController>().GameStart = true;
        // print("���ӽ��� ���� �Ϸ�"); -> ok
        GameObject score = GameController.GetComponent<GameController>().score;
        
        score.SetActive(true);
        // print("�������� ���� �Ϸ�"); -> ok
    }

    IEnumerator _typing()
    {
        for (int j = 0; j < 2; j++) {
            for (int i = 0; i <= m_text[j].Length; i++)
            {
                txt[j].text = m_text[j].Substring(0, i);

                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
