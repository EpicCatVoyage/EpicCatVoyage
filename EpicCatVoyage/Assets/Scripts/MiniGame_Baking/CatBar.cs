using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CatBar : MonoBehaviour
{
    private Rigidbody2D PlayerRb;
    private Animator anim;

    private float speed = 7.0f; //감도 조절.
    private string test = "Check";

    private float time = 0f;
    private float score = 0f; //이번 점수
    private float S_score = 0f; //누적 점수
    private float S_max = 122f; //E 만땅 
    private float S_min = 0f; //E 최소
    private bool Gameover = false;

    public GameObject Score; //녹색바
    public GameObject Imag;//고양이 그림

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

        if (Input.GetKey(KeyCode.Space)) //입력 감지
        {
            PlayerRb.AddForce(Vector2.up * speed);
        }

        time += Time.deltaTime;
        if (!Gameover && time >= 0.1f)
        {
            S_score += score; //점수 반영
            Score.transform.localScale = new Vector3(S_score, Score.transform.localScale.y, 0f);
            time = 0f;

            if (S_score >= S_max) //결과
            {
                Debug.Log("이겼다");
                Gameover = true;
                gameObject.SetActive(false);
                SceneManager.LoadScene("Baking_Ending");
            }
            else if (S_score <= S_min)
            {
                Debug.Log("졌어");
                Gameover = true;
                gameObject.SetActive(false);
                SceneManager.LoadScene("Baking_BadEnding");
            }
        }
    }

    void OnTriggerStay2D(Collider2D other) //SafeZone(녹색바) 안에 있는지 검사
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
