using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;

public class foot : MonoBehaviour
{

    Animator anim;

    public GameObject GameController;
    public GameObject spawner;
    public GameObject explosion;
    public Stopwatch checkTiming;

    public float animationSpeed;

    public float timingCheck;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        checkTiming = spawner.GetComponent<Spawner>().checkTiming;
    }

    // Update is called once per frame
    void Update()
    {
        animationSpeed = spawner.GetComponent<Spawner>().footSpeed;
        anim.SetFloat("footSpeed", animationSpeed);
        //print("���� �ִϸ��̼� ���ǵ�" + animationSpeed);
    }

    private void OnMouseDown()
    {
        //print("Ŭ����");
        Vector3 MousePosition = Input.mousePosition;
        MousePosition = Camera.main.ScreenToWorldPoint(MousePosition);

        Destroy(gameObject);
        Instantiate(explosion, transform.position, Quaternion.identity);
        checkTiming.Stop();
        timingCheck = checkTiming.ElapsedMilliseconds;
        checkTiming.Reset();
        //print("���� üũ�ϴ� �Լ��� �̵�");
        GameController.GetComponent<GameController>().CheckScore(animationSpeed, timingCheck, MousePosition);
    }
}
