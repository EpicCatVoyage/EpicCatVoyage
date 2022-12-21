using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CatBar : MonoBehaviour
{
    private Rigidbody2D PlayerRb;
    private Animator anim;

    private float speed = 7.0f; //���� ����.
    private string test = "Check";

    private float time = 0f;
    private float score = 0f; //�̹� ����
    private float S_score = 0f; //���� ����
    private float S_max = 122f; //E ���� 
    private float S_min = 0f; //E �ּ�
    private bool Gameover = false;

    public GameObject Score; //�����
    public GameObject Imag;//����� �׸�

    // Start is called before the first frame update
    void Start()
    {
        PlayerRb = GetComponent<Rigidbody2D>();
        anim = Imag.GetComponent<Animator>();

        S_score = Score.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("B", PlayerRb.velocity.y >= 0f);

        if (Input.GetKey(KeyCode.Space)) //�Է� ����
        {
            PlayerRb.AddForce(Vector2.up * speed);
        }

        time += Time.deltaTime;
        if (!Gameover && time >= 0.1f)
        {
            S_score += score; //���� �ݿ�
            Score.transform.localScale = new Vector3(S_score, Score.transform.localScale.y, 0f);
            time = 0f;

            if (S_score >= S_max) //���
            {
                Debug.Log("�̰��");
                Gameover = true;
                gameObject.SetActive(false);
                SceneManager.LoadScene("Baking_Ending");
            }
            else if (S_score <= S_min)
            {
                Debug.Log("����");
                Gameover = true;
                gameObject.SetActive(false);
                SceneManager.LoadScene("Baking_BadEnding");
            }
        }
    }

    void OnTriggerStay2D(Collider2D other) //SafeZone(�����) �ȿ� �ִ��� �˻�
    {
        if (test == other.tag)
        {
            score = - 0.5f;
        }
        else
        {
            score = 0.5f;
        }

        test = other.tag;
    }
}
