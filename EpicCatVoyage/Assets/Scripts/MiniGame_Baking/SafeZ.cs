using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZ : MonoBehaviour
{
    private float limit = 5.0f;
    private float location;
    private Transform tf;
    private Vector3 locationV;

    //�����̴� ��. 
    private float time = 0f;
    private float tr = 0f; //�ð�
    private int dr = 0; //����
    private float vr = 0f; //�ӷ�

    private Rigidbody2D Rd; 
    // Start is called before the first frame update
    void Start()
    {
        Rd = GetComponent<Rigidbody2D>();

        //SafeZone ũ�� ����
        Vector3 size = new Vector3 (0.16f, Random.Range(1f, 2.5f), 0f); //ũ�� ���� ����
        tf = GetComponent<Transform>(); 
        tf.localScale = size; //�� ũ�⿡ ����.

        //SafeZone ��ġ ����
        location = (limit - size.y) / 2;
        locationV = new Vector3(7.01f, Random.Range(-location + 1, location + 1), 0f); //�������� ��ġ ȣ��  //+1�� ���� ��ǥ�� �����ֱ� 
        tf.position = locationV; //����
    }

    // Update is called once per frame
    void Update()
    {
        //������ ������ ������ �ٲ�
        if (-location + 1 >= tf.position.y)
        {
            dr = 1;
        }
        else if (tf.position.y >= location + 1)
        {
            dr = -1;
        }

        //�����̰� �ð� ����
        Rd.velocity = new Vector2(0f, dr * vr);
        time += Time.deltaTime;

        //�ൿ ���� �ٲ��ֱ�
        if (time >= tr)
        {
            time = 0f;
            tr = Random.Range(0.5f, 1.5f);
            dr = Random.Range(-1, 2);
            vr = Random.Range(1f, 5f);
        }
    }
}
