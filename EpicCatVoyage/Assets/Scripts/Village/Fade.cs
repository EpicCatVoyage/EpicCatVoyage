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

        fadeimage.SetActive(true); //�� ���� �������ϸ� ����� image Ȱ��ȭ��Ű�� �� ���� ��
    }

    void Start()
    {
        StartCoroutine(Fadein());
    }

    // Update is called once per frame
    void Update()
    {

    }

    //���� true�� ����.
    IEnumerator Fadein() //ȭ���� ������ ��. 
    {
        while (color.a > 0)
        {
            color.a -= Time.deltaTime;
            image.color = color;

            yield return null;
        }

        fadeimage.SetActive(false);
    }

    IEnumerator Fadeout(string Name, bool Loading) //�� �̵�
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
            LoadingSceneController.LoadScene(Name); //�ε����� �̿��� �ε�.
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
/*��뼳���� 
 * 1. �ε� �� �ϰ� �� ��ȯ�ϰ� �ʹ� >> B_Fadeout ȣ��
 * 2. �ε��ϰ� �� ��ȯ�ϰ� �ʹ� >> L_Fadeout ȣ��
 */
