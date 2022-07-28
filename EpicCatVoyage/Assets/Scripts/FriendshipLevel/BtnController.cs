using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnController : MonoBehaviour
{
    public GameObject leftBtn;
    public GameObject rightBtn;
    public GameObject screen;
    public Animator screenAnim;

    // Start is called before the first frame update
    void Start()
    {
        screenAnim = screen.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void moveLeft()
    {
        screenAnim.SetTrigger("moveLeft");
        leftBtn.SetActive(false);
        rightBtn.SetActive(true);
    }

    public void moveRight()
    {
        screenAnim.SetTrigger("moveRight");
        rightBtn.SetActive(false);
        leftBtn.SetActive(true);
    }

}
