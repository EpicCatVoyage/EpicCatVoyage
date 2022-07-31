using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZ : MonoBehaviour
{
    private float limit = 5.0f;
    private float location;
    private Transform tf;
    private Vector3 locationV;

    //움직이는 것. 
    private float time = 0f;
    private float tr = 0f; //시간
    private int dr = 0; //방향
    private float vr = 0f; //속력

    private Rigidbody2D Rd; 
    // Start is called before the first frame update
    void Start()
    {
        Rd = GetComponent<Rigidbody2D>();

        //SafeZone 크기 설정
        Vector3 size = new Vector3 (0.16f, Random.Range(1f, 2.5f), 0f); //크기 랜덤 생성
        tf = GetComponent<Transform>(); 
        tf.localScale = size; //바 크기에 적용.

        //SafeZone 위치 설정
        location = (limit - size.y) / 2;
        locationV = new Vector3(7.01f, Random.Range(-location + 1, location + 1), 0f); //랜덤으로 위치 호출  //+1는 실제 좌표로 맞춰주기 
        tf.position = locationV; //적용
    }

    // Update is called once per frame
    void Update()
    {
        //범위를 넘으면 방향을 바꿈
        if (-location + 1 >= tf.position.y)
        {
            dr = 1;
        }
        else if (tf.position.y >= location + 1)
        {
            dr = -1;
        }

        //움직이고 시간 누적
        Rd.velocity = new Vector2(0f, dr * vr);
        time += Time.deltaTime;

        //행동 패턴 바꿔주기
        if (time >= tr)
        {
            time = 0f;
            tr = Random.Range(0.5f, 1.5f);
            dr = Random.Range(-1, 2);
            vr = Random.Range(1f, 5f);
        }
    }
}
