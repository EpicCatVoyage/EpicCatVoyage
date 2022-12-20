using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public string[] game = {"NoteGameMain", "MainGame", "Baking", "CatchMouse", "miniGame_kneading" };

    public GameObject Center;
    public GameObject City;
    public GameObject Home;
    public GameObject Market;
    public GameObject Residential;
    public GameObject School;
    public GameObject UI;
    public GameObject Fade;

    private Animator anim;
    private bool animState = false;


    // Start is called before the first frame update
    void Start()
    {
        anim = UI.GetComponent<Animator>();
        anim.SetBool("State", animState);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGame()
    {
        int num = Random.Range(0, 5);
        Fade f = Fade.GetComponent<Fade>();
        f.L_Fadeout(game[num]);
    }

    public void moveUI()
    {
        if (animState == false)
            animState = true;
        else
            animState = false;

        anim.SetBool("State", animState);
    }

    public void ToCenter()
    {
        Center.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ToCity()
    {
        City.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ToHome()
    {
        Home.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ToMarket()
    {
        Market.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ToResidential()
    {
        Residential.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ToSchool()
    {
        School.SetActive(true);
        gameObject.SetActive(false);
    }
}
