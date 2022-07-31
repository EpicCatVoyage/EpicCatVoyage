using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatPosition : MonoBehaviour
{
    public Sprite upcat1;
    public Sprite upcat2;
    public Sprite downcat1;
    public Sprite downcat2;
    public Sprite leftcat1;
    public Sprite leftcat2;
    public Sprite rightcat1;
    public Sprite rightcat2;
    public Sprite angrycat;
    public SpriteRenderer spriteRenderer;
    public static float x = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            StopAllCoroutines();
            StartCoroutine("upcat");
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            StopAllCoroutines();
            StartCoroutine("downcat");
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            StopAllCoroutines();
            StartCoroutine("leftcat");
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            StopAllCoroutines();
            StartCoroutine("rightcat");
        }
    }
    IEnumerator upcat()
    {
        while(true)
        {
            yield return new WaitForSeconds(x);
            spriteRenderer.sprite = upcat1;     
            yield return new WaitForSeconds(x);
            spriteRenderer.sprite = upcat2;
        } 
    }

    IEnumerator downcat()
    {
        while (true)
        {
            yield return new WaitForSeconds(x);
            spriteRenderer.sprite = downcat1;     
            yield return new WaitForSeconds(x);
            spriteRenderer.sprite = downcat2;
        } 
    }

    IEnumerator leftcat()
    {
        while(true)
        {
            yield return new WaitForSeconds(x);
            spriteRenderer.sprite = leftcat1;
            yield return new WaitForSeconds(x);
            spriteRenderer.sprite = leftcat2;
        } 
    }

    IEnumerator rightcat()
    {
        while (true)
        {
            yield return new WaitForSeconds(x);
            spriteRenderer.sprite = rightcat1;     
            yield return new WaitForSeconds(x);
            spriteRenderer.sprite = rightcat2;
        }
        
    }
}
