using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArrows : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] gameArrows;
    public static int spawnNum = 6;
    public List <GameObject> showArrow = new List <GameObject>();
    public List <string> nameOfArrow = new List <string> ();
    public GameObject[] renameArrows;

    void Start()
    {
        
    }

    void Update()
    {
        renameArrows = GameObject.FindGameObjectsWithTag("arrows");
        for (int x = 0; x < renameArrows.Length; x++)
        {
            if (renameArrows[x].transform.position == spawnPoints[0].position)
            {
                renameArrows[x].name = "first";
            }
            if (renameArrows[x].transform.position == spawnPoints[1].position)
            {
                renameArrows[x].name = "second";
            }
            if (renameArrows[x].transform.position == spawnPoints[2].position)
            {
                renameArrows[x].name = "third";
            }
            if (renameArrows[x].transform.position == spawnPoints[3].position)
            {
                renameArrows[x].name = "fourth";
            }
            if (renameArrows[x].transform.position == spawnPoints[4].position)
            {
                renameArrows[x].name = "fifth";
            }
        }
    }

    void OnEnable()
    {

        for (int x = 0; x < spawnNum; x++)
        {
            GameObject addArrow;
            addArrow = gameArrows[Random.Range(0,4)];
            showArrow.Add(addArrow);
            nameOfArrow.Add(addArrow.name);     
        }
        for (int x = 0; x < spawnNum; x++)
        {
            if (x<5)
            {
                Instantiate(showArrow[x],spawnPoints[x].position,Quaternion.identity);
            }
        }
    }
}
