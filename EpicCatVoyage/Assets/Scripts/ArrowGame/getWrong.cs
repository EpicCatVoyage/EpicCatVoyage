using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getWrong : MonoBehaviour
{
    public SpawnArrows spawner;
    public ArrowGame gameinfo;
    public SpriteRenderer spriteRenderer2;
    public Sprite angryCat;
    public Sprite upCat;
    public GameObject wrongUpArrow;
    public GameObject wrongDownArrow;
    public GameObject wrongLeftArrow;
    public GameObject wrongRightArrow;
    public GameObject wrongArrow;
    public GameObject orgArrow;
    private bool waitCheck;
    public GameObject gamescriptholder;
    public GameObject catscriptholder;
    private delegate IEnumerator CoroutineDelegate();
    private CoroutineDelegate crt;

    void OnEnable()
    {
        gamescriptholder = GameObject.Find("gamescriptholder");
        catscriptholder = GameObject.Find("catscriptholder");
        waitCheck = gameinfo.check;
        crt = wrongFunction;
        orgArrow = GameObject.Find("first");
        StartCoroutine(crt());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator wrongFunction()
    {
        yield return new WaitUntil(()=> waitCheck == false);
        gamescriptholder.SetActive(false);
        catscriptholder.SetActive(false);
        spriteRenderer2.sprite = angryCat;
        createWrongArrow();
        yield return new WaitForSeconds(0.5f);
        wrongArrow.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        wrongArrow.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        wrongArrow.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        wrongArrow.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        wrongArrow.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        spriteRenderer2.sprite = upCat;
        orgArrow.SetActive(true);
        Destroy(wrongArrow);
        gamescriptholder.SetActive(true);
        catscriptholder.SetActive(true); 
        
    }

    void createWrongArrow()
    {
        orgArrow.SetActive(false);
        if (spawner.nameOfArrow[0]=="uparrow")
        {
            wrongArrow = Instantiate(wrongUpArrow, spawner.spawnPoints[0].position ,Quaternion.identity);
        }
        if (spawner.nameOfArrow[0]=="downarrow")
        {
            wrongArrow = Instantiate(wrongDownArrow, spawner.spawnPoints[0].position ,Quaternion.identity);
        }
        if (spawner.nameOfArrow[0]=="leftarrow")
        {
            wrongArrow = Instantiate(wrongLeftArrow, spawner.spawnPoints[0].position ,Quaternion.identity);
        }
        if (spawner.nameOfArrow[0]=="rightarrow")
        {
            wrongArrow = Instantiate(wrongRightArrow, spawner.spawnPoints[0].position ,Quaternion.identity);
        }
        
    }
}
