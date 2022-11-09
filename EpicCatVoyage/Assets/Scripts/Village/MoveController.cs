using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public GameObject Center;
    public GameObject City;
    public GameObject Home;
    public GameObject Market;
    public GameObject Residential;
    public GameObject School;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
