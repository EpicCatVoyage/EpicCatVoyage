using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DDM : MonoBehaviour
{
    public GameObject Set;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) == true)
        {
            buttonSetting();
        }
    }

    public void goToInven()
    {
        SceneManager.LoadScene("Inventory");
    }

    public void goToCust()
    {
        SceneManager.LoadScene("Customizing");
    }

    public void goToStage()
    {
        SceneManager.LoadScene("StageChoice");
    }

    public void goToVillage()
    {
        SceneManager.LoadScene("Village");
    }

    public void buttonSetting()
    {
        if (Set.activeSelf == true)
            Set.SetActive(false);
        else
            Set.SetActive(true);
    }
}
